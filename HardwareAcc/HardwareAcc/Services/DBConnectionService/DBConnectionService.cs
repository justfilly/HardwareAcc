using System;
using MySqlConnector;

namespace HardwareAcc.Services.DBConnectionService;

public class DBConnectionService : IDBConnectionService
{
    private readonly string _connectionString;
    private MySqlConnection? _connection;

    public DBConnectionService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public MySqlConnection GetConnection()
    {
        _connection ??= new MySqlConnection(_connectionString);

        if (_connection.State != System.Data.ConnectionState.Open) 
            _connection.Open();

        return _connection;
    }

    public void Dispose()
    {
        _connection?.Dispose();
        GC.SuppressFinalize(this);
    }
}