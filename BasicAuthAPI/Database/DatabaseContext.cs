using BasicAuthAPI.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthAPI.Database;

public class DatabaseContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) 
        : base(options)
    {
    }
}