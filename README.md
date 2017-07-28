# Building the Bangazon Platform API



 .NET Web API app that makes each resource in the Bangazon ERD available to application developers throughout the entire company.


### Model Classes Used to build DataBase structure, Primary and Foreign Keys

1. Product
1. ProductType
1. Customer
1. Order
1. OrderProduct
1. PaymentType
1. Employee
1. Computer
1. TrainingProgram
1. Department

### Controllers That Handle Get, Post, Put and Delete operations
1. ProductController            operations: Get(), Post(), Put(), Delete()
1. ProductTypeController        operations: Get(), Post(), Put(), Delete()
1. CustomerController           operations: Get(), Post(), Put()
1. OrderController              operations: Get(), Post(), Put(), Delete()
1. PaymentTypeController        operations: Get(), Post(), Put(), Delete()
1. EmployeeController           operations: Get(), Post(), Put()
1. ComputerController           operations: Get(), Post(), Put(), Delete()
1. TrainingProgramController    operations: Get(), Post(), Put(), Delete()
1. DepartmentController         operations: Get(), Post(), Put()

# How to Test 
### First Clone the Repository 
https://github.com/Abcrete/BangazonAPI

## Create an Environment Variable to set file path for Database
* On Mac open and edit your .zshrc file in terminal
```
1. vim ~/.zshrc
2. enter i to insert

Add variable
3. export BANGAZON_DB="/Enter the full filepath to the directory where you cloned the repo/Bangazon.db"
4. press esc
5. :x to Save and exit vim
to refresh file type the following command in terminal and hit enter:
6. source ~/.zshrc
```

Navigate to your project directory in terminal and run the following commands:
```
1. dotnet restore

* Migration
This will read the Models and build the instructions necessary to create corresponding tables in the database based off of your annotations and foreign key relationships.

2.  dotnet ef migrations add InitialDBCreation

* Update Database
Execute the instructions created by the Migration and Build the Database

3. dotnet ef database update
```

## Use PostMan to Post Values to Database
Make sure to have Postman installed: https://www.getpostman.com/
```
Example for Product:
{
	"Price": 11.00,
	"Title": "Pizza",
	"Description": "A giant slice of delicious pizza from Joey's.",
	"CustomerId": 1,
	"ProductAmount": 1,
}

To view all products:
Select GET
http://localhost:5000/api/product

To view a single product:
Select GET
http://localhost:5000/api/product/{id}

To edit a single product
Select PUT
http://localhost:5000/api/product/{id}

To delete a single product
Select DELETE
http://localhost:5000/api/product/{id}
```


## This application consists of:

*   ASP.NET Core MVC
*   [Bower](https://go.microsoft.com/fwlink/?LinkId=518004) for managing client-side libraries
*   Theming using [Bootstrap](https://go.microsoft.com/fwlink/?LinkID=398939)
*   Asp.Net Core Web API

