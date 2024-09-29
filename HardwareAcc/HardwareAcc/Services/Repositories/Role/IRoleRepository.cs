using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.Role;

public interface IRoleRepository
{
    Task<IEnumerable<RoleModel>> GetAllAsync();

    Task<RoleModel> GetByNameAsync(string name);
}