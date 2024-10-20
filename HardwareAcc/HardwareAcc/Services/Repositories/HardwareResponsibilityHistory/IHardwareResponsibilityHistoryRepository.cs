using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.HardwareResponsibilityHistory;

public interface IHardwareResponsibilityHistoryRepository
{
    event Action Changed;

    Task<IEnumerable<HardwareResponsibilityHistoryModel>> GetAllByHardwareIdAsync(int hardwareId);
    Task<HardwareResponsibilityHistoryModel> GetWithLatestStartDateByHardwareIdAsync(int hardwareId);
    Task<HardwareResponsibilityHistoryModel> GetByUserIdAndStartDateAsync(int userId, DateTime startDate);

    Task AddAsync(HardwareResponsibilityHistoryModel model);
    Task DeleteAsync(int id);
    Task UpdateAsync(HardwareResponsibilityHistoryModel model);
}