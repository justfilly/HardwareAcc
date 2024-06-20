using System;
using System.Threading.Tasks;
using MySqlConnector;

namespace HardwareAcc.Services.DBConnectionService;

public interface IDBConnectionService : IDisposable
{
    MySqlConnection GetConnection();
}