

✔ Full project description
✔ Technologies
✔ Features
✔ Folder structure
✔ API details
✔ Database schema
✔ SQL instructions
✔ How to run (frontend + backend)
✔ JWT + Swagger info
✔ Student CRUD details
✔ Pagination
✔ Image upload notes

---

# ✅ **README.md (Backend Repository)**

### File: `README.md`

Copy–paste the full content below 👇

---

# 📘 **Student Management System – Backend (.NET + SQL Server + JWT)**

This is the **backend API** for the Student Management System built for the Visual Softech assignment.

The application supports:

* User Login
* JWT-based authentication
* Student CRUD operations
* Subject management
* Image upload + compression
* State dynamic master
* Delete, Update with password
* SQL Server Database
* Swagger API Documentation

---

## ✅ **Tech Stack**

| Component         | Technology                      |
| ----------------- | ------------------------------- |
| Backend Framework | **ASP.NET Core Web API**        |
| Language          | **C#**                          |
| Database          | **MS SQL Server (SSMS)**        |
| Authentication    | **JWT Token**                   |
| Image Processing  | **C# Image Compression (≤2KB)** |
| Documentation     | **Swagger / Swashbuckle**       |

---

## 📁 **Project Structure**

```
/Controllers
    AuthController.cs
    StudentController.cs
    StateController.cs

/Models
    User.cs
    Student.cs
    StudentDetail.cs
    State.cs

/Data
    ApplicationDbContext.cs

/Helpers
    JwtService.cs
    FileCompressor.cs

/sql
    database.sql (FULL DB creation script)
```

---

# 🔐 **Authentication – JWT**

* User logs in using:

  ```
  username: admin  
  password: admin123
  ```
* On success → server returns a **JWT token**
* Frontend must store the token (localStorage)
* All private endpoints require:

  ```
  Authorization: Bearer <token>
  ```

---

# 🧪 **Swagger**

After running backend:

```
https://localhost:7029/swagger
https://localhost:7029/swagger/index.html
```

---

# 🗄 **Database Schema (SQL Server)**

A file named **sql/database.sql** is included.
It contains:

### ✔ Tables

1. `user_master` – login users
2. `student_master` – student main info
3. `student_detail` – subject list
4. `state_name` – states list

### ✔ Views

(optional: students + subjects combined)

### ✔ Functions

(optional helper functions)

---

### ✔ **SQL Schema (copy into sql/database.sql)**

```sql
CREATE TABLE user_master (
    id INT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(50),
    password VARCHAR(100)
);

INSERT INTO user_master (username, password)
VALUES ('admin', 'admin123');


CREATE TABLE state_name (
    id INT IDENTITY(1,1) PRIMARY KEY,
    state VARCHAR(100)
);

CREATE TABLE student_master (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(150) NOT NULL,
    age INT NOT NULL,
    dob DATE NOT NULL,
    address VARCHAR(255),
    state_id INT NOT NULL,
    phone VARCHAR(15),
    photo_path VARCHAR(255),
    FOREIGN KEY (state_id) REFERENCES state_name(id)
);

CREATE TABLE student_detail (
    id INT IDENTITY(1,1) PRIMARY KEY,
    student_id INT NOT NULL,
    subject_name VARCHAR(150),
    FOREIGN KEY (student_id) REFERENCES student_master(id)
);
```

Upload this into:

```
backend/sql/database.sql
```

---

# 🚀 **How to Run Backend**

### **1. Restore packages**

```
dotnet restore
```

### **2. Update your SQL connection string**

File: `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### **3. Run**

```
dotnet run
```

Backend starts at:

```
https://localhost:7029
```

---

# 🧩 **API Endpoints**

## 🔐 Auth

| Method | Endpoint          | Description       |
| ------ | ----------------- | ----------------- |
| POST   | `/api/auth/login` | Returns JWT token |

---

## 👨‍🎓 Student

| Method | Endpoint                              | Description                                 |
| ------ | ------------------------------------- | ------------------------------------------- |
| GET    | `/api/student/all?page=1&pageSize=10` | Get students with pagination                |
| GET    | `/api/student/{id}`                   | Get student by ID                           |
| POST   | `/api/student/create`                 | Create student (with subjects + photos)     |
| PUT    | `/api/student/update/{id}`            | Update student (password required: `72991`) |
| DELETE | `/api/student/delete/{id}`            | Delete student + subjects                   |

### 📌 Update Password Rule

A modal in frontend asks the user:

```
Enter update password:
```

Password is:

```
72991
```

---

## 🌍 State

| Method | Endpoint            | Description        |
| ------ | ------------------- | ------------------ |
| GET    | `/api/state/all`    | Get all states     |
| POST   | `/api/state/create` | Create a new state |

---

# 🖼 Photo Upload Rules

* User can upload multiple images
* Must be compressed to **≤ 2 KB each**
* Saved into `/uploads` folder
* Path stored in DB

---

---

