using AeroVeloz.Application.Repositories.Users;
using AeroVeloz.Domain.Common.Exceptions;
using AeroVeloz.Domain.Models.Users;
using AeroVeloz.Domain.Models.UserSystem;
using AeroVeloz.Infraestructure.Persistence.context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AeroVeloz.Domain.DomainServices.Interfaces.User;
using Microsoft.Extensions.Logging;

namespace AeroVeloz.Infraestructure.Persistence.Repositories.User
{
    public class UserRepository : IUserRepository, IDomainServiceUser
    {

        private readonly AeroVelozContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AeroVelozContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> CreateEntity(Domain.Entities.Users.User.User entity)
        {
            try
            {
                _context.Users.Add(entity);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la entidad de usuario");
                throw new DatabaseOperationException("No se pudo crear el usuario debido a un error en la base de datos.", ex);
            }
        }

        public async Task<bool> DeleteEntity(Domain.Entities.Users.User.User entity)
        {
            try
            {
                var result = await _context.Users.Where(us => us.Id == entity.Id)
                    .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.isActive, false)
                    );

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar (eliminar) la entidad de usuario {Id}", entity.Id);
                throw new DatabaseOperationException("No se pudo desactivar el usuario debido a un error en la base de datos.", ex);
            }
        }
        public async Task<UserSystemModel> GetByUserName(string nameUser, int orgId) 
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.nameUser == nameUser && u.idOrganization == orgId );
                Console.WriteLine(user!.nameUser);
                if (user != null)
                {
                   return new UserSystemModel(
                        user!.Id,
                        user.nameUser,
                        (bool)user.isActive!,
                        user.failedLoginAttempts ?? 0,
                        user.lockedUntil
                    );
                }
                return null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consultando usuario {NameUser} en org {OrgId}", nameUser, orgId);
                return null!;
            }
        }

        public async Task<IReadOnlyCollection<UserDetailModel>> GetUserByOrganizationsId( int orgId)
        {
            try
            {
                var users = await (
                     from u in _context.Users.AsNoTracking()
                     join r in _context.Roles.AsNoTracking()
                     on u.idRol  equals r.Id
                     join o in _context.Organizations.AsNoTracking()
                     on  u.idOrganization equals o.Id
                     where u.idOrganization == orgId
                     select  new UserDetailModel(u.Id, u.nameUser, u.nameUser, o.nameOrganization, o.typeOrganization, u.isActive, u.lockedUntil.HasValue && u.lockedUntil.Value > DateTime.UtcNow, r.nameRol , u.createAt )

                    ).ToListAsync();
                if (users.Any())
                    return users;

                return Array.Empty<UserDetailModel>();  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo usuarios de la organización {OrgId}", orgId);
                return Array.Empty<UserDetailModel>();
            }
        }


        public async Task<bool> UpdateEntity(Domain.Entities.Users.User.User entity)
        {
            try
            {
                var hasher = new PasswordHasher<Domain.Entities.Users.User.User>();

                if (string.IsNullOrWhiteSpace(entity.passwordHash))
                {
                    var currentUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == entity.Id);
                    if (currentUser != null)
                    {
                        entity = new Domain.Entities.Users.User.User
                        {
                            Id = entity.Id,
                            nameUser = entity.nameUser,
                            passwordHash = currentUser.passwordHash, 
                            isActive = entity.isActive,
                            idRol = entity.idRol,
                            idOrganization = entity.idOrganization,
                            createAt = currentUser.createAt,
                            failedLoginAttempts = currentUser.failedLoginAttempts,
                            lastLoginAt = currentUser.lastLoginAt,
                            lockedUntil = currentUser.lockedUntil
                        };
                    }
                }
                else
                {
                    string hash = hasher.HashPassword(null!, entity.passwordHash);
                    entity = new Domain.Entities.Users.User.User
                    {
                        Id = entity.Id,
                        nameUser = entity.nameUser,
                        passwordHash = hash,
                        isActive = entity.isActive,
                        idRol = entity.idRol,
                        idOrganization = entity.idOrganization,
                        createAt = (await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == entity.Id))?.createAt ?? DateTime.UtcNow,
                        failedLoginAttempts = (await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == entity.Id))?.failedLoginAttempts,
                        lastLoginAt = (await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == entity.Id))?.lastLoginAt,
                        lockedUntil = (await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == entity.Id))?.lockedUntil
                    };
                }

                var result = await _context.Users.Where(us => us.Id == entity.Id)
                    .ExecuteUpdateAsync(setters => setters
                      .SetProperty(u => u.nameUser, entity.nameUser)
                      .SetProperty(u => u.isActive, entity.isActive)
                      .SetProperty(u => u.passwordHash, entity.passwordHash)
                      .SetProperty(u => u.idOrganization, entity.idOrganization)
                      .SetProperty(u => u.idRol, entity.idRol));

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando la entidad de usuario {Id}", entity.Id);
                throw new DatabaseOperationException("No se pudo actualizar el usuario debido a un error en la base de datos.", ex);
            }
        }

        public async Task<bool> ExistActiveUserAsync(Guid userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null) return false;
                return user.isActive;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verificando si el usuario {UserId} está activo", userId);
                return false;
            }
        }

        public async Task<bool> UserNameExistOrganization(string? userName, int orgId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.nameUser == userName && u.idOrganization == orgId);
                return user != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verificando existencia del userName {UserName} en org {OrgId}", userName, orgId);
                return false;
            }
        }
    }
}
