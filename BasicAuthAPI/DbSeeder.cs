using BasicAuthAPI.Core;
using BasicAuthAPI.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace BasicAuthAPI.Database;


public class DbSeeder
{
    private readonly DatabaseContext databaseContext;
    private readonly ILogger<DbSeeder> logger;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<IdentityUser> userManager;

    public DbSeeder(
        ILogger<DbSeeder> logger,
        DatabaseContext databaseContext,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        this.logger = logger;
        this.databaseContext = databaseContext;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task SeedAsync()
    {
        databaseContext.Database.EnsureDeleted();
        databaseContext.Database.EnsureCreated();

        var editor = await CreateUser("editor", "S3cret!", Roles.Editor);
        var writer = await CreateUser("writer", "S3cret!", Roles.Writer);
        var anotherWriter = await CreateUser("anotherWriter", "S3cret!", Roles.Writer);
        var subscriber = await CreateUser("subscriber", "S3cret!", Roles.Subscriber);
        var article1 = databaseContext
            .Articles.Add(
                new Article
                {
                    Title = "First article",
                    Content = "Breaking news",
                    Author = writer
                }
            )
            .Entity;
        var article2 = databaseContext
            .Articles.Add(
                new Article
                {
                    Title = "Second article",
                    Content = "Another breaking news",
                    Author = anotherWriter
                }
            )
            .Entity;
        databaseContext.Comments.Add(
            new Comment
            {
                Content = "First comment",
                Author = subscriber,
                Article = article1
            }
        );
        databaseContext.Comments.Add(
            new Comment
            {
                Content = "I'm a troll",
                Author = subscriber,
                Article = article2
            }
        );

        databaseContext.SaveChanges();
    }

    private async Task<IdentityUser> CreateUser(string username, string password, string role)
    {
        await roleManager.CreateAsync(new IdentityRole(role));
        var user = new IdentityUser
        {
            UserName = username,
            Email = username,
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            foreach (var error in result.Errors)
                logger.LogWarning("{Code}: {Description}", error.Code, error.Description);
        user = await userManager.FindByNameAsync(username);
        if (user != null)
            await userManager.AddToRoleAsync(user!, role!);
        return user!;
    }
}