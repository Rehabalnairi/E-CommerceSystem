"# E-CommerceSystem-" 
this system is a comprehensive e-commerce platform designed to facilitate online shopping and streamline the buying and selling process. 
It offers a user-friendly interface for customers to browse products, add items to their cart, 
and complete purchases securely. 
The system also includes features for sellers to manage their inventory,
process orders, and track sales performance.
 ** Fisrt you neet to update:
 go to Tool -Packege Manager Console and run this commands:
 Update-database ,then check in Sql Server managment studio.
 this project have 5 tables in database:
 1-Users(store user information)
 columns:
 UId (Primary Key): unique identifier for each user.
 UName
 Email
 Password
 Role (e.g., customer, seller)
 CreatedAt.
-One-to-many → Orders
-One-to-many → Reviews

2-Products
 columns:
 PId (Primary Key): unique identifier for each product.
 ProductName
 Description
 Price
 Stock
 overallRating
-Many-to-many ↔ Orders (via OrderProducts)
-One-to-many → Reviews

 3-Orders
 columns:
 OId (Primary Key): unique identifier for each order.
 OrderDate
 TotalAmount
 UId (Foreign Key referencing Users)
-Many-to-one → Users
-Many-to-many ↔ Products (via OrderProducts)

 4-Reviews
 columns:
 ReviewID (Primary Key): unique identifier for each review.
 Rating
 Comment
 ReviewDate
 UId (Foreign Key referencing Users)
 PId (Foreign Key referencing Products)
-Many-to-one → Users
-Many-to-one → Products 

 5-OrderProducts 
 columns:
 OId (Foreign Key referencing Orders)
 PId (Foreign Key referencing Products)
 -Many-to-many ↔ Products (via OrderProducts)
- Many-to-many ↔ Orders (via OrderProducts)
 
 -EFMigrationsHistory
A system table created by Entity Framework to track applied migrations.
Columns:
MigrationId: unique identifier of the migration.

ProductVersion: EF version used.
 this System  have Models:
 - User: Represents a user of the e-commerce platform, including customers and sellers.
	- 
