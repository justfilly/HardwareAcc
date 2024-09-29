using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.DBConnection;
using MySqlConnector;

namespace HardwareAcc.Services.Repositories.Status;

public class StatusRepository : IStatusRepository
{
    private readonly IDBConnectionService _dbConnectionService;

    public StatusRepository(IDBConnectionService dbConnectionService)
    {
        _dbConnectionService = dbConnectionService;
    }

    public event Action Changed;

    public async Task<IEnumerable<StatusModel>> GetAllAsync()
    {
        List<StatusModel> statuses = new();

        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT * 
            FROM hardwareacc.hardware_statuses";

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync()) 
            statuses.Add(DeserializeStatus(reader));

        return statuses;
    }

    public async Task<StatusModel> GetByNameAsync(string name)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();
        
        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT * 
            FROM hardwareacc.hardware_statuses 
            WHERE name = @name";
        command.Parameters.AddWithValue("@name", name);

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return DeserializeStatus(reader);

        return null;
    }

    public async Task AddAsync(StatusModel model)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO hardwareacc.hardware_statuses (name)
            VALUES (@name)";
        command.Parameters.AddWithValue("@name", model.Name);

        await command.ExecuteNonQueryAsync();
        Changed?.Invoke();
    }

    public async Task DeleteAsync(int statusId)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            DELETE FROM hardwareacc.hardware_statuses 
            WHERE hardware_status_id = @statusId";
        command.Parameters.AddWithValue("@statusId", statusId);

        await command.ExecuteNonQueryAsync();
        Changed?.Invoke();
    }

    public async Task UpdateAsync(StatusModel model)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE hardwareacc.hardware_statuses
            SET name = @name
            WHERE hardware_status_id = @id";
        command.Parameters.AddWithValue("@name", model.Name);
        command.Parameters.AddWithValue("@id", model.Id);

        await command.ExecuteNonQueryAsync();
        Changed?.Invoke();
    }


    private static StatusModel DeserializeStatus(IDataRecord reader)
    {
        return new StatusModel
        {
            Id = reader.GetInt32(reader.GetOrdinal("hardware_status_id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
        };
    }
}