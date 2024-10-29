using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.DBConnection;
using MySqlConnector;

namespace HardwareAcc.Services.Repositories.HardwareAudienceHistory
{
    public class HardwareAudienceHistoryRepository : IHardwareAudienceHistoryRepository
    {
        private readonly IDBConnectionService _dbConnectionService;

        public HardwareAudienceHistoryRepository(IDBConnectionService dbConnectionService)
        {
            _dbConnectionService = dbConnectionService;
        }

        public event Action Changed;

        public async Task<IEnumerable<HardwareAudienceHistoryModel>> GetAllByHardwareIdAsync(int hardwareId)
        {
            List<HardwareAudienceHistoryModel> history = new();

            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT h.*, a.code AS audience_code
                FROM hardwareacc.hardware_audiences_history h
                JOIN hardwareacc.audiences a ON h.audience_id = a.audience_id
                WHERE h.hardware_id = @hardwareId";
            command.Parameters.AddWithValue("@hardwareId", hardwareId);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                history.Add(DeserializeHistory(reader));

            return history;
        }

        public async Task<HardwareAudienceHistoryModel> GetWithLatestTransferredDateByHardwareIdAsync(int hardwareId)
        {
            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT h.*, a.code AS audience_code
                FROM hardwareacc.hardware_audiences_history h
                JOIN hardwareacc.audiences a ON h.audience_id = a.audience_id
                WHERE h.hardware_id = @hardwareId
                ORDER BY h.transferred_date DESC
                LIMIT 1";
            command.Parameters.AddWithValue("@hardwareId", hardwareId);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? DeserializeHistory(reader) : null;
        }

        public async Task<HardwareAudienceHistoryModel> GetByUserIdAndTransferredDateAsync(int userId, DateTime transferredDate)
        {
            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT h.*, a.code AS audience_code
                FROM hardwareacc.hardware_audiences_history h
                JOIN hardwareacc.audiences a ON h.audience_id = a.audience_id
                WHERE h.audience_id = @userId AND h.transferred_date = @transferredDate";
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@transferredDate", transferredDate);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();
            return await reader.ReadAsync() ? DeserializeHistory(reader) : null;
        }

        public async Task AddAsync(HardwareAudienceHistoryModel model)
        {
            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO hardwareacc.hardware_audiences_history 
                (hardware_id, audience_id, transferred_date, removed_date)
                VALUES (@hardwareId, @audienceId, @transferredDate, @removedDate)";

            command.Parameters.AddWithValue("@hardwareId", model.HardwareId);
            command.Parameters.AddWithValue("@audienceId", model.AudienceId);
            command.Parameters.AddWithValue("@transferredDate", model.TransferredDate);
            command.Parameters.AddWithValue("@removedDate", model.RemovedDate == DateTime.MinValue ? DBNull.Value : model.RemovedDate);

            await command.ExecuteNonQueryAsync();
            Changed?.Invoke();
        }

        public async Task DeleteAsync(int id)
        {
            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                DELETE FROM hardwareacc.hardware_audiences_history 
                WHERE hardware_audiences_history_id = @id";
            command.Parameters.AddWithValue("@id", id);

            await command.ExecuteNonQueryAsync();
            Changed?.Invoke();
        }

        public async Task UpdateAsync(HardwareAudienceHistoryModel model)
        {
            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE hardwareacc.hardware_audiences_history
                SET hardware_id = @hardwareId, audience_id = @audienceId, transferred_date = @transferredDate, removed_date = @removedDate
                WHERE hardware_audiences_history_id = @id";

            command.Parameters.AddWithValue("@hardwareId", model.HardwareId);
            command.Parameters.AddWithValue("@audienceId", model.AudienceId);
            command.Parameters.AddWithValue("@transferredDate", model.TransferredDate);
            command.Parameters.AddWithValue("@removedDate", model.RemovedDate == DateTime.MinValue ? DBNull.Value : model.RemovedDate);
            command.Parameters.AddWithValue("@id", model.Id);

            await command.ExecuteNonQueryAsync();
            Changed?.Invoke();
        }

        private static HardwareAudienceHistoryModel DeserializeHistory(IDataRecord reader)
        {
            return new HardwareAudienceHistoryModel
            {
                Id = reader.GetInt32(reader.GetOrdinal("hardware_audiences_history_id")),
                HardwareId = reader.GetInt32(reader.GetOrdinal("hardware_id")),
                AudienceId = reader.GetInt32(reader.GetOrdinal("audience_id")),
                TransferredDate = reader.GetDateTime(reader.GetOrdinal("transferred_date")),
                RemovedDate = reader.IsDBNull(reader.GetOrdinal("removed_date")) ? null : reader.GetDateTime(reader.GetOrdinal("removed_date")),
                AudienceCode = reader.GetString(reader.GetOrdinal("audience_code")),
            };
        }
    }
}
