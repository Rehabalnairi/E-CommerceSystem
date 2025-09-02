"# E-CommerceSystem-" 
this system is a comprehensive e-commerce platform designed to facilitate online shopping and streamline the buying and selling process. 
It offers a user-friendly interface for customers to browse products, add items to their cart, 
and complete purchases securely. 
The system also includes features for sellers to manage their inventory,
process orders, and track sales performance.

Database Name: ECommerceDB
name of the project: E-CommerceSystem

-----------------------------------------------
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

-------------------------------------------------------
This project is contain 5 Repository for each class and each Repository have Interface:
1- OrderRepo class that implements IOrderRepo
first use ApplicationDbContext which manages the database connection and tables
Provides CRUD operations in this project (GetAllOrders, GetOrderById, GetOrderByUserId, AddOrder, DeleteOrder, UpdateOrder)

2- ProductRepo class that implements IProductRepo
use ApplicationDbContext which manages the database connection and tables
Provides CRUD operations in this project (GetAllProducts, GetProductById, AddProduct, UpdateProduct, GetProductByName)

3- ReviewRepo class that implements IReviewRepo
use ApplicationDbContext which manages the database connection and tables
Provides CRUD operations in this project (GetAllReviews, GetReviewById, GetReviewsByProductIdAndUserId, AddReview, UpdateReview, DeleteReview, GetReviewByProductId)

4- UserRepo class that implements IUserRepo
use ApplicationDbContext which manages the database connection and tables
Provides CRUD operations in this project (GetAllUsers, GetUserById, AddUser, UpdateUser, DeleteUser, GetUSer)

5- OrderProductsRepo class that implements IOrderProductsRepo
use ApplicationDbContext which manages the database connection and tables
Provides CRUD operations in this project (AddOrderProducts, GetAllOrders, GetOrdersByOrderId)

------------------------------------------------------------

Services:
UserService
Methods:
-AddUser(User user)
Adds a new user to the database via _userRepo.

-DeleteUser(int uid)
Deletes a user by ID.
Throws KeyNotFoundException if the user does not exist.

-GetAllUsers()
Retrieves a list of all users.

-GetUSer(string email, string password)
Authenticates a user by email and password.
Throws UnauthorizedAccessException if credentials are invalid.
(Note: Method name has a small typo; should be GetUser.)

-GetUserById(int uid)
Retrieves a user by ID.
Throws KeyNotFoundException if not found.

-UpdateUser(User user)
Updates an existing user.
Throws KeyNotFoundException if the user does not exist.

**********************************
1-OrderService
Methods:
-GetAllOrders(int uid):
Retrieves all orders of a given user.
Collects related OrderProducts entries for each order.
Throws exception if user has no orders.

-GetOrderById(int oid, int uid):
Fetches a specific order by ID for a given user.
Builds a list of OrdersOutputOTD objects containing:
Product name
Quantity
Order date
Total amount per item

-GetOrderByUserId(int uid)
Retrieves all orders for a specific user.
Throws exception if none found.

-DeleteOrder(int oid):
Deletes an order by ID.
Throws error if order not found.

-AddOrder(Order order) & UpdateOrder(Order order):
Adds a new order or updates an existing one in the repository.

-PlaceOrder(List<OrderItemDTO> items, int uid)
Main business logic for placing orders:
Validates products exist and stock is sufficient.
Creates a new Order record for the user.
For each item:
Deducts stock.
  Creates OrderProducts link entry.
  Updates total price.
  the Order.TotalAmount.
************************************
-ProductService
Methods:
-GetAllProducts(int pageNumber, int pageSize, string? name = null, decimal? minPrice = null, decimal? maxPrice = null)
Retrieves products with:
Optional filters (by name, min price, max price).
Pagination (page number & page size).
Uses LINQ filtering and Skip/Take for pagination.

-GetProductById(int pid)
Retrieves a product by its ID.
Throws KeyNotFoundException if not found.

-AddProduct(Product product)
Adds a new product to the repository.

-UpdateProduct(Product product)
Checks if the product exists.
If not, throws KeyNotFoundException.
If yes, updates it in the repository.

-GetProductByName(string productName)
Retrieves a product by its name.
Throws KeyNotFoundException if not found.

******************************************

-ReviewService
Methods:
-GetAllReviews(int pageNumber, int pageSize, int pid)
Returns a paginated list of reviews for a product.

-GetReviewsByProductIdAndUserId(int pid, int uid)
Retrieves a review for a product by a specific user.

-GetReviewById(int rid)
Retrieves a review by its ID. Throws an exception if not found.

-GetReviewByProductId(int pid)
Retrieves all reviews for a specific product.

-AddReview(int uid, int pid, ReviewDTO reviewDTO)
Validates that the user has purchased the product.
Checks if the user already reviewed the product.
Adds a new review and recalculates the product’s overall rating.

-UpdateReview(int rid, ReviewDTO reviewDTO)
Updates an existing review.
Updates the product’s overall rating.

-DeleteReview(int rid)
Deletes a review.

-Updates the product’s overall rating.
RecalculateProductRating(int pid)
Calculates the average rating of all reviews for a product.
Updates the product’s OverallRating property.

Note :all this servers with interfaces in IServices .
*****************************************




