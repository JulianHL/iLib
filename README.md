# 📚 iLib — Library Management System

A role-based Library Management System built with **C# and .NET (MVC architecture)** where:

- 🎓 Students can borrow and return books  
- 📚 Librarians can manage and administer the catalog  

The interface dynamically adapts depending on the authenticated user role.

---

## 🚀 Overview

iLib is a structured digital solution designed to replace manual library processes with an automated and secure system.

The application ensures:

- Role-based dashboards
- Real-time inventory updates
- Secure authentication & authorization
- Persistent loan tracking
- Clean separation of concerns using MVC

---

## 🏗️ Architecture

The system follows the **MVC (Model–View–Controller)** architectural pattern:

### 🧩 Model
- Represents core entities (Users, Books, Loans)
- Handles data structure and validation
- Managed through **Entity Framework**

### 🎨 View
- Role-based user interface
- Dynamic dashboards (Student / Librarian)
- Displays real-time system data

### 🎮 Controller
- Handles user requests
- Applies business logic
- Coordinates between Views and Models

---

## 👥 User Roles

### 🎓 Student

Students can:

- Browse available books
- Request book loans
- Return borrowed books
- View their loan history
- Check availability in real time

---

### 📚 Librarian

Librarians can:

- Add new books
- Update book information
- Remove books from inventory
- Monitor active loans
- Manage stock availability

---

## 🔄 Role-Based Interface

After authentication, the system identifies the user's role and renders the corresponding dashboard:

- `Student Dashboard`
- `Librarian Dashboard`

This ensures:

- Secure access control  
- Clear separation of responsibilities  
- Improved user experience  

---

## 🛠️ Tech Stack

- **Language:** C#  
- **Framework:** .NET  
- **Architecture Pattern:** MVC  
- **ORM:** Entity Framework  
- **Database:** SQL Server (Relational Database)  

---

## 🔐 Security

- Authentication system  
- Role-based authorization  
- Protected administrative actions  
- Data validation at model level  

---

## 📊 System Flow

1. User logs in  
2. System authenticates credentials  
3. Role is identified  
4. Appropriate dashboard is rendered  
5. Borrow/Return/Manage actions update the database through Entity Framework  
6. Inventory reflects changes instantly  

---

## 🎯 Project Goal

To provide an automated, structured, and scalable solution for institutional library management using modern .NET development practices.

---
