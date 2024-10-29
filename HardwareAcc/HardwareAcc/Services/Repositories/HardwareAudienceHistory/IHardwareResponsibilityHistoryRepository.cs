using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.HardwareResponsibilityHistory;

public interface IHardwareAudienceHistoryRepository
{
    event Action Changed;

    Task<IEnumerable<HardwareAudienceHistoryModel>> GetAllByHardwareIdAsync(int hardwareId);
    Task<HardwareAudienceHistoryModel> GetWithLatestTransferredDateByHardwareIdAsync(int hardwareId);
    Task<HardwareAudienceHistoryModel> GetByUserIdAndTransferredDateAsync(int userId, DateTime transferredDate);

    Task AddAsync(HardwareAudienceHistoryModel model);
    Task DeleteAsync(int id);
    Task UpdateAsync(HardwareAudienceHistoryModel model);
}