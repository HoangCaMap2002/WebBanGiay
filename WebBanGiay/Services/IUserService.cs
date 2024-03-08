using WebBanGiay.Models;
using WebBanGiay.ViewModels;

namespace WebBanGiay.Services
{
    public interface IUserService
    {
        Task<User> GetUserByUserNameAsync(string username);
        Task<List<string>> GetAllNameRoleUserAsync(int id);
        Task<bool> Regiter(RegisterVM rvm); 
    }
}
