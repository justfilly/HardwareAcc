using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.Audience;

public interface IAudienceRepository
{
    event Action Changed;
    
    Task<IEnumerable<AudienceModel>> GetAllAsync();
    Task<AudienceModel> GetByCodeAsync(string code);
    Task<AudienceModel> GetByIdAsync(int id);

    Task AddAsync(AudienceModel model);
    Task DeleteAsync(int id);
    Task UpdateAsync(AudienceModel model);
}