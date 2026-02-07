using JWTHLAPI.ModelLayer.Entities.Auth;
using System.Threading.Tasks;

namespace JWTHLAPI.DataLayer.Interfaces.Auth
{
    public interface IUserRepository
    {
        Task<User> GetByUsername(string userName);
        Task<bool> IsUsernameExists(string userName);
        Task<int> Create(User user);
        Task<int> GetDefaultRoleId();
    }
}