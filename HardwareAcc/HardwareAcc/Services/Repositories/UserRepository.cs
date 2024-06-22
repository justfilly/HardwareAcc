using System;
using System.Data;
using System.Threading.Tasks;
using HardwareAcc.Models;
using HardwareAcc.Services.DBConnection;
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

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        
        if (await reader.ReadAsync())
            return DeserializeUser(reader);

        return null;
    }
    
    public async Task<User?> GetUserByEmailAsync(string? email)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM hardwareacc.users WHERE email = @email";
        command.Parameters.AddWithValue("@email", email);

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        
        if (await reader.ReadAsync())
            return DeserializeUser(reader);

        return null;
    }

    public async Task<User?> GetUserByPhoneNumberAsync(string? phoneNumber)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM hardwareacc.users WHERE phone_number = @phone_number";
        command.Parameters.AddWithValue("@phone_number", phoneNumber);

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        
        if (await reader.ReadAsync())
            return DeserializeUser(reader);

        return null;
    }

    public async Task AddUserAsync(User user)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
                INSERT INTO hardwareacc.users (login, password, role_id, email, phone_number, first_name, second_name, patronymic)
                VALUES (@login, @password, @role_id, @Email, @PhoneNumber, @FirstName, @SecondName, @Patronymic)";

        command.Parameters.AddWithValue("@login", user.Login);
        command.Parameters.AddWithValue("@password", user.Password);
        command.Parameters.AddWithValue("@role_id", user.RoleId);
        command.Parameters.AddWithValue("@FirstName", user.FirstName);
        command.Parameters.AddWithValue("@SecondName", user.SecondName);
        command.Parameters.AddWithValue("@Patronymic", user.Patronymic);

        if (string.IsNullOrEmpty(user.Email))
            command.Parameters.AddWithValue("@Email", DBNull.Value);
        else
            command.Parameters.AddWithValue("@Email", user.Email);

        if (string.IsNullOrEmpty(user.PhoneNumber))
            command.Parameters.AddWithValue("@PhoneNumber", DBNull.Value);
        else
            command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

        await command.ExecuteNonQueryAsync();
    }

    private static User? DeserializeUser(IDataRecord reader)
    {
        return new User
        {
            Id = reader.GetInt32(reader.GetOrdinal("user_id")),
            Login = reader.GetString(reader.GetOrdinal("login")),
            Password = reader.GetString(reader.GetOrdinal("password")),
            RoleId = reader.GetInt32(reader.GetOrdinal("role_id")),

            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),

            PhoneNumber = reader.IsDBNull(reader.GetOrdinal("phone_number"))
                ? null
                : reader.GetString(reader.GetOrdinal("phone_number")),

            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
            SecondName = reader.GetString(reader.GetOrdinal("second_name")),
            Patronymic = reader.GetString(reader.GetOrdinal("patronymic"))
        };
    }
}
