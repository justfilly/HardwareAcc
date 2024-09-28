using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.Status;

public interface IStatusRepository
{
    event Action StatusesChanged;
    Task<IEnumerable<StatusModel>> GetAllStatusesAsync();
    Task AddStatusAsync(StatusModel status);
    Task DeleteStatusAsync(int statusId);
    Task UpdateStatusAsync(StatusModel status);
    Task<StatusModel> GetStatusByNameAsync(string name);
}