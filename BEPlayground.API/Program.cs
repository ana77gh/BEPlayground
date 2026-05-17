using BEPlayground.Infrastructure.Data;
using BEPlayground.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using BEPlayground.Application.Common.Behaviours;
using FluentValidation;
using BEPlayground.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(cfg => 
    { cfg.RegisterServicesFromAssembly(
        Assembly.Load("BEPlayground.Application")); 
    });
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppDbContext>());
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BEPlaygroundDemoDb"));

// -- Pipeline order important! (outer->inner)
// Add logging behaviour to MediatR pipeline
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
// Add validation behavior to MediatR pipeline
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
// Register validator
builder.Services.AddValidatorsFromAssembly(Assembly.Load("BEPlayground.Application"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.Run();

