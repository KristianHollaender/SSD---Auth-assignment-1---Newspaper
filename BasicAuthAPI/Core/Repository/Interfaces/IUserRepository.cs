using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.DTOs;

namespace BasicAuthAPI.Core.Repository.Interfaces;

public interface IUserRepository
{
    public Task<List<User>> GetAllUsers();
    public Task<User> CreateUser(CreateUserDTO user);
    public Task DeleteUser(int userId);
    public Task<User> GetUserById(int userId);
}