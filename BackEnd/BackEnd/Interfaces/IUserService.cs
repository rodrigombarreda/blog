using BackEnd.Models;

namespace BackEnd.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        Task<User> AddUser(User user);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<bool> DeleteUser(Guid id);
    }
}
