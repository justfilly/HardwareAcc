using MySqlConnector;

namespace HardwareAcc.Services.DBConnection;

public interface IDBConnectionService
{
    MySqlConnection GetConnection();
    bool CheckForConnection();
}