using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.GrpcServices;
using UserService.Interfaces;
using UserService.Repositories;
using UserService.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core; // Add this using
using Grpc.AspNetCore.Server; // Add this using


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UsersService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add gRPC services
builder.Services.AddGrpc();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<UserGrpcService>();
app.Run();
