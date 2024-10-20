using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.DBConnection;
using MySqlConnector;

namespace HardwareAcc.Services.Repositories.HardwareResponsibilityHistory
{
    public class HardwareResponsibilityHistoryRepository : IHardwareResponsibilityHistoryRepository
    {
        private readonly IDBConnectionService _dbConnectionService;

        public HardwareResponsibilityHistoryRepository(IDBConnectionService dbConnectionService)
        {
            _dbConnectionService = dbConnectionService;
        }

        public event Action Changed;

        public async Task<IEnumerable<HardwareResponsibilityHistoryModel>> GetAllByHardwareIdAsync(int hardwareId)
        {
            List<HardwareResponsibilityHistoryModel> history = new();

            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT hrh.*, u.login AS responsible_user_login 
                FROM hardwareacc.hardware_responsibility_history hrh
                INNER JOIN hardwareacc.users u ON hrh.responsible_user_id = u.user_id
                WHERE hardware_id = @hardwareId";
            command.Parameters.AddWithValue("@hardwareId", hardwareId);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
                history.Add(DeserializeHistory(reader));

            return history;
        }

        public async Task<HardwareResponsibilityHistoryModel> GetWithLatestStartDateByHardwareIdAsync(int hardwareId)
        {
            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                SELECT hrh.*, u.login AS responsible_user_login 
                FROM hardwareacc.hardware_responsibility_history hrh
                INNER JOIN hardwareacc.users u ON hrh.responsible_user_id = u.user_id
                WHERE hardware_id = @hardwareId
                ORDER BY responsibility_start_date DESC
                LIMIT 1";
            command.Parameters.AddWithValue("@hardwareId", hardwareId);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return DeserializeHistory(reader);

            return null;
        }

        public async Task<HardwareResponsibilityHistoryModel> GetByUserIdAndStartDateAsync(int userId, DateTime startDate)
        {
            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();

            command.CommandText = @"
                SELECT hrh.*, u.login AS responsible_user_login 
                FROM hardwareacc.hardware_responsibility_history hrh
                INNER JOIN hardwareacc.users u ON hrh.responsible_user_id = u.user_id
                WHERE hrh.responsible_user_id = @userId 
                AND hrh.responsibility_start_date = @startDate";
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@startDate", startDate);

            await using MySqlDataReader reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return DeserializeHistory(reader);

            return null;
        }

        public async Task AddAsync(HardwareResponsibilityHistoryModel model)
        {
            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO hardwareacc.hardware_responsibility_history 
                (hardware_id, responsible_user_id, comment, responsibility_start_date, responsibility_end_date)
                VALUES (@hardwareId, @responsibleUserId, @comment, @responsibilityStartDate, @responsibilityEndDate)";

            command.Parameters.AddWithValue("@hardwareId", model.HardwareId);
            command.Parameters.AddWithValue("@responsibleUserId", model.ResponsibleUserId);
            command.Parameters.AddWithValue("@comment", string.IsNullOrEmpty(model.Comment) ? DBNull.Value : model.Comment);
            command.Parameters.AddWithValue("@responsibilityStartDate", model.ResponsibilityStartDate);
            command.Parameters.AddWithValue("@responsibilityEndDate", model.ResponsibilityEndDate == DateTime.MinValue ? DBNull.Value : model.ResponsibilityEndDate);

            await command.ExecuteNonQueryAsync();
            Changed?.Invoke();
        }

        public async Task DeleteAsync(int id)
        {
            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                DELETE FROM hardwareacc.hardware_responsibility_history 
                WHERE hardware_responsibility_history_id = @id";
            command.Parameters.AddWithValue("@id", id);

            await command.ExecuteNonQueryAsync();
            Changed?.Invoke();
        }

        public async Task UpdateAsync(HardwareResponsibilityHistoryModel model)
        {
            await using MySqlConnection connection = _dbConnectionService.GetConnection();
            await using MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE hardwareacc.hardware_responsibility_history
                SET hardware_id = @hardwareId, responsible_user_id = @responsibleUserId, comment = @comment, responsibility_start_date = @responsibilityStartDate, responsibility_end_date = @responsibilityEndDate
                WHERE hardware_responsibility_history_id = @id";

            command.Parameters.AddWithValue("@hardwareId", model.HardwareId);
            command.Parameters.AddWithValue("@responsibleUserId", model.ResponsibleUserId);
            command.Parameters.AddWithValue("@comment", string.IsNullOrEmpty(model.Comment) ? DBNull.Value : model.Comment);
            command.Parameters.AddWithValue("@responsibilityStartDate", model.ResponsibilityStartDate);
            command.Parameters.AddWithValue("@responsibilityEndDate", model.ResponsibilityEndDate == DateTime.MinValue ? DBNull.Value : model.ResponsibilityEndDate);
            command.Parameters.AddWithValue("@id", model.Id);

            await command.ExecuteNonQueryAsync();
            Changed?.Invoke();
        }

        private static HardwareResponsibilityHistoryModel DeserializeHistory(IDataRecord reader)
        {
            string responsibilityEndDateText = "";

            if (reader.IsDBNull(reader.GetOrdinal("responsibility_end_date")) == false)
            {
                responsibilityEndDateText = reader.GetDateTime(reader.GetOrdinal("responsibility_end_date"))
                    .ToString("dd.MM.yyyy");
            }
            
            return new HardwareResponsibilityHistoryModel
            {
                Id = reader.GetInt32(reader.GetOrdinal("hardware_responsibility_history_id")),
                HardwareId = reader.GetInt32(reader.GetOrdinal("hardware_id")),
                ResponsibleUserId = reader.GetInt32(reader.GetOrdinal("responsible_user_id")),
                Comment = reader.IsDBNull(reader.GetOrdinal("comment")) ? null : reader.GetString(reader.GetOrdinal("comment")),
                ResponsibilityStartDate = reader.GetDateTime(reader.GetOrdinal("responsibility_start_date")),
                ResponsibilityEndDate = reader.IsDBNull(reader.GetOrdinal("responsibility_end_date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("responsibility_end_date")),
                ResponsibleUserLogin = reader.GetString(reader.GetOrdinal("responsible_user_login")),
                ResponsibilityEndDateText = responsibilityEndDateText,
            };
        }
    }
}
