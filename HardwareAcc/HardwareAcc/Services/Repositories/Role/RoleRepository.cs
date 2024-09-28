using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.DBConnection;
using MySqlConnector;

namespace HardwareAcc.Services.Repositories.Role;

public class RoleRepository : IRoleRepository
{
    private readonly IDBConnectionService _dbConnectionService;

    public RoleRepository(IDBConnectionService dbConnectionService)
    {
        _dbConnectionService = dbConnectionService;
    }

    public async Task<IEnumerable<RoleModel>> GetAllRolesAsync()
    {
        List<RoleModel> roles = new();

        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT * 
            FROM hardwareacc.roles";

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync()) 
            roles.Add(DeserializeRole(reader));

        return roles;
    }

    private static RoleModel DeserializeRole(IDataRecord reader)
    {
        return new RoleModel
        {
            Id = reader.GetInt32(reader.GetOrdinal("role_id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
        };
    }
}