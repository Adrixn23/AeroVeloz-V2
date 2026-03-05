using AeroVeloz.Domain.DomainServices.Interfaces.Organization;
using AeroVeloz.Domain.DomainServices.Interfaces.User.security;
using AeroVeloz.Domain.Events.Result;
using AeroVeloz.Application.DTOs.Users;

namespace AeroVeloz.Application.Services.Users
{
    public class UserAuthenticationHandler
    {
        private readonly IDomainServiceOrganization _domainServiceOrganization;
        private readonly IUserRepositoryAuthenticacion _userRepositoryAuthenticacion;
        private readonly IUserRepositoryAuthorization _userRepositoryAuthorization;

        //caso de uso de para la orquestacion del logueo de los usuarios mediante cualquier aplicativo correspondiente
        //el mismo valida y return el el operation result correspondiente para dar acceso al usuario segun la organizacion 
        //donde se encuentre para asi entocnes validar que tipo de usuario es, permissos, roles y demas componentes necesarios
        //

        public UserAuthenticationHandler(
            IDomainServiceOrganization domainServiceOrganization,
            IUserRepositoryAuthorization userRepositoryAuthorization,
            IUserRepositoryAuthenticacion userRepositoryAuthenticacion

            )
        {
            _domainServiceOrganization = domainServiceOrganization;
            _userRepositoryAuthorization = userRepositoryAuthorization;
            _userRepositoryAuthenticacion = userRepositoryAuthenticacion;
        }

        public async Task<OperationResult<UserLoginDto>> ExcuteAsyncLogin(string nameUser, string password, string emailOrganization)
        {

            //aqui va la orquestacion del flujo operational de los elementos pertinentes para return el operational result corersspondiente
            //que a su vez se encarga entocnes de realizar las notificacioens correspondientes a los interesados dentro del sistema y guardar los flujos de logs 
            //gestionados por el modulo de tranzabilidad del programa
            return null;
        }


    }
}
