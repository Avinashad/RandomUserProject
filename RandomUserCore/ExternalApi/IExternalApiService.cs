using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RandomUserCore.Models;
using RandomUserCore.Models.coreModels;
using RandomUserCore.Models.IntegrationModels;

namespace RandomUserCore.ExternalApi
{
    public interface IExternalApiService
    {
        Task<IEnumerable<UserEntity>> GetRandomUsers();
    }
}