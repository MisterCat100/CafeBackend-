using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Server.Models;

public class SQLiteRepositiry : IModelDB
{
    private readonly string _connection;
    private List<Order> orders = new();
    private const string CreateTableQuery = @"
        CREATE TABLE IF NOT EXISTS Orders (
            ID INTEGER PRIMARY KEY,
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
            Order order = new(((string)(reader["Customer"])).ToUpper(),
                              ((string)(reader["Product"])).ToUpper(),
                              (Int64)(reader["Count"]));
            orders.Add(order);
        }
        return orders;
    }

    public void DeleteOrder(string orderName)
    {
        string query = "DELETE FROM Orders WHERE @Name = Product";
        using SQLiteConnection connection = new(_connection);
        connection.Open();
        using SQLiteCommand command = new(query, connection);
        command.Parameters.AddWithValue("@Name", orderName.ToUpper());
        command.ExecuteNonQuery();
    }

    public void AddOrder(Order order)
    {
        string query = "INSERT INTO Orders (Customer, Product, Count) VALUES (@Customer, @Product, @Count)";
        using SQLiteConnection connection = new(_connection);
        connection.Open();
        using SQLiteCommand command = new(query, connection);
        command.Parameters.AddWithValue("@Customer", order.Customer.ToUpper());
        command.Parameters.AddWithValue("@Product", order.Product.ToUpper());
        command.Parameters.AddWithValue("@Count", order.Count);
        command.ExecuteNonQuery();
    }

    public void EraseDataBase(string password)
    {
        Console.WriteLine("Вы уверены, что хотите удалить базу данных?\nYES/NO");
        string? answer = Console.ReadLine();
        if (answer == "YES") File.Delete("DataBase.db");
    }


    private void ReadDataFromDatabase()
    {
        orders = GetAllOrders();
    }

    private void InitializeDatabase()
    {
        SQLiteConnection connection = new(_connection);
        Console.WriteLine("Подкючение к базе данных  " + _connection + " создано");
        connection.Open();
        SQLiteCommand command = new(CreateTableQuery, connection);
        command.ExecuteNonQuery();
    }
}
