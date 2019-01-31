using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//1.	Show all info about the employee with ID 8.
//5.	Calculate the count of employees from London.

//17.	Show the count of orders made by each customer from France.
//13.	Show first and last names of the employees who used to serve orders shipped to Madrid.

//24.   Show the list of french customers’ names who used to order french products.
//30.	Show the list of cities where employees and customers are from and where orders have been made to. Duplicates should be eliminated.


//31.	Insert 5 new records into Employees table.
//Fill in the following  fields: LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes. The Notes field should contain your own name.
//33.	Change the City field in one of your records using the UPDATE statement.
namespace ADO_v1
{
    class Program
    {
        static void MakeSelection(string connectionString, string commandSQL)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(commandSQL, connection);
                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine(reader.GetName(i) + "\t\t" + reader.GetValue(i));
                        }

                        Console.WriteLine("-----------------------------------------------------");
                    }
                }
                reader.Close();
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True;";


            string showAllAbout8 = "SELECT * FROM Employees WHERE Employees.EmployeeID=8";
            string showCountEmplLondon = "SELECT COUNT(Employees.EmployeeID) AS [CountLondon] FROM Employees WHERE Employees.City = 'London'";

            string showCountOrdersFromFrance = "SELECT COUNT(Orders.OrderID) AS [Count], Customers.ContactName FROM (Customers JOIN Orders ON Customers.CustomerID = Orders.CustomerID) " +
                "WHERE Customers.Country = 'France' GROUP BY Customers.ContactName";
            string showEmployeeOrderToMadrid = "SELECT Employees.FirstName, Employees.LastName FROM Orders JOIN Employees ON Employees.EmployeeID = Orders.EmployeeID " +
                "WHERE Orders.ShipCity = 'Madrid'";

            string showWhoOrderFromFrance = "SELECT DISTINCT Customers.ContactName FROM Customers " +
                "JOIN Orders ON Customers.CustomerID = Orders.CustomerID " +
                "JOIN [Order Details] ON Orders.OrderID = [Order Details].OrderID " +
                "JOIN Products ON [Order Details].ProductID = Products.ProductID " +
                "JOIN Suppliers ON Suppliers.SupplierID = Products.SupplierID " +
                "WHERE (Suppliers.Country = 'France' AND Customers.Country = 'France')";
            string showCitiesWhereFrom = "SELECT DISTINCT Customers.City, Employees.City, Orders.ShipCity FROM Customers " +
                "JOIN Orders ON Customers.CustomerID = Orders.CustomerID " +
                "JOIN Employees ON Orders.EmployeeID = Employees.EmployeeID";

            string insertIntoEmployee = "INSERT INTO Employees(LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes) " +
                "VALUES ('Kostelna', 'Oksana', 1997-12-12, 2017-11-28, 'Univercity stt.', 'Lviv', 'Ukraine', 'Oksana');" +
                "INSERT INTO Employees(LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes) " +
                "VALUES ('Loka', 'Vena', 1997-9-1, 2017-11-28, 'Univercity stt.', 'Lviv', 'Ukraine', 'Oksana');" +
                "INSERT INTO Employees(LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes) " +
                "VALUES ('Illichova', 'Tanya', 1997-06-25, 2017-5-28, 'Univercity stt.', 'Lviv', 'Ukraine', 'Oksana');" +
                "INSERT INTO Employees(LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes) " +
                "VALUES ('Zubal', 'Bohdan', 1997-11-13, 2017-5-1, 'Univercity stt.', 'Lviv', 'Ukraine', 'Oksana');" +
                "INSERT INTO Employees(LastName, FirstName, BirthDate, HireDate, Address, City, Country, Notes) " +
                "VALUES ('Maruskevuch', 'Anrew', 1997-06-25, 2017-5-28, 'Univercity stt.', 'Lviv', 'Ukraine', 'Oksana');";
            string updateCity = "UPDATE Employees SET City = 'Kyiv' WHERE Employees.LastName = 'Kostelna'";



            MakeSelection(connectionString, showCitiesWhereFrom);
            Console.Read();
        }
    }
}
