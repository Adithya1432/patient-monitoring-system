using DoctorAvailabilityService.Data;
using DoctorAvailabilityService.GrpcServices;
using DoctorAvailabilityService.Interfaces;
using DoctorAvailabilityService.Repositories;
using DoctorAvailabilityService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DoctorAvailabilityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IDoctorAvailabilityRepository, DoctorAvailabilityRepository>();
builder.Services.AddScoped<IDoctorAvailabilityService, DoctorAvailableService>();

builder.Services.AddGrpc();
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
app.MapGrpcService<DoctorAvailabilityGrpcService>();

app.Run();
