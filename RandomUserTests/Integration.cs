
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RandomUserCore;
using RandomUserCore.Models;
using System.Net.Http;


namespace RandomUserTests
{
    public class Integration
    {
        protected readonly HttpClient TestClient;
        public Integration()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(RandomUserContext));
                        services.AddDbContext<RandomUserContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });
            TestClient = appFactory.CreateClient();
        }


    }
}
