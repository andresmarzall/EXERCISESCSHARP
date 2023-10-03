using System;
using System.Linq;
using ConsoleTables;
using EXERCISES_1.Models;

namespace EXERCISES_1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter the start date (yyyy-MM-dd):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    Console.WriteLine("Please enter the end date (yyyy-MM-dd):");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                    {
                        using (var context = new DbExerciseContext())
                        {
                            // Query to retrieve orders within the specified date range
                            var query = from order in context.Orders
                                        where order.OrderDate >= startDate && order.OrderDate <= endDate
                                        join customer in context.Customers on order.CustomerId equals customer.CustomerId
                                        join employee in context.Employees on order.EmployeeId equals employee.EmployeeId
                                        orderby order.OrderId
                                        select new
                                        {
                                            OrderID = order.OrderId,
                                            CustomerID = customer.CustomerId,
                                            CustomerName = customer.CompanyName,
                                            EmployeeID = employee.EmployeeId,
                                            EmployeeName = employee.FirstName + " " + employee.LastName,
                                            OrderDate = order.OrderDate,
                                            RequiredDate = order.RequiredDate,
                                            ShippedDate = order.ShippedDate
                                        };

                            var results = query.ToList();

                            Console.WriteLine("List of orders within the selected date range:");

                            // Create a console table with headers
                            var table = new ConsoleTable("Order ID", "Customer ID", "Customer", "Employee ID", "Employee", "Order Date", "Required Date", "Shipped Date");

                            foreach (var result in results)
                            {
                                // Add each data row to the table
                                table.AddRow(result.OrderID, result.CustomerID, result.CustomerName,
                                             result.EmployeeID, result.EmployeeName,
                                             result.OrderDate.ToString(),
                                             result.RequiredDate.ToString(),
                                             result.ShippedDate?.ToString("yyyy-MM-dd") ?? "N/A");
                            }

                            // Display the table in the console
                            table.Write();
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid end date. Please enter a date in the correct format (yyyy-MM-dd).");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid start date. Please enter a date in the correct format (yyyy-MM-dd).");
                }
            }
        }
    }
}

// Purpose Summary:
// The purpose of this program is to allow the user to input a date range (start date and end date) and retrieve a list of orders that fall within that date range from a database. 
// Then, this information is presented to the user in the form of a table in the console for visualization. 
// This program is useful because it provides a convenient way to filter and display specific data from the database based on a date criterion. 
// This data can be used later for applying CRUD (Create, Read, Update, Delete) operations in the database, making the management and manipulation of order records efficient.
