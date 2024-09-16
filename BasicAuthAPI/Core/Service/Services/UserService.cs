using AutoMapper;
using BasicAuthAPI.Core.Entities;
using BasicAuthAPI.Core.Repository.Interfaces;
using BasicAuthAPI.Core.Service.Interfaces;
using BasicAuthAPI.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BasicAuthAPI.Core.Service.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<User> CreateUser(CreateUserDTO dto)
    {
        var user = await _userRepository.CreateUser(_mapper.Map<CreateUserDTO>(dto));
        return user;
    }

    public Task DeleteUser(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(int userId)
    {
        throw new NotImplementedException();
    }
}