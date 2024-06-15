using System;
using MySqlConnector;

namespace HardwareAcc.Services.DBConnectionService;

public interface IDBConnectionService : IDisposable
{
    MySqlConnection GetConnection();
}