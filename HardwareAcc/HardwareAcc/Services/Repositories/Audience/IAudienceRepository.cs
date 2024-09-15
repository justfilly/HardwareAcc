using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.Models;

namespace HardwareAcc.Services.Repositories.Audience;

public interface IAudienceRepository
{
    Task<IEnumerable<AudienceModel>> GetAllAudiencesAsync();
    Task AddAudienceAsync(AudienceModel audience);
}