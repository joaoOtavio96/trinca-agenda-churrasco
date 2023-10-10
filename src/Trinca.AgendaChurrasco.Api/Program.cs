using Microsoft.EntityFrameworkCore;
using Trinca.AgendaChurrasco.Data;
using Trinca.AgendaChurrasco.Data.Repository;
using Trinca.AgendaChurrasco.Domain.Repositories;
using Trinca.AgendaChurrasco.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AgendaChurrascoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddScoped<IChurrascoService, ChurrascoService>();
builder.Services.AddScoped<IChurrascoRepository, ChurrascoRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();