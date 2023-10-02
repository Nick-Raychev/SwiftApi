using Microsoft.Data.Sqlite;
using System;
using System.Data.Common;

public class SwiftDbContext : IDisposable
{
    private readonly DbConnection _connection;

    public SwiftDbContext(string connectionString)
    {
        _connection = new SqliteConnection(connectionString);
        _connection.Open();
    }

    public void Dispose()
    {
        _connection.Close();
    }
}