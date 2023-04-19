using cms.Context;
using cms.Repositories;
using cms.Services;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// add connection database
builder.Services.AddTransient<MySqlConnection>(_ => 
    new MySqlConnection(builder.Configuration.GetConnectionString("mysql")));

// add DB context
builder.Services.AddDbContext<ContextCMS>(options => options.UseMySQL(builder.Configuration.GetConnectionString("mysql")));

// add repository layer
builder.Services.AddScoped<accountRepository>();

// add service layer
builder.Services.AddScoped<accountService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
