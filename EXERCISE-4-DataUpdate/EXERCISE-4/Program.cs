using System;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using EXERCISE_4.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EFUpdateCustomer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new DbExerciseContext())
            {               // transaction begins
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    // Customer ID to update (updated by CustomerId)
                    string customerIdToUpdate = "ADG"; // Change the ID of the client you want to update.

                    try
                    {
                        //Search the customer by their ID (CustomerId)
                        var customerToUpdate = dbContext.Customers.FirstOrDefault(c => c.CustomerId == customerIdToUpdate);

                        if (customerToUpdate != null)
                        {
                            // Update ADG Customer Phone Number
                            customerToUpdate.Phone = "+1 111-222-3333"; 
                            customerToUpdate.Fax = "+1 111-222-3333";
                            dbContext.SaveChanges();

                            // Confirm the transaction if everything was done correctly
                            transaction.Commit();

                            Console.WriteLine($"Customer phone and fax number {customerIdToUpdate} successfully updated.");
                        }
                        else
                        {
                            Console.WriteLine($"No client was found with the ID. {customerIdToUpdate}.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error updating client: {ex.Message}");

                        // In case of error, reverse the transaction.
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
// Using Transactions: A transaction is used to ensure that changes to the database are made safely.
// If the update is successful, the transaction is confirmed; In case of error, a rollback is performed
// to maintain the integrity of the database.


                    // Result:
                        
                    // Before:
                    //CustomerId = "ADG",                        
                    //Phone = "+1 666-555-6555",
                    //Fax = "+1 666-555-6556"

                    // After:
                    //CustomerId = "ADG",
                    //Phone = "+1 111-222-3333",
                    //Fax = "+1 111-222-3333"

