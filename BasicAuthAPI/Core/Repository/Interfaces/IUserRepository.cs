using BasicAuthAPI.Core.Entities;

namespace BasicAuthAPI.Core.Repository.Interfaces;

public interface IUserRepository
{
    public Task<List<User>> GetAllUsers();
    public Task<User> CreateUser(User user);
}