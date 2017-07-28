# Building the Bangazon Platform API



 .NET Web API  built that makes each resource in the Bangazon ERD available to application developers throughout the entire company.


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
* Open .zshrc file
```
1. vim ~/.zshrc
2. enter i to insert

Add variable
3. export BANGAZON_DB="/Users/YourNameHere/workspace/csharp/BangazonAPI/Bangazon.db"
4. press esc
5. :x to Save and exit vim
to refresh file 
6. source ~/.zshrc
```

To Run Code :
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
Example:
{
        "ProductTypeId": 4,
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


## To Test that only Bangazonians can make requests to the Web Api
* Create a seperate Project With Ajax request to localhost:5000/api/value
```
Use this code :
<html><title></title> <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script> <script>
$.ajax({
    url: "http://localhost:5000/api/ValueYouWishToTest",
                   method: "GET"
}).done(function(data){
    console.log("Data", data);
});

</script>
</html>
```


## Create a sudo alias for localhost
### For Mac users
Open your terminal and add code :
```
1. sudo vim /etc/hosts

Will be prompted to enter your Password
Enter i to insert

Add an alias for localhost IP Address
2. 127.0.0.1    www.Bangazon.com

Save changes by entering 
3. :x

In your Bangazon Project start your server with port 5000
4. dotnet run

In the terminal for AJAX request project file. Start a sudo server with the Browser default Port of 80
5. sudo http-server -p 80
```
Open your browser with www.Bangazon.com
Data should be displayed in the console.


### For Windows users Find Hosts File
```
1. C:\Windows\System32\drivers\etc\hosts

Open NotePad as Administrator and add 
2. 127.0.0.1    www.Bangazon.com

save changes by entering 
3. :x

In your Bangazon Project start your server with port 5000
4. dotnet run

In the terminal for AJAX request project file. Start a sudo server with the Browser default Port of 80
5. sudo http-server -p 80
```
Open your browser with www.Bangazon.com
Data should be displayed in the console.


## This application consists of:

*   ASP.NET Core MVC
*   [Bower](https://go.microsoft.com/fwlink/?LinkId=518004) for managing client-side libraries
*   Theming using [Bootstrap](https://go.microsoft.com/fwlink/?LinkID=398939)
*   Asp.Net Core Web API

