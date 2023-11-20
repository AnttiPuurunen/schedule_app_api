using Microsoft.EntityFrameworkCore;
using ScheduleAppApi.Models;

var AllowSpecificOrigins = "_allowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithMethods("GET", "POST", "PUT", "DELETE")
                          .SetIsOriginAllowed(origin =>
                          {
                            if (origin.ToLower().StartsWith("http://localhost")) return true;
                            return false;
                          })
                          .AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionStrings:ScheduleDb"];

builder.Services.AddDbContext<ScheduleContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(AllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
