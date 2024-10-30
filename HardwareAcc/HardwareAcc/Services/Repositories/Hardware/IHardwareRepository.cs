using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.Hardware;

public interface IHardwareRepository
{
    event Action Changed;
    Task<IEnumerable<HardwareModel>> GetAllAsync();
    Task<IEnumerable<HardwareModel>> GetAllByResponsibleUserIdAsync(int responsibleUserId);
    Task<HardwareModel> GetByInventoryNumberAsync(string inventoryNumber);
    Task<HardwareModel> GetByIdAsync(int id);
    Task AddAsync(HardwareModel model);
    Task DeleteAsync(int id);
    Task UpdateAsync(HardwareModel model);
}