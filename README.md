# Order & User Management System - Windows Forms App (C#)

This is a desktop-based application built using C# and Windows Forms as part of a database programming course. It allows users to log in and perform various role-based operations such as managing users, products, orders, and shipments.

## 🚀 Features

- 🧑‍💼 User login system
- 🛠 Role-based panels: Admin, Courier, Customer
- 📦 Add and list products
- 🧾 Add and manage orders
- 💰 Handle payment entries
- 🚚 View shipment and delivery details

## 🗂 Project Structure

## 🖼 Screenshots
![image](https://github.com/user-attachments/assets/7613a46f-4f56-4e6d-80e8-0c6f671017ec)

![image](https://github.com/user-attachments/assets/6592f8f9-8aa6-4e3a-8ee4-867323f665d6)

### 🔐 Login Screen
![image](https://github.com/user-attachments/assets/44163437-5f92-42a5-8c74-2569ef3ba91e)

### ➕ Add Order
![image](https://github.com/user-attachments/assets/3c902321-93a5-4da1-8a67-90ec87baacdd)


### 📦 Add Product
![image](https://github.com/user-attachments/assets/d59f7e2d-58af-486b-af62-c827dbfb377a)


> Gray boxes represent data grids showing listed data such as orders or products.

## 🛢 Database

The application requires a SQL Server (or compatible) database. Update the connection string in `App.config` according to your local setup. Expected tables include:

- `Users`
- `Roles`
- `Products`
- `Orders`
- `Payments`
- `Shipments`

## ▶️ How to Run

1. Clone or download this repository.
2. Open the solution in **Visual Studio**.
3. Check and update the connection string in `App.config`.
4. Ensure your database and required tables are created.
5. Build and run the application.

## 👨‍💻 Author

**Isa Şentürk**  
This project was developed as part of a database course assignment.

This project is for educational purposes only. Not licensed for commercial use.
