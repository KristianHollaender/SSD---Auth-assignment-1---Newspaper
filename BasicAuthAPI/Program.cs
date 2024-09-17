using AutoMapper;
using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Database;

using BasicAuthAPI.DTOs.NewsDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("NewsDB");
    options.UseSqlite(connectionString);
});
builder.Services.AddScoped<DbSeeder>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

var mapper = new MapperConfiguration(configure =>
{
    configure.CreateMap<ArticleDto, Article>();
    configure.CreateMap<ArticleFormDto, Article>();

    configure.CreateMap<CommentDto, Comment>();
    configure.CreateMap<CommentFormDto, Comment>();
    
}).CreateMapper();

builder.Services.AddSingleton(mapper);

builder
    .Services.AddIdentityApiEndpoints<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>();

var app = builder.Build();

// Seed
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<DbSeeder>().SeedAsync().Wait();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<IdentityUser>();

app.Run();