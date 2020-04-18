using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RandomUserCore.Models;

namespace RandomUserCore.ExternalApi
{
    public class ExternalApiService : IExternalApiService
    {
        private readonly ILogger<ExternalApiService> _logger;
        private readonly IMapper _mapper;

        public ExternalApiService(ILogger<ExternalApiService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserEntity>> GetRandomUsers()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://randomuser.me/api/");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    var response = await client.GetAsync($"?inc=name,email,dob,phone,picture&noinfo&exc=nat&results=20");

                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawRandomUser = JsonConvert.DeserializeObject<RandomUserModel>(stringResult);
                    return _mapper.Map<IEnumerable<UserEntity>>(rawRandomUser.Results);
                }
                catch (HttpRequestException httpRequestException)
                {
                    _logger.LogError(httpRequestException, $"Error getting users from RandomUserApi: {httpRequestException.Message}");
                    throw httpRequestException;
                }
            }
        }
    }
}