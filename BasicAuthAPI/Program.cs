using AutoMapper;
using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Core.Repository.Interfaces;
using BasicAuthAPI.Core.Repository.Repositories;
using BasicAuthAPI.Core.Service.Interfaces;
using BasicAuthAPI.Core.Service.Services;
using BasicAuthAPI.Database;
using BasicAuthAPI.DTOs;
using BasicAuthAPI.DTOs.NewsDTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapper = new MapperConfiguration(configure =>
{
    configure.CreateMap<CreateUserDTO, User>();
    
    configure.CreateMap<CreateArticleDTO, Article>();
    configure.CreateMap<EditArticleDTO, Article>();
    
    configure.CreateMap<CreateCommentDTO, Comment>();
    configure.CreateMap<EditCommentDTO, Comment>();
}).CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();