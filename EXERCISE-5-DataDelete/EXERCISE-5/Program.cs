using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EXERCISE_5.Models;

namespace EFDeleteCustomer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new DbExerciseContext())
            {
                // Customer ID to delete
                string customerIdToDelete = "BLONP";

                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        // We search for the customer by their ID (CustomerId)
                        var customerToDelete = dbContext.Customers
                            .Include(c => c.Orders)
                            .ThenInclude(o => o.OrderDetails)
                            .FirstOrDefault(c => c.CustomerId == customerIdToDelete);

                        if (customerToDelete != null)
                        {
                            // We delete the order data associated with the customer.
                            var orderDetailsToDelete = customerToDelete.Orders.SelectMany(o => o.OrderDetails).ToList();
                            dbContext.OrderDetails.RemoveRange(orderDetailsToDelete);

                            // We eliminate customer orders
                            var ordersToDelete = customerToDelete.Orders.ToList();
                            dbContext.Orders.RemoveRange(ordersToDelete);

                            // We eliminate the client
                            dbContext.Customers.Remove(customerToDelete);

                            // Guardamos los cambios en la base de datos
                            dbContext.SaveChanges();

                            // We confirm the transaction
                            transaction.Commit();

                            Console.WriteLine($"The client {customerIdToDelete} and its related data were successfully deleted.");
                        }
                        else
                        {
                            Console.WriteLine($"No client was found with the ID. {customerIdToDelete}.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting client: {ex.Message}");
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}

// Secure deletion of client 'BLONP' and its related data following best practices to ensure data integrity