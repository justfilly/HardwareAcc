using System;
using System.Threading.Tasks;
using HardwareAcc.Models;
using HardwareAcc.Services.DBConnectionService;
using MySqlConnector;

namespace HardwareAcc.Services.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDBConnectionService _dbConnectionService;

    public UserRepository(IDBConnectionService dbConnectionService)
    {
        _dbConnectionService = dbConnectionService;
    }

    public async Task<User?> GetUserByLoginAsync(string login)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM hardwareacc.users WHERE login = @login";
        command.Parameters.AddWithValue("@login", login);
            
        await using (command)
        {
            await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("user_id")),
                        Login = reader.GetString(reader.GetOrdinal("login")),
                        Password = reader.GetString(reader.GetOrdinal("password")),
                        RoleId = reader.GetInt32(reader.GetOrdinal("role_id")),
                        
                        Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : 
                            reader.GetString(reader.GetOrdinal("email")),
                        
                        PhoneNumber = reader.IsDBNull(reader.GetOrdinal("phone_number")) ? null : 
                            reader.GetString(reader.GetOrdinal("phone_number")),
                        
                        FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                        SecondName = reader.GetString(reader.GetOrdinal("second_name")),
                        Patronymic = reader.GetString(reader.GetOrdinal("patronymic"))
                    };
                }
            }
        }

        return null;
    }
}