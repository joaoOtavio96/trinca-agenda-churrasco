using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using Trinca.AgendaChurrasco.Data;

namespace Trinca.AgendaChurrasco.Testes.Integracao.Infra;

public class IntegrationTestWebApp : WebApplicationFactory<Program>
{
    private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .WithPassword("TestStrongPass#23@")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(DbContextOptions<AgendaChurrascoDbContext>);

            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == descriptorType);

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<AgendaChurrascoDbContext>(options =>
                options.UseSqlServer(_dbContainer.GetConnectionString()));
        });
    }
    
    public async Task StartDatabase()
    {
        await _dbContainer.StartAsync();
    }
    
    public async Task StopDatabase()
    {
        await _dbContainer.StopAsync();
    }
}