using JWTHLAPI.ModelLayer.DTO.Auth;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JWTHLAPI.DataLayer.Interfaces.Auth
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleResponseDto>> GetAll();
    }
}