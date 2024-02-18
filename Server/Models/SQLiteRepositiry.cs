using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Server.Models;

public class SQLiteRepositiry : IModelDB
{
    private string _connection = "dd";
    private List<Order> orders = new();
    private const string CreateTableQuery = @"
        CREATE TABLE IF NOT EXISTS Orders (
            ID INT PRIMARY KEY AUTO_INCREMENT,
            Customer TEXT NOT NULL,
            Product string NOT NULL,
            Count INTEGER NOT NULL
        )";

    public SQLiteRepositiry(string connectionString)
    {
        _connection = connectionString;
        InitializeDatabase();
        ReadDataFromDatabase();
    }



    // для АПИшки
    public List<Order> GetAllOrders()
    {
        List<Order> orders = new();
        string query = "SELECT * FROM Orders";
        using SQLiteConnection connection = new(_connection);
        connection.Open();
        using SQLiteCommand command = new(query, connection);
        using SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Order order = new((int)(reader["ID"]), (string)(reader["Customer"]), (string)(reader["Product"]), (int)(reader["Count"]));
            orders.Add(order);
        }
        return orders;
    }

    public void DeleteOrder(string orderName)
    {
        string query = "DELETE FROM Orders WHERE @Name = Product";
        using SQLiteConnection connection = new(_connection);
        using SQLiteCommand command = new(query, connection);
        command.Parameters.AddWithValue("@Name", orderName.ToUpper());
        command.ExecuteNonQuery();
    }

    public void AddOrder(Order order)
    {
        string query = "INSERT INTO Products (Customer, Product, Count) VALUES (@Customer, @Product, @Count)";
        using SQLiteConnection connection = new(_connection);
        using SQLiteCommand command = new(query, connection);
        command.Parameters.AddWithValue("@Customer", order.Customer);
        command.Parameters.AddWithValue("@Product", order.Product);
        command.Parameters.AddWithValue("@Count", order.Count);
        command.ExecuteNonQuery();
    }


    private void ReadDataFromDatabase()
    {
        orders = GetAllOrders();
    }

    private void InitializeDatabase()
    {
        SQLiteConnection connection = new(_connection);
        Console.WriteLine("База данных :  " + _connection + " создана");
        connection.Open();
        SQLiteCommand command = new(CreateTableQuery, connection);
        command.ExecuteNonQuery();
    }
}
