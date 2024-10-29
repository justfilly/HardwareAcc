using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.DBConnection;
using MySqlConnector;

namespace HardwareAcc.Services.Repositories.Audience;

public class AudienceRepository : IAudienceRepository
{
    private readonly IDBConnectionService _dbConnectionService;

    public AudienceRepository(IDBConnectionService dbConnectionService)
    {
        _dbConnectionService = dbConnectionService;
    }

    public event Action Changed;

    public async Task<IEnumerable<AudienceModel>> GetAllAsync()
    {
        List<AudienceModel> audiences = new();

        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM hardwareacc.audiences";

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync()) 
            audiences.Add(DeserializeAudience(reader));

        return audiences;
    }

    public async Task<AudienceModel> GetByCodeAsync(string code)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();
        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT * 
            FROM hardwareacc.audiences 
            WHERE code = @code";
        command.Parameters.AddWithValue("@code", code);

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return DeserializeAudience(reader);

        return null;
    }

    public async Task<AudienceModel> GetByIdAsync(int id)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();
        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
        SELECT * 
        FROM hardwareacc.audiences 
        WHERE audience_id = @id";
        command.Parameters.AddWithValue("@id", id);

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return DeserializeAudience(reader);

        return null;
    }

    public async Task AddAsync(AudienceModel model)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO hardwareacc.audiences (name, code)
            VALUES (@name, @code)";
        
        command.Parameters.AddWithValue("@name", string.IsNullOrEmpty(model.Name) ? DBNull.Value : model.Name);
        command.Parameters.AddWithValue("@code", model.Code);

        await command.ExecuteNonQueryAsync();
        Changed?.Invoke();
    }

    public async Task DeleteAsync(int id)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            DELETE FROM hardwareacc.audiences 
            WHERE audience_id = @audienceId";
        command.Parameters.AddWithValue("@audienceId", id);

        await command.ExecuteNonQueryAsync();
        Changed?.Invoke();
    }

    public async Task UpdateAsync(AudienceModel model)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE hardwareacc.audiences
            SET name = @name, code = @code
            WHERE audience_id = @id";
        command.Parameters.AddWithValue("@name", string.IsNullOrEmpty(model.Name) ? DBNull.Value : model.Name);
        command.Parameters.AddWithValue("@code", model.Code);
        command.Parameters.AddWithValue("@id", model.Id);

        await command.ExecuteNonQueryAsync();
        Changed?.Invoke();
    }


    private static AudienceModel DeserializeAudience(IDataRecord reader)
    {
        return new AudienceModel
        {
            Id = reader.GetInt32(reader.GetOrdinal("audience_id")),
            Name = reader.IsDBNull(reader.GetOrdinal("name")) ? null : reader.GetString(reader.GetOrdinal("name")),
            Code = reader.GetString(reader.GetOrdinal("code"))
        };
    }
}