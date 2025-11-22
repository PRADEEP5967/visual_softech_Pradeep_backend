using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication3.Data;
using WebApplication3.DTOs;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
     
       
            private readonly ApplicationDbContext _context;

            public StudentsController(ApplicationDbContext context)
            {
                _context = context;
            }

        // GET ALL STUDENTS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _context.Students.ToListAsync();

            if (students == null || students.Count == 0)
                return Ok(new List<object>()); // return empty array instead of null

            var result = students.Select(s => new
            {
                s.Id,
                s.Name,
                s.Age,
                s.Address,
                s.State,
                s.PhoneNumber,
                s.Subjects,
                Photo = s.Photo != null ? Convert.ToBase64String(s.Photo) : null
            });

            return Ok(result);
        }


        // GET BY ID
        [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                    return NotFound(new { message = "Student Not Found" });

                return Ok(student);
            }

            // CREATE
            [HttpPost]
            public async Task<IActionResult> Create([FromForm] StudentDto dto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var student = new Student
                {
                    Name = dto.Name,
                    Age = dto.Age,
                    Address = dto.Address,
                    State = dto.State,
                    PhoneNumber = dto.PhoneNumber,
                    Subjects = dto.Subjects,
                };

                if (dto.Photo != null)
                {
                    using var ms = new MemoryStream();
                    await dto.Photo.CopyToAsync(ms);
                    student.Photo = ms.ToArray();
                }

                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                return Ok(student);
            }

            // UPDATE
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromForm] StudentDto dto)
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                    return NotFound(new { message = "Student Not Found" });

                student.Name = dto.Name;
                student.Age = dto.Age;
                student.Address = dto.Address;
                student.State = dto.State;
                student.PhoneNumber = dto.PhoneNumber;
                student.Subjects = dto.Subjects;

                if (dto.Photo != null)
                {
                    using var ms = new MemoryStream();
                    await dto.Photo.CopyToAsync(ms);
                    student.Photo = ms.ToArray();
                }

                await _context.SaveChangesAsync();

                return Ok(student);
            }

            // DELETE
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                    return NotFound(new { message = "Student Not Found" });

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Deleted Successfully" });
            }
        }

    }

