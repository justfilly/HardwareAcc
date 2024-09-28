using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace HardwareAcc.Services.DBConnection;

public sealed class DBConnectionService : IDBConnectionService
{
    private readonly string _connectionString;
    
    public DBConnectionService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Dev");
    }

    public MySqlConnection GetConnection()
    {
        MySqlConnection connection = new(_connectionString);
        if (connection.State != ConnectionState.Open)
            connection.Open();

        return connection;
    }

}