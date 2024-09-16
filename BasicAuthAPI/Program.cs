using AutoMapper;
using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Core.Repository.Interfaces;
using BasicAuthAPI.Core.Repository.Repositories;
using BasicAuthAPI.Core.Service.Interfaces;
using BasicAuthAPI.Core.Service.Services;
using BasicAuthAPI.Database;
using BasicAuthAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);

var mapper = new MapperConfiguration(configure => { configure.CreateMap<CreateUserDTO, User>(); }).CreateMapper();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(mapper);
builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();