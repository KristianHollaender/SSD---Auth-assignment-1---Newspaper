using System.Data.Common;
using System.Security.Cryptography;
using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Core.Repository.Interfaces;
using BasicAuthAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace BasicAuthAPI.Core.Repository.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public string HashPassword(string password)  
    {  
        // Create a salt  
        byte[] salt = new byte[16];  
        using (var rng = RandomNumberGenerator.Create())  
        {  
            rng.GetBytes(salt);  
        }  
  
        // Create the Rfc2898DeriveBytes and get the hash value  
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))  
        {  
            byte[] hash = pbkdf2.GetBytes(20);  
  
            // Combine the salt and password bytes for later use  
            byte[] hashBytes = new byte[36];  
            Array.Copy(salt, 0, hashBytes, 0, 16);  
            Array.Copy(hash, 0, hashBytes, 16, 20);  
  
            // Turn the combined salt+hash into a string for storage  
            string savedPasswordHash = Convert.ToBase64String(hashBytes);  
            return savedPasswordHash;  
        }  
    }  

    
    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> CreateUser(User user)  
    {  
        var passwordHash = HashPassword(user.PasswordHash);  
  
        var newUser = new User  
        {  
            Username = user.Username,  
            PasswordHash = passwordHash,  
        };  
  
        await _context.Users.AddAsync(user);  
        
        await _context.SaveChangesAsync();  
  
        return user;  
    }  

}