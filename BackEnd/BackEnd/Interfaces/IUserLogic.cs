using BackEnd.Models;

namespace BackEnd.Interfaces
{
    public interface IUserLogic
    {
        Task<LoginResponse> Login(string email, string password);
        Task<User> Register(string firstName, string lastName, string email, string password);
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(Guid id);
        Task<bool> DeleteUser(Guid id);
    }
}
