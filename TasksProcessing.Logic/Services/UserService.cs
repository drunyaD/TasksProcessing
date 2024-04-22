using TasksProcessing.DataAccess.EF.Entities;
using TasksProcessing.DataAccess.Interfaces;
using TasksProcessing.Logic.DTO;
using TasksProcessing.Logic.Exceptions;
using TasksProcessing.Logic.Interfaces;
using TasksProcessing.Logic.Requests;

namespace TasksProcessing.Logic.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto> CreateAsync(CreateUserRequest createUserRequest)
    {
        var existingUser = await _unitOfWork.Users.FindByName(createUserRequest.Name);
        if (existingUser is not null)
        {
            throw new LogicException($"User with name '{createUserRequest.Name}' already exists");
        }

        var allTasks = _unitOfWork.Tasks.GetAll();
        var newUser = new UserEntity
        {
            Name = createUserRequest.Name,
            TasksToWork = allTasks.ToList()
        };

        await _unitOfWork.Users.CreateAsync(newUser);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(newUser);
    }

    public async Task<UserDto?> GetAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetAsync(userId);
        if (user is null)
        {
            return null;
        }
        else
        {
            return MapToDto(user);
        }
    }

    public IEnumerable<UserDto> GetAll()
    {
        var users = _unitOfWork.Users.GetAll();
        return users.Select(MapToDto);
    }

    private UserDto MapToDto(UserEntity userEntity)
    {
        return new UserDto(userEntity.Id, userEntity.Name);
    }
}
