using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.Audience;

public interface IAudienceRepository
{
    Task<IEnumerable<AudienceModel>> GetAllAudiencesAsync();
    Task AddAudienceAsync(AudienceModel audience);
    event Action AudiencesChanged;
    Task DeleteAudienceAsync(int audienceId);
    Task UpdateAudienceAsync(AudienceModel audience);
    Task<AudienceModel?> GetAudienceByCodeAsync(string code);
}