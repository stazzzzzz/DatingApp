using System.Threading.Tasks;
using DatingAppapi.Models;

namespace DatingAppapi.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user,string password);

         Task<User> Login(string userName,string password);

         Task<bool> UserExist(string userName);
    }
}