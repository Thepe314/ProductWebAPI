# IMS PRODUCTAPI

A simple inventory Managment System built with .Net and SQL Server (SSMS)

## What it does
-Manages Product (CREATE, READ, UPDATE, DELETE)
-Restock and Sell Products
-View your montly sales report

## Technologies Used
- .Net 8
-Swagger UI
-SQL Server (SSMS)

## How to run the project
### 1.Clone the repo git hub
https://github.com/Thepe314/ProductWebAPI.git

### 2. Set up the database 
- Open SSMS  and connect to your SQL Server
- Update the connection string in appsettings with your server name: "DefaultConnection";
- "Server=YOUR_SERVER_NAME\\SQLEXPRESS;Database=ProductsDB;Trusted_Connection=True;TrustServerCertificate=True;"

### 3.Run the project 
- using 'dotnet run'

### 4.Open the swagger
-Go to http://localhost:5078/swagger 
-Test the API

## Api Endpoints
### Products
-GET /api/Products  -> Get a list of products
-POST /api/Products -> Create a new product
-GET /api/Products/ {id} ->Find a product with id
-PUT /api/products/ {id} ->Change values of product with id
-DELETE /api/products/{id} ->Delete an existing product

### Transactions
-POST /api/Transactions/restock ->Add stock to existing product (Seller restocking product)
-POST /api/Transactions/sell -> Decrease stock from exisitng product (Customer buys a product)
-GET /api/Transactions/report -> Get totalTransactions, TotalQuantity, Total Revenue from a month, in this case 2 where it is februray)


