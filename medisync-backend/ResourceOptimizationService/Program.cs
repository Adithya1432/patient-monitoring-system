using Microsoft.EntityFrameworkCore;
using ResourceOptimizationService.Data;
using ResourceOptimizationService.GrpcServices;
using ResourceOptimizationService.Interfaces;
using ResourceOptimizationService.Repositories;
using ResourceOptimizationService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ResourceOptimizationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IResourceOptimizationRepository, ResourceOptimizationRepository>();
builder.Services.AddScoped<IResourceOptimizationService, ResourceOptimizationsService>();
// Add services to the container.
builder.Services.AddGrpc();

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
app.MapGrpcService<ResourceOptimizationGrpcService>();
app.Run();
