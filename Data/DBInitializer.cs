using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using  BangazonAPI.Models;
using System.Threading.Tasks;
namespace BangazonAPI.Data 
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BangazonContext(serviceProvider.GetRequiredService<DbContextOptions<BangazonContext>>()))
            {   
                //This if statement will check the database and if it is empty it will populate it with some dummy data 
                // This was authored by Azim:
                if(context.Customer.Any())
                {
                    return; 
                }
                //The following statements will be automatically added to the database and create tables 
                // This was authored by Azim:
                var customer = new Customer[]
                {
                    new Customer {
                        FirstName = "Gucci",
                        LastName = "Mane"
                    }, 
                    new Customer{
                        FirstName = "Riff",
                        LastName= "Raff"
                    },
                    new Customer{
                        FirstName = "Wacka Flocka",
                        LastName = "Flame"
                    }
                };
                foreach (Customer i in customer)
                {
                    context.Customer.Add(i);
                }
                context.SaveChanges();
                var productsType =  new ProductType[]
                {
                    new ProductType{
                        Name = "Sports"
                    },
                    new ProductType{
                        Name = "Toys"
                    }  
                };
                foreach(ProductType i in productsType)
                {
                    context.ProductType.Add(i);
                }  
                context.SaveChanges();
        
                var products = new Product[]
                {
                    new Product{
                        ProductTypeId = productsType.Single(s => s.Name =="Sports").ProductTypeId,
                        Price = 50.00, 
                        Title = "Baseball Glove", 
                        Description = "This glove will help you catch baseballs.",
                        CustomerId = customer.Single(s => s.FirstName == "Wacka Flocka").CustomerId   
                    },
                    new Product{
                        ProductTypeId = productsType.Single(s => s.Name =="Sports").ProductTypeId,
                        Price = 30.00, 
                        Title = "Basketball", 
                        Description = "Learn to dunk!",
                        CustomerId = customer.Single(s => s.FirstName == "Riff").CustomerId    
                    },
                    new Product{
                        ProductTypeId = productsType.Single(s => s.Name =="Toys").ProductTypeId,
                        Price = 10.00, 
                        Title = "Teddy Bear", 
                        Description = "Get it for the kids.",
                        CustomerId = customer.Single(s => s.FirstName == "Wacka Flocka").CustomerId   
                    },
                    new Product{
                        ProductTypeId = productsType.Single(s => s.Name =="Toys").ProductTypeId,
                        Price = 5.00, 
                        Title = "Coloring Book", 
                        Description = "Stay in the lines. Or don't. Its up to you.",
                        CustomerId = customer.Single(s => s.FirstName == "Gucci").CustomerId 
                    }
                };  
                foreach(Product i in products)
                {
                    context.Product.Add(i);
                }
                context.SaveChanges();
        
                var paymentTypes = new PaymentType[]
                {
                    new PaymentType{
                        CustomerId = customer.Single(s => s.FirstName == "Gucci").CustomerId, 
                        AccountNumber = 2, 
                        Type = "American Express",
                    },
                    new PaymentType{
                        CustomerId = customer.Single(s => s.FirstName == "Riff").CustomerId,
                        AccountNumber = 3, 
                        Type = "Visa",
                    }, 
                    new PaymentType{
                        CustomerId = customer.Single(s => s.FirstName == "Wacka Flocka").CustomerId, 
                        AccountNumber = 4,
                        Type = "Mastercard", 
                    }
                }; 
                foreach(PaymentType i in paymentTypes)
                {
                    context.PaymentType.Add(i);
                }
                context.SaveChanges();
                var orders = new Order[]
                {
                    new Order{
                        CustomerId = customer.Single(s => s.FirstName == "Wacka Flocka").CustomerId,
                        PaymentTypeId = paymentTypes.Single(s => s.Type == "American Express").PaymentTypeId, 
                    },
                    new Order{
                        CustomerId = customer.Single(s => s.FirstName == "Riff").CustomerId,
                        PaymentTypeId = paymentTypes.Single(s => s.Type == "Visa").PaymentTypeId, 
                    },
                    new Order{
                        CustomerId = customer.Single(s => s.FirstName == "Gucci").CustomerId,
                        PaymentTypeId = paymentTypes.Single(s => s.Type == "Mastercard").PaymentTypeId, 
                    }
                };
                foreach(Order i in orders)
                {
                    context.Order.Add(i);
                }
                context.SaveChanges();
                var orderProduct = new OrderProduct[]
                {
                    new OrderProduct{
                        OrderId = 1,
                        ProductId = products.Single(s => s.Title == "Baseball Glove").ProductId
                    },
                    new OrderProduct{
                        OrderId = 1,
                        ProductId = products.Single(s => s.Title == "Teddy Bear").ProductId
                    },
                    new OrderProduct{
                        OrderId = 2,
                        ProductId = products.Single(s => s.Title == "Basketball").ProductId
                    },
                    new OrderProduct{
                        OrderId = 3,
                        ProductId = products.Single(s => s.Title == "Coloring Book").ProductId
                    } 
                };
                foreach(OrderProduct i in orderProduct)
                {
                    context.OrderProduct.Add(i);
                }
                context.SaveChanges();
                var departments = new Department[]
                {
                    new Department{
                        Name = "Sporting Goods",
                        Budget = 1500
                    },
                    new Department{
                        Name = "Toy Department",
                        Budget = 1250
                    }
                }; 
                foreach(Department i in departments)
                {
                    context.Department.Add(i);
                }
                context.SaveChanges();
                var employees = new Employee[]
                {
                    new Employee{
                        FirstName = "2Pac",
                        LastName = "Shakur", 
                        DepartmentId = departments.Single(s => s.Name == "Sporting Goods").DepartmentId,
                        IsSupervisor = 0,
                    },
                    new Employee{
                        FirstName = "Kendric",
                        LastName = "Lamar",
                        DepartmentId = departments.Single(s => s.Name == "Sporting Goods").DepartmentId,
                        IsSupervisor = 1,
                    },
                    new Employee{
                        FirstName = "Drake",
                        LastName = "Drake",
                        DepartmentId = departments.Single(s => s.Name == "Toy Department").DepartmentId,
                        IsSupervisor = 0,
                    }
                }; 
                foreach(Employee i in employees)
                {
                    context.Employee.Add(i);
                }
                context.SaveChanges();
                var computers = new Computer[]
                {
                    new Computer{
                        DatePurchased = new DateTime(2017, 08, 01),
                    },
                    new Computer{
                        DatePurchased = new DateTime(2017, 08, 03),
                    },
                    new Computer{
                        DatePurchased = new DateTime(2017, 08, 06),
                    }   
                }; 
                foreach(Computer i in computers)
                {
                    context.Computer.Add(i);
                }
                context.SaveChanges();
                var trainingProgram = new TrainingProgram[]
                {
                    new TrainingProgram{
                        StartDate = new DateTime(2017, 08, 01),
                        EndDate = new DateTime(2017, 08, 07),
                        Name = "Computer Training",
                        MaxCapacity = 25
                    },
                    new TrainingProgram{
                        StartDate = new DateTime(2017, 09, 01),
                        EndDate = new DateTime(2017, 09, 07),
                        Name = "Up Selling", 
                        MaxCapacity = 40
                    },
                    new TrainingProgram{
                        StartDate = new DateTime(2017, 09, 10),
                        EndDate = new DateTime(2017, 09, 12),
                        Name = "Sensitivity Traning",
                        MaxCapacity = 6
                    }
                };
                foreach(TrainingProgram i in trainingProgram)
                {
                    context.TrainingProgram.Add(i);
                }
                context.SaveChanges();
                var employeeTrainings = new EmployeeTraining[]
                {
                    new EmployeeTraining{
                        EmployeeId = employees.Single(s => s.FirstName == "Drake").EmployeeId,
                        TrainingProgramId = trainingProgram.Single(s => s.Name ==  "Computer Training").TrainingProgramId
                    },
                        new EmployeeTraining{
                        EmployeeId = employees.Single(s => s.FirstName == "2Pac").EmployeeId,
                        TrainingProgramId = trainingProgram.Single(s => s.Name ==  "Up Selling").TrainingProgramId
                    },
                        new EmployeeTraining{
                        EmployeeId = employees.Single(s => s.FirstName == "Kendric").EmployeeId,
                        TrainingProgramId = trainingProgram.Single(s => s.Name == "Sensitivity Traning").TrainingProgramId
                    }
                }; 
                foreach(EmployeeTraining i in employeeTrainings)
                {
                    context.EmployeeTraining.Add(i);
                }
                context.SaveChanges();
                var employeeComputers = new EmployeeComputer[]
                {
                    new EmployeeComputer{
                        ComputerId = computers.Single(s => s.DatePurchased == new DateTime(2017, 08, 01)).ComputerId,
                        EmployeeId = employees.Single(s => s.FirstName == "Drake").EmployeeId
                    },
                        new EmployeeComputer{
                        ComputerId = computers.Single(s => s.DatePurchased == new DateTime(2017, 08, 03)).ComputerId,
                        EmployeeId = employees.Single(s => s.FirstName == "2Pac").EmployeeId
                    },
                        new EmployeeComputer{
                        ComputerId = computers.Single(s => s.DatePurchased == new DateTime(2017, 08, 06)).ComputerId,
                        EmployeeId = employees.Single(s => s.FirstName == "Kendric").EmployeeId
                    }
                }; 
                foreach(EmployeeComputer i in employeeComputers)
                {
                    context.EmployeeComputer.Add(i);
                }
                context.SaveChanges();
            }
        }
    }
}