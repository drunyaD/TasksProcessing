using TasksProcessing.DataAccess.EF.Entities;

namespace TasksProcessing.DataAccess.Interfaces;

public interface IUserRepository: IRepository<UserEntity, int>
{
    Task<UserEntity?> GetRandomUserAsync(int? currentUserId = default);

    Task<UserEntity?> FindByName(string name);
}
