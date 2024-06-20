using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace HardwareAcc.Services.DBConnectionService;

public class DBConnectionService : IDBConnectionService
{
    private readonly string? _connectionString;
    private MySqlConnection? _connection;
    
    public DBConnectionService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Dev");
    }

    public MySqlConnection GetConnection()
    {
        _connection ??= new MySqlConnection(_connectionString);
        
        if (_connection.State != ConnectionState.Open) 
            _connection.Open();
        
        return _connection;
    }

    public void Dispose()
    {
        _connection?.Dispose();
        GC.SuppressFinalize(this);
    }
}