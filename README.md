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
1. ProductController
1. ProductTypeController
1. CustomerController
1. OrderController
1. PaymentTypeController
1. EmployeeController
1. ComputerController
1. TrainingProgramController
1. DepartmentController

# How to Test 
### First Clone the Repository 
https://github.com/Abcrete/BangazonAPI

* To Run Code :
```
1. dotnet restore

* Migration
This will read the Models and build the instructions necessary to create corresponding tables in the database based off of your annotations and foreign key relationships.

1.  dotnet ef migrations add InitialDBCreation

* Update Database
Execute the instructions created by the Migration and Build the Database

1. dotnet ef database update
```

* Use PostMan to Post Values to Database
ex.


## To ensure only Bangazonians can make requests to the Web Api
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


## Create a sudo alias for localhost:8080
### For IOS users
Open your terminal and add code :
```
1. sudo vim /etc/hosts

will be prompted to enter your Password
enter i to insert

add an alias for localhost IP Address
1. 127.0.0.1    www.Bangazon.com

save changes by entering 
:x

### Start sudo server with the Browser default Port of 80
sudo http-server -p 80
```
Then run localhost:5000 server
Open your browser with www.Bangazon.com
Data should be displayed in the console.


### For Windows users Find Hosts File

```
1. C:\Windows\System32\drivers\etc\hosts

Open NotePad as Administrator and add 
1. 127.0.0.1    www.Bangazon.com

save changes by entering 
:x

### Start sudo server with the Browser default Port of 80
sudo http-server -p 80
```
Then run localhost:5000 server
Open your browser with www.Bangazon.com
Data should be displayed in the console.


## This application consists of:

*   ASP.NET Core MVC
*   [Bower](https://go.microsoft.com/fwlink/?LinkId=518004) for managing client-side libraries
*   Theming using [Bootstrap](https://go.microsoft.com/fwlink/?LinkID=398939)
*   Asp.Net Core Web API

