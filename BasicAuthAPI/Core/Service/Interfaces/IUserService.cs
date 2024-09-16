using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.DTOs;

namespace BasicAuthAPI.Core.Service.Interfaces;

public interface IUserService
{
    public Task<List<User>> GetAllUsers();
    public Task<User> CreateUser(CreateUserDTO user);
}