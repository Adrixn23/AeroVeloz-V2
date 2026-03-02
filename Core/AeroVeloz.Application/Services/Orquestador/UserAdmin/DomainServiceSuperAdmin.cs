using AeroVeloz.Application.Repositories.UseAdmin;

namespace AeroVeloz.Application.Services.Orquestador.UserAdmin
{
    public class DomainServiceSuperAdmin
    {
        public IUserAdminRepository _userAdminRepository;
        
        public DomainServiceSuperAdmin(IUserAdminRepository userAdminRepository) { 
                _userAdminRepository = userAdminRepository;
        }



    }
}
