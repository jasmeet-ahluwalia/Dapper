using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

public class GenericDatabaseHelper<T> where T : class, new()
{
    private readonly string _connectionString;

    public GenericDatabaseHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public void CreateTable()
    {
        using var connection = CreateConnection();
        connection.Open();

        var tableName = typeof(T).Name + "s";
        var properties = typeof(T).GetProperties().Where(p => p.Name != "Id"); // Exclude Id property
        var columns = properties.Select(p => $"{p.Name} {GetSqlType(p.PropertyType)}");
        var createTableQuery = $"IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '{tableName}') " +
                               $"CREATE TABLE {tableName} (Id INT PRIMARY KEY IDENTITY(1,1), {string.Join(", ", columns)})";

        connection.Execute(createTableQuery);
    }


    public void Insert(T entity)
    {
        using var connection = CreateConnection();
        connection.Open();

        var tableName = typeof(T).Name + "s";
        var properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToArray();
        var columns = string.Join(", ", properties.Select(p => p.Name));
        var values = string.Join(", ", properties.Select(p => "@" + p.Name));
        var insertQuery = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

        connection.Execute(insertQuery, entity);
    }

    public IEnumerable<T> GetAll()
    {
        using var connection = CreateConnection();
        connection.Open();

        var tableName = typeof(T).Name + "s";
        var selectQuery = $"SELECT * FROM {tableName}";

        return connection.Query<T>(selectQuery);
    }

    public T GetById(int id)
    {
        using var connection = CreateConnection();
        connection.Open();

        var tableName = typeof(T).Name + "s";
        var selectQuery = $"SELECT * FROM {tableName} WHERE Id = @Id";

        return connection.QuerySingleOrDefault<T>(selectQuery, new { Id = id });
    }

    public void Update(T entity)
    {
        using var connection = CreateConnection();
        connection.Open();

        var tableName = typeof(T).Name + "s";
        var properties = typeof(T).GetProperties().Where(p => p.Name != "Id").ToArray();
        var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
        var updateQuery = $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";

        connection.Execute(updateQuery, entity);
    }

    public void Delete(int id)
    {
        using var connection = CreateConnection();
        connection.Open();

        var tableName = typeof(T).Name + "s";
        var deleteQuery = $"DELETE FROM {tableName} WHERE Id = @Id";

        connection.Execute(deleteQuery, new { Id = id });
    }

    private string GetSqlType(Type type)
    {
        if (type == typeof(int)) return "INT";
        if (type == typeof(string)) return "NVARCHAR(MAX)";
        if (type == typeof(DateTime)) return "DATETIME";
        // Add more type mappings as needed
        throw new NotSupportedException($"Unsupported type: {type.Name}");
    }
}
