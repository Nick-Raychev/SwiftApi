using Microsoft.Data.Sqlite;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
namespace SwiftMT799Api
{
public class SwiftRepository
{
    private readonly string _connectionString;

    public SwiftRepository(string connectionString)
    {
       if (connectionString == null)
        {
            throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null.");
        }

        _connectionString = connectionString;
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            // Създаване на таблица, ако не съществува
            connection.Execute(@"
                CREATE TABLE IF NOT EXISTS SwiftMessages (
                    Id INTEGER PRIMARY KEY,
                    Field20 TEXT,
                    Field21 TEXT,
                    Field79 TEXT
                    -- Добавете други полета според вашите нужди
                )");
        }
    }

    public void SaveSwiftMessage(SwiftMessage swiftMessage)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            connection.Execute(@"
                INSERT INTO SwiftMessages (Field20, Field21, Field79)
                VALUES (@Field20, @Field21, @Field79)", swiftMessage);
        }
    }

    public IEnumerable<SwiftMessage> GetAllSwiftMessages()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            return connection.Query<SwiftMessage>("SELECT * FROM SwiftMessages");
        }
    }
}
}