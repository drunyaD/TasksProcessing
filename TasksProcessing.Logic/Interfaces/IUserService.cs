using TasksProcessing.Logic.DTO;
using TasksProcessing.Logic.Requests;

namespace TasksProcessing.Logic.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateAsync(CreateUserRequest createUserRequest);

    Task<UserDto?> GetAsync(int userId);

    IEnumerable<UserDto> GetAll();
}
