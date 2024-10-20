using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.DBConnection;
using MySqlConnector;

namespace HardwareAcc.Services.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly IDBConnectionService _dbConnectionService;

    public UserRepository(IDBConnectionService dbConnectionService)
    {
        _dbConnectionService = dbConnectionService;
    }

    public event Action Changed;

    public async Task<IEnumerable<UserModel>> GetAllAsync()
    {
        List<UserModel> users = new();

        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT u.*, r.name AS role_name 
            FROM hardwareacc.users u
            LEFT JOIN hardwareacc.roles r ON u.role_id = r.role_id";

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync()) 
            users.Add(DeserializeUser(reader));

        return users;
    }

    public async Task<UserModel> GetByLoginAsync(string login)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT u.*, r.name AS role_name 
            FROM hardwareacc.users u 
            LEFT JOIN hardwareacc.roles r ON u.role_id = r.role_id 
            WHERE u.login = @login";
        command.Parameters.AddWithValue("@login", login);

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
    
        if (await reader.ReadAsync())
            return DeserializeUser(reader);

        return null;
    }

    public async Task<UserModel> GetByEmailAsync(string email)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT u.*, r.name AS role_name 
            FROM hardwareacc.users u 
            LEFT JOIN hardwareacc.roles r ON u.role_id = r.role_id 
            WHERE u.email = @Email";
        command.Parameters.AddWithValue("@Email", email);

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
    
        if (await reader.ReadAsync())
            return DeserializeUser(reader);

        return null;
    }

    public async Task<UserModel> GetByPhoneNumberAsync(string phoneNumber)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT u.*, r.name AS role_name 
            FROM hardwareacc.users u 
            LEFT JOIN hardwareacc.roles r ON u.role_id = r.role_id 
            WHERE u.phone_number = @phone_number";
        command.Parameters.AddWithValue("@phone_number", phoneNumber);

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
    
        if (await reader.ReadAsync())
            return DeserializeUser(reader);

        return null;
    }

    public async Task<UserModel> GetByIdAsync(int id)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();
        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
        SELECT u.*, r.name AS role_name 
        FROM hardwareacc.users u 
        LEFT JOIN hardwareacc.roles r ON u.role_id = r.role_id 
        WHERE u.user_id = @id";
        command.Parameters.AddWithValue("@id", id);

        await using MySqlDataReader reader = await command.ExecuteReaderAsync();
    
        if (await reader.ReadAsync())
            return DeserializeUser(reader);

        return null;
    }

    public async Task AddAsync(UserModel model)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
                INSERT INTO hardwareacc.users (login, password, role_id, email, phone_number, first_name, second_name, patronymic)
                VALUES (@login, @password, @role_id, @Email, @PhoneNumber, @FirstName, @SecondName, @Patronymic)";

        command.Parameters.AddWithValue("@login", model.Login);
        command.Parameters.AddWithValue("@password", model.Password);
        command.Parameters.AddWithValue("@role_id", model.RoleId);
        command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(model.Email) ? DBNull.Value : model.Email);
        command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(model.Email) ? DBNull.Value : model.PhoneNumber);
        command.Parameters.AddWithValue("@FirstName", model.FirstName);
        command.Parameters.AddWithValue("@SecondName", model.SecondName);
        command.Parameters.AddWithValue("@Patronymic", model.Patronymic);
        
        await command.ExecuteNonQueryAsync();
        
        Changed?.Invoke();
    }

    public async Task DeleteAsync(int id)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        command.CommandText = @"
            DELETE FROM hardwareacc.users 
            WHERE user_id = @userId";
        command.Parameters.AddWithValue("@userId", id);

        await command.ExecuteNonQueryAsync();
        Changed?.Invoke();
    }

    public async Task UpdateAsync(UserModel model)
    {
        await using MySqlConnection connection = _dbConnectionService.GetConnection();

        await using MySqlCommand command = connection.CreateCommand();
        
        command.CommandText = @"
            UPDATE hardwareacc.users
            SET login = @login, password = @password, role_id = @role_id, email = @Email, phone_number = @PhoneNumber, first_name = @FirstName, second_name = @SecondName, patronymic = @Patronymic
            WHERE user_id = @id";
        
        command.Parameters.AddWithValue("@login", model.Login);
        command.Parameters.AddWithValue("@password", model.Password);
        command.Parameters.AddWithValue("@role_id", model.RoleId);
        command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(model.Email) ? DBNull.Value : model.Email);
        command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(model.Email) ? DBNull.Value : model.PhoneNumber);
        command.Parameters.AddWithValue("@FirstName", model.FirstName);
        command.Parameters.AddWithValue("@SecondName", model.SecondName);
        command.Parameters.AddWithValue("@Patronymic", model.Patronymic);
        command.Parameters.AddWithValue("@id", model.Id);


        await command.ExecuteNonQueryAsync();
        Changed?.Invoke();
    }


    private static UserModel DeserializeUser(IDataRecord reader)
    {
        return new UserModel
        {
            Id = reader.GetInt32(reader.GetOrdinal("user_id")),
            Login = reader.GetString(reader.GetOrdinal("login")),
            Password = reader.GetString(reader.GetOrdinal("password")),
            RoleId = reader.GetInt32(reader.GetOrdinal("role_id")),
            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
            PhoneNumber = reader.IsDBNull(reader.GetOrdinal("phone_number")) ? null : reader.GetString(reader.GetOrdinal("phone_number")),
            FirstName = reader.GetString(reader.GetOrdinal("first_name")),
            SecondName = reader.GetString(reader.GetOrdinal("second_name")),
            Patronymic = reader.GetString(reader.GetOrdinal("patronymic")),
                
            RoleName = reader.GetString(reader.GetOrdinal("role_name")),
        };
    }
}
