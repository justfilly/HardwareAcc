using System;
using MySqlConnector;

namespace HardwareAcc.Services.DBConnection;

public interface IDBConnectionService : IDisposable
{
    MySqlConnection GetConnection();
}