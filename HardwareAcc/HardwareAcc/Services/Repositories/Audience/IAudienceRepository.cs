using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.Audience;

public interface IAudienceRepository
{
    event Action AudiencesChanged;
    
    Task<IEnumerable<AudienceModel>> GetAllAudiencesAsync();
    Task<AudienceModel> GetAudienceByCodeAsync(string code);
    
    Task AddAudienceAsync(AudienceModel model);
    Task DeleteAudienceAsync(int id);
    Task UpdateAudienceAsync(AudienceModel model);
}