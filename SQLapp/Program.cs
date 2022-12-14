using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using static Bestbuy.IDepartmentRepository;
using Org.BouncyCastle.Asn1.X509;
using Microsoft.VisualBasic;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

namespace Bestbuy
// Note: actual namespace depends on the project name.
{
    internal class Program
    {

        static IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        static string? connString = config.GetConnectionString("DefaultConnection");
        static IDbConnection conn = new MySqlConnection(connString);



        static void Main(string[] args)
        {


           
            do // do while to repaet the program over 
            {
                // User selections with 5 options to select 

                Console.WriteLine("********Best Buy Inventory********");
                var tm = DateAndTime.Now.ToString();
                Console.WriteLine(tm);
                Console.WriteLine("Please Select :");
                Console.WriteLine("1. Create Product");
                Console.WriteLine("2. Update Product");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. Product List");
                Console.WriteLine("5. Exit ");



                var userinput = int.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Please wait loadding!");


                for (int i = 0; i < 6; i++) // loading 
                {
                    Console.Write(".");
                    Thread.Sleep(1000);
                }


                if (userinput == 1) // if statement to validate the user input 
                {
                    Console.Clear();
                    CreateAndListProducts();

                }


                if (userinput == 2)

                {
                    Console.Clear();
                    UpdateProductName();

                }



                if (userinput == 3)

                {
                    Console.Clear();
                    DeleteProduct();

                }

                if (userinput == 4)
                {
                    Console.Clear();
                    ListProducts();
                }

                if (userinput == 5)

                {
                    Environment.Exit(0);
                }
            
                else
                {

                    Console.WriteLine("please select a valid option from the Start Manu!");

                }

                

            } while (true);

            
        }
        public static void DeleteProduct()
        {
            //ProductRepository instance - so we can call our dapper methods
            var prodRepo = new DapperProductRepository(conn);

            //User interaction
            Console.WriteLine($"Please enter the productID of the product you would like to delete:");
            var productID = Convert.ToInt32(Console.ReadLine());

            //Call the Dapper method
            prodRepo.DeleteProduct(productID);
          
          
        }
      
        

        
        public static void UpdateProductName()
        {
            var prodRepo = new DapperProductRepository(conn);

            Console.WriteLine($"What is the productID of the product you would like to update?");
            var productID = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine($"What is the new name you would like for the product with an id of {productID}?");
            var updatedName = Console.ReadLine();

            prodRepo.UpdateProductName(productID, updatedName);
        }



        public static void CreateAndListProducts()
        {
            //created instance so we can call methods that hit the database
            var prodRepo =  new DapperProductRepository(conn);

            Console.WriteLine($"What is the new product name?");
            var prodName = Console.ReadLine();

            Console.WriteLine($"What is the new product's price?");
            var price = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"What is the new product's category id?");
            var categoryID = Convert.ToInt32(Console.ReadLine());

            prodRepo.CreateProduct(prodName, price, categoryID);


            var products = prodRepo.GetAllProducts();

            //print each product from the products collection to the console
            foreach (var product in products)

                Console.WriteLine($"{product.CategoryID} {product.Name}");
        }



        public static void ListProducts()
        {
            var prodRepo = new DapperProductRepository(conn);
            var products = prodRepo.GetAllProducts();

            //print each product from the products collection to the console
            foreach (var product in products)
            {
                Console.WriteLine($"{product.CategoryID} {product.Name}");
            }
        }


        public static void ListDepartments()
        {
            var repo = new DapperDepartmentRepository(conn);

            var departments = repo.GetAllDepartments();

            foreach (var item in departments)
            {
                Console.WriteLine($"{item.DepartmentID} {item.Name}");
            }
        }


        public static void DepartmentUpdate()
        {
            var repo = new DapperDepartmentRepository(conn);

            Console.WriteLine($"Would you like to update a department? yes or no");

            if (Console.ReadLine().ToUpper() == "YES")
            {
                Console.WriteLine($"What is the ID of the Department you would like to update?");

                var id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"What would you like to change the name of the department to?");

                var newName = Console.ReadLine();

                repo.UpdateDepartment(id, newName);

                var depts = repo.GetAllDepartments();

                foreach (var item in depts)
                {
                    Console.WriteLine($"{item.DepartmentID} {item.Name}");
                }
            }















        }
    }
}