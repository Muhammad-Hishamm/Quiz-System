# 📚 Quiz Management System

An examination platform built using **ASP.NET Core Web API** and **EF Core**.
The system enables instructors to create and manage quizzes for their courses, and allows students to participate in exams and receive automated results.

---

## 🚀 Project Overview

This project implements a structured online Quiz/Exam System based on a detailed Software Requirements Specification (SRS). It supports course management, exam creation, automatic evaluation, authentication, and controlled student access.

The goal is to provide a scalable, secure, and user-friendly assessment platform for academic institutions.

---

## ✅ Core Features

### 👨‍🏫 Instructor Capabilities

* Register/login securely
* Create, edit, and delete courses
* Add/manage questions and multiple-choice answers
* Create quizzes/final exams with question difficulty levels:

  * Simple
  * Medium
  * Hard
* Assign students to courses
* Assign exams to enrolled students
* Choose:

  * **Manual exam question selection**, or
  * **Automatic generation** with balanced difficulty
* View results of all students per exam

### 🎓 Student Capabilities

* Register/login
* Enroll in assigned courses
* Take assigned quizzes and exams
* Can take multiple quizzes per course
* Can take **only one final exam** per course
* View results immediately after submitting

---

## 🏗 System Architecture

| Layer          | Technology                 |
| -------------- | -------------------------- |
| Backend API    | ASP.NET Core Web API       |
| ORM            | Entity Framework Core      |
| Database       | SQL Server (Relational DB) |
| Authentication | JWT / Role Based           |
| Design         | RESTful Architecture       |

---

## 🧩 Business Rules

* Instructor can only view and manage their own content
* Questions can be reused in multiple exams
* Students can take exams **only** for courses they are enrolled in and assigned to
* Automatic exam generation balances difficulty types
* Prevent cascading delete issues using restricted relationships

---

## 🗂 Database Model (Summary)

Entities include:

* Instructor
* Student
* Course
* Exam (Quiz / Final)
* Question (with Difficulty)
* Choice
* StudentExam
* ExamQuestion

Relationships:

* Many-to-many:

  * Student ↔ Course
  * Student ↔ Exam
  * Exam ↔ Question
* One-to-many:

  * Instructor → Course, Question
  * Course → Exam
  * Question → Choice

---

## 🔐 Security

* Role-based Authorization
* Encrypted communications (HTTPS)
* Validation and ownership checks

---

## 📈 Non-Functional Requirements

* ✅ Handles up to 100 concurrent users
* ✅ Responsive and intuitive UI when a frontend is integrated
* ✅ 99.9% uptime target
* ✅ Clean architecture and maintainable code

---

## 🛠 How to Run the Project

```
git clone <repository-url>
cd QuizSystem
update appsettings.json with SQL Server credentials
Add-Migration InitialCreate
Update-Database
dotnet run
```

API will start on:

```
https://localhost:5001
http://localhost:5000
```

---

## 📌 Future Enhancements

* UI frontend with Angular/React
* Question randomization
* Time-limited exams
* Advanced reporting and analytics
* Cloud deployment

---
