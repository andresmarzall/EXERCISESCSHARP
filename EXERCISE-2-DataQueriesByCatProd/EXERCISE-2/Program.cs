using System;
using System.Linq;
using ConsoleTables;
using EXERCISE_2.Models;

namespace ProductCategorySummary
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a database context to work with the DbExercise database
            using (var context = new DbExerciseContext())
            {
                // Query the database to get a summary of product categories and their total stock
                var query = from c in context.Categories
                            join p in context.Products on c.CategoryId equals p.CategoryId
                            group p by new
                            {
                                c.CategoryId,
                                c.CategoryName
                            } into categoryGroup
                            select new
                            {
                                CategoryName = categoryGroup.Key.CategoryName,
                                TotalStock = categoryGroup.Sum(p => p.UnitsInStock)
                            };

                // Execute the query and retrieve the results as a list
                var results = query.ToList();

                // Print a header for the category stock summary
                Console.WriteLine("Category Stock Summary:");

                // Create a console table to display the results with headers
                var table = new ConsoleTable("Category", "Total in Stock");

                // Iterate through the results and add each category and total stock to the table
                foreach (var result in results)
                {
                    table.AddRow(result.CategoryName, result.TotalStock);
                }

                // Display the table in the console
                table.Write();
            }
        }
    }
}


//Purpose Summary:
//The purpose of this program is to retrieve a summary of product categories and their total
//stock from a database (DbExercise) using Entity Framework Core.It then presents this summary
//in a tabular format in the console. This program is useful for gaining insights into the
//stock levels of different product categories, which can be valuable for inventory management and analysis.