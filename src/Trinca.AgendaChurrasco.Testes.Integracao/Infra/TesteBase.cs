using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using Trinca.AgendaChurrasco.Data;

namespace Trinca.AgendaChurrasco.Testes.Integracao.Infra;

[SetUpFixture]
public class TesteBase : IDisposable
{
    protected AgendaChurrascoDbContext DbContext = null!;
    protected IServiceProvider ServiceProvider = null!;
    protected HttpClient HttpClient = null!;
    private Respawner _respawnDb = null!; 
    private IntegrationTestWebApp _webApp = null!;
    private IServiceScope _scope = null!;
    
    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _webApp = new IntegrationTestWebApp();
        await _webApp.StartDatabase();
        _scope = _webApp.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<AgendaChurrascoDbContext>();
        await DbContext.Database.MigrateAsync();
        ServiceProvider = _scope.ServiceProvider;
        HttpClient = _webApp.CreateClient();
        _respawnDb = await Respawner.CreateAsync(DbContext.Database.GetConnectionString());
    }
    
    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _webApp.StopDatabase();
    }
    
    [TearDown]
    public async Task TearDown()
    {
        await _respawnDb.ResetAsync(DbContext.Database.GetConnectionString());
    }

    public void Dispose()
    {
        _scope?.Dispose();
        DbContext?.Dispose();
    }
}