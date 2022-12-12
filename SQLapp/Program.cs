using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace SQLapp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);


            var  repo = new DapperDepartmentRepository(conn);
            var departments = repo.GetAllDepartments();
            Console.WriteLine("*************Best Buy Departments**************");
            foreach (var item in departments)
            {
                Console.WriteLine($"{item.DepartmentID}    {item.Name}");
            }

        }
    }
}