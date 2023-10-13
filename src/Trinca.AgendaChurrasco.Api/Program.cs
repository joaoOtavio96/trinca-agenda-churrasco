using Microsoft.EntityFrameworkCore;
using Trinca.AgendaChurrasco.Data;
using Trinca.AgendaChurrasco.Data.Interceptors;
using Trinca.AgendaChurrasco.Data.Repository;
using Trinca.AgendaChurrasco.Domain.Churrascos;
using Trinca.AgendaChurrasco.Domain.Participantes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AgendaChurrascoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
        .AddInterceptors(new SoftDeleteInterceptor())
        .AddInterceptors(new DataCriacaoInterceptor())
    );

builder.Services.AddScoped<IChurrascoService, ChurrascoService>();
builder.Services.AddScoped<IChurrascoRepository, ChurrascoRepository>();
builder.Services.AddScoped<IParticipanteService, ParticipanteService>();
builder.Services.AddScoped<IParticipanteRepository, ParticipanteRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }