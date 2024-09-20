using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HardwareAcc.Models;
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

    public event Action? AudiencesChanged;

    public async Task<IEnumerable<AudienceModel>> GetAllAudiencesAsync()
    {
        var audiences = new List<AudienceModel>();

        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM hardwareacc.audiences";

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync()) 
            audiences.Add(DeserializeAudience(reader));

        return audiences;
    }

    public async Task AddAudienceAsync(AudienceModel audience)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO hardwareacc.audiences (name, code)
            VALUES (@name, @code)";
        command.Parameters.AddWithValue("@name", audience.Name);
        command.Parameters.AddWithValue("@code", audience.Code);

        await command.ExecuteNonQueryAsync();
        AudiencesChanged?.Invoke();
    }
    
    public async Task DeleteAudienceAsync(int audienceId)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            DELETE FROM hardwareacc.audiences 
            WHERE audience_id = @audienceId";
        command.Parameters.AddWithValue("@audienceId", audienceId);

        await command.ExecuteNonQueryAsync();
        AudiencesChanged?.Invoke();
    }

    public async Task UpdateAudienceAsync(AudienceModel audience)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE hardwareacc.audiences
            SET name = @name, code = @code
            WHERE audience_id = @id";
        command.Parameters.AddWithValue("@name", audience.Name);
        command.Parameters.AddWithValue("@code", audience.Code);
        command.Parameters.AddWithValue("@id", audience.Id);

        await command.ExecuteNonQueryAsync();
        AudiencesChanged?.Invoke(); // Notify listeners of the change
    }
    
    private static AudienceModel DeserializeAudience(IDataRecord reader)
    {
        return new AudienceModel
        {
            Id = reader.GetInt32(reader.GetOrdinal("audience_id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
            Code = reader.GetString(reader.GetOrdinal("code"))
        };
    }
}