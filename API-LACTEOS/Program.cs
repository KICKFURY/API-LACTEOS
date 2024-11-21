using Microsoft.EntityFrameworkCore;
using API_LACTEOS.Models;
using API_LACTEOS.Servicios;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var misReglasCors = "ReglasCors";
builder.Services.AddCors(opt => opt.AddPolicy(name: misReglasCors, builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LacteosBdContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));
builder.Services.AddScoped<ServiciosBD>();
builder.Services.AddScoped<Database>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(misReglasCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
