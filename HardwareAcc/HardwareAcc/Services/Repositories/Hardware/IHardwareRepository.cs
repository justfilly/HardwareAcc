using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.Hardware;

public interface IHardwareRepository
{
    event Action Changed;
    Task<IEnumerable<HardwareModel>> GetAllAsync();
    Task<HardwareModel> GetByInventoryNumberAsync(string inventoryNumber);
    Task AddAsync(HardwareModel model);
    Task DeleteAsync(int id);
    Task UpdateAsync(HardwareModel model);
}