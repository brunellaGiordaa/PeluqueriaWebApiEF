using Microsoft.EntityFrameworkCore;
using PeluqueriaDLL.Data.Models;
using PeluqueriaDLL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PeluqueriaDbContext>(options =>
options
.UseSqlServer(builder.Configuration
.GetConnectionString("DefaultConnection")));//Inyecta la conexion con un scoped.

builder.Services.AddScoped<IServicioRepository, ServicioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
