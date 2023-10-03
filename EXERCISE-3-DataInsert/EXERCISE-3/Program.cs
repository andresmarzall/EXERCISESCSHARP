using System;
using System.Collections.Generic;
using System.Linq; 
using EXERCISE_3.Models;
using Microsoft.EntityFrameworkCore;

namespace EFInsert
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new DbExerciseContext())
            {
                // Creating new clients to add to the database
                var newCustomers = new List<Customer>
                {
                    new Customer
                    {
                        CustomerId = "NTA",
                        CompanyName = "NewTecnologi Adictive",
                        ContactName = "Javi Summers",
                        ContactTitle = "CTO",
                        Address = "6565 Innovation Fames",
                        City = "Silicon Hills",
                        Region = "CA",
                        PostalCode = "06321",
                        Country = "USA",
                        Phone = "+1 934-954-3210",
                        Fax = "+1 111-954-3210"
                    },
                    new Customer
                    {
                        CustomerId = "GID",
                        CompanyName = "Global Industry",
                        ContactName = "Emily Brown",
                        ContactTitle = "CEO",
                        Address = "7922 Main Boulevard",
                        City = "Big City",
                        Region = "NY",
                        PostalCode = "55145",
                        Country = "USA",
                        Phone = "+1 666-456-7890",
                        Fax = "+1 125-456-7891"
                    },
                    new Customer
                    {
                        CustomerId = "ADG",
                        CompanyName = "Advance Dynamics GMC.",
                        ContactName = "Michael Jordan",
                        ContactTitle = "VP Sales",
                        Address = "9333 Tech Avenue",
                        City = "Los Angeles",
                        Region = "CA",
                        PostalCode = "31110",
                        Country = "USA",
                        Phone = "+1 666-555-6555",
                        Fax = "+1 666-555-6556"
                    }
                };

                foreach (var customer in newCustomers)
                {
                    // We check if the customer already exists in the database using CustomerId
                    var existingCustomer = dbContext.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);

                    if (existingCustomer == null)
                    {
                        // If the client does not exist it will be added.
                        dbContext.Customers.Add(customer);
                    }
                    else
                    {
                        Console.WriteLine($"The customer with CustomerId already exists in the database.{customer.CustomerId} will not be added ");
                    }
                }

                dbContext.SaveChanges();
            }

            Console.WriteLine("Data added successfully.");
        }
    }
}


// The purpose of the code is to create new customers and add them to the database, making
// sure not to duplicate customers with the same "CustomerId" and providing a message if a customer already exists.