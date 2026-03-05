using AeroVeloz.Application.Repositories.Base;
using AeroVeloz.Domain.Entities.Users.User;
using AeroVeloz.Domain.Models.Users;
using AeroVeloz.Domain.Models.UserSystem;

namespace AeroVeloz.Application.Repositories.Users
{
    public interface IUserRepository : IBRepository<User, Guid>
    {
        //esta interfaz contiene los elementos basicos de consulta para el modulo de usuarios permitiendo obtener
        //los diversos usuarios dentro del  organizmo x y tambien obtener la informacion de x usuario  dentro de x organizacion 

        Task<IReadOnlyCollection<UserDetailModel>> GetUserByOrganizationsId(Guid userId, int orgId);
        Task<UserSystemModel> GetByUserName(string nameUser);   //agregar elemento para sacar usuario solamente de una organizacion correspondiente 

    }
}
