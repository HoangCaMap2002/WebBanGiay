using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;
using WebBanGiay.Models;
using WebBanGiay.ViewModels;

namespace WebBanGiay.Services
{
    public class UserService : IUserService
    {
        private readonly QlbanGiayContext _dbcontext;
        public UserService(QlbanGiayContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<string>> GetAllNameRoleUserAsync(int id)
        {
            try
            {
                var usersWithTypes = await (from u in _dbcontext.Users
                                            join tu in _dbcontext.UserTypeUsers on u.UserId equals tu.UserId
                                            join t in _dbcontext.TypeUsers on tu.TypeUserId equals t.TypeUserId
                                            where u.UserId == id
                                            select t.NameTyPe).ToListAsync();
                if (usersWithTypes!= null)
                {
                    return usersWithTypes;
                }
                return null;
            }
            catch(Exception e) { 
                return null;
            }

        }

        public async Task<User> GetUserByUserNameAsync(string username)
        { 
            try
            {
                User? user = await _dbcontext.Users.SingleOrDefaultAsync(u => u.Username == username);
                if (user != null)
                {
                    return user;
                }
                else return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public async Task<bool> Regiter(RegisterVM rvm)
        {
            var existingUser = _dbcontext.Users.FirstOrDefault(u => u.Username == rvm.Username);

            if (existingUser != null)
            {
                return false; // Người dùng đã tồn tại
            }

            string plainPassword = rvm.Password;
            var passwordHasher = new PasswordHasher();
            string hashedPassword = passwordHasher.HashPassword(plainPassword);
                User user = new User()
                {
                    Username = rvm.Username,
                    Password = hashedPassword,
                    Name = rvm.Name,
                    Email = rvm.Email,
                };
                _dbcontext.Add(user);
                await _dbcontext.SaveChangesAsync();
            UserTypeUser userTypeUser = new UserTypeUser() {
                UserId = user.UserId,
                TypeUserId = 1
                };
            _dbcontext.Add(userTypeUser);
            await _dbcontext.SaveChangesAsync();
            return true; // Đăng ký thành công
        }
    }
}
