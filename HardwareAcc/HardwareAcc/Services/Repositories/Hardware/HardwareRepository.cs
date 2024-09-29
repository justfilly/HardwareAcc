using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.DBConnection;
using MySqlConnector;

namespace HardwareAcc.Services.Repositories.Hardware;

public class HardwareRepository : IHardwareRepository
{
    private readonly IDBConnectionService _dbConnectionService;

    public HardwareRepository(IDBConnectionService dbConnectionService)
    {
        _dbConnectionService = dbConnectionService;
    }

    public event Action Changed;

    public async Task<IEnumerable<HardwareModel>> GetAllAsync()
    {
        List<HardwareModel> hardwareItems = new();

        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT h.*, 
                   a.code AS audience_code, 
                   u.login AS responsible_user_login, 
                   s.name AS status_name
            FROM hardwareacc.hardware h
            LEFT JOIN hardwareacc.audiences a ON h.audience_id = a.audience_id
            LEFT JOIN hardwareacc.users u ON h.responsible_user_id = u.user_id
            LEFT JOIN hardwareacc.hardware_statuses s ON h.status_id = s.hardware_status_id";

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            hardwareItems.Add(Deserialize(reader));

        return hardwareItems;
    }

    public async Task<HardwareModel> GetByInventoryNumberAsync(string inventoryNumber)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
                SELECT h.*, 
                       a.code AS audience_code, 
                       u.login AS responsible_user_login, 
                       s.name AS status_name
                FROM hardwareacc.hardware h
                LEFT JOIN hardwareacc.audiences a ON h.audience_id = a.audience_id
                LEFT JOIN hardwareacc.users u ON h.responsible_user_id = u.user_id
                LEFT JOIN hardwareacc.hardware_statuses s ON h.status_id = s.hardware_status_id
                WHERE h.inventory_number = @inventoryNumber";
        command.Parameters.AddWithValue("@inventoryNumber", inventoryNumber);

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return Deserialize(reader);

        return null;
    }

    public async Task AddAsync(HardwareModel model)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
                INSERT INTO hardwareacc.hardware (name, inventory_number, image, price, responsible_user_id, audience_id, status_id)
                VALUES (@name, @inventoryNumber, @image, @price, @responsibleUserId, @audienceId, @statusId)";

        command.Parameters.AddWithValue("@name", model.Name);
        command.Parameters.AddWithValue("@inventoryNumber", model.InventoryNumber);
        command.Parameters.AddWithValue("@image", DBNull.Value);
        command.Parameters.AddWithValue("@price", model.Price == 0 ? DBNull.Value : model.Price);
        command.Parameters.AddWithValue("@responsibleUserId", model.ResponsibleUserId == 0 ? DBNull.Value : model.ResponsibleUserId);
        command.Parameters.AddWithValue("@audienceId", model.AudienceId == 0 ? DBNull.Value : model.AudienceId);
        command.Parameters.AddWithValue("@statusId", model.StatusId == 0 ? DBNull.Value : model.StatusId);

        await command.ExecuteNonQueryAsync();
        Changed?.Invoke();
    }

    public async Task DeleteAsync(int id)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
                DELETE FROM hardwareacc.hardware 
                WHERE hardware_id = @hardwareId";
        command.Parameters.AddWithValue("@hardwareId", id);

        await command.ExecuteNonQueryAsync();
        Changed?.Invoke();
    }

    public async Task UpdateAsync(HardwareModel model)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
                UPDATE hardwareacc.hardware
                SET name = @name, 
                    inventory_number = @inventoryNumber, 
                    image = @image, 
                    price = @price, 
                    responsible_user_id = @responsibleUserId, 
                    audience_id = @audienceId, 
                    status_id = @statusId
                WHERE hardware_id = @id";

        command.Parameters.AddWithValue("@name", model.Name);
        command.Parameters.AddWithValue("@inventoryNumber", model.InventoryNumber);
        command.Parameters.AddWithValue("@image", DBNull.Value);
        command.Parameters.AddWithValue("@price", model.Price == 0 ? DBNull.Value : model.Price);
        command.Parameters.AddWithValue("@responsibleUserId", model.ResponsibleUserId == 0 ? DBNull.Value : model.ResponsibleUserId);
        command.Parameters.AddWithValue("@audienceId", model.AudienceId == 0 ? DBNull.Value : model.AudienceId);
        command.Parameters.AddWithValue("@statusId", model.StatusId == 0 ? DBNull.Value : model.StatusId);
        command.Parameters.AddWithValue("@id", model.Id);

        await command.ExecuteNonQueryAsync();
        Changed?.Invoke();
    }
    
    private static HardwareModel Deserialize(IDataRecord reader)
    {
        return new HardwareModel
        {
            Id = reader.GetInt32(reader.GetOrdinal("hardware_id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
            InventoryNumber = reader.GetString(reader.GetOrdinal("inventory_number")),
            Price = reader.IsDBNull(reader.GetOrdinal("price")) ? 0 : reader.GetDouble(reader.GetOrdinal("price")),
            
            ResponsibleUserId = reader.IsDBNull(reader.GetOrdinal("responsible_user_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("responsible_user_id")),
            ResponsibleUserLogin = reader.IsDBNull(reader.GetOrdinal("responsible_user_login")) ? null : reader.GetString(reader.GetOrdinal("responsible_user_login")),
            
            AudienceId = reader.IsDBNull(reader.GetOrdinal("audience_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("audience_id")),
            AudienceCode = reader.IsDBNull(reader.GetOrdinal("audience_code")) ? null : reader.GetString(reader.GetOrdinal("audience_code")),
            
            StatusId = reader.IsDBNull(reader.GetOrdinal("status_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("status_id")),
            StatusName = reader.IsDBNull(reader.GetOrdinal("status_name")) ? null : reader.GetString(reader.GetOrdinal("status_name")),
        };
    }
}