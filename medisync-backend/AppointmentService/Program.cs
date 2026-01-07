using AppointmentService.Data;
using AppointmentService.Interfaces;
using AppointmentService.Services;
using AppointmentService.Repositories;
using Microsoft.EntityFrameworkCore;
using Shared.Protos.User;
using Shared.Protos.DoctorAvailability;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppointmentDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentsService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpcClient<DoctorUserService.DoctorUserServiceClient>(o =>
    {
        o.Address = new Uri("https://localhost:7006");
    });

builder.Services.AddGrpcClient<DoctorAvailabilityCheckService.DoctorAvailabilityCheckServiceClient>(o =>
    {
        o.Address = new Uri("https://localhost:7003");
    });
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
