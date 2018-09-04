using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DapperConsole.Models;

namespace DapperConsole
{
    class Program
    {
        

        static void Main(string[] args)
        {
            IDbConnection db = new SqlConnection(
                ConfigurationManager.ConnectionStrings["DapperConsole"].ConnectionString);

            string SqlString = "SELECT TOP 100 [CustomerID],[CustomerFirstName],[CustomerLastName],[IsActive] FROM [Customer]";
            var ourCustomers = (List<Customer>)db.Query<Customer>(SqlString);

            foreach (var customer in ourCustomers)
            {
                Console.WriteLine(new string('*', 20));
                Console.WriteLine("\nCustomer ID: " + customer.CustomerID.ToString());
                Console.WriteLine("First Name: " + customer.CustomerFirstName);
                Console.WriteLine("Last Name: " + customer.CustomerLastName);
                Console.WriteLine("Is Active? " + customer.IsActive + "\n");
                Console.WriteLine(new string('*', 20));
            }
            Console.ReadKey();
        }
    }
}
