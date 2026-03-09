using AeroVeloz.Domain.Entities.Users.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasData(
                // Permisos de organizaciones (solo SYSTEMADMIN)
                    new Permission { Id = 1, codePermision = "ORG_CREATE", description = "Crear organizaciones" },
                    new Permission { Id = 2, codePermision = "ORG_EDIT", description = "Editar organizaciones" },
                    new Permission { Id = 3, codePermision = "ORG_DEACTIVATE", description = "Desactivar organizaciones" },

                    // Permisos de usuarios (SYSTEMADMIN, AIRPORTADMIN, AIRLINEADMIN)
                    new Permission { Id = 4, codePermision = "USER_CREATE", description = "Crear usuarios" },
                    new Permission { Id = 5, codePermision = "USER_EDIT", description = "Editar usuarios" },
                    new Permission { Id = 6, codePermision = "USER_DEACTIVATE", description = "Desactivar usuarios" },

                    // Auditoría (roles ADMIN)
                    new Permission { Id = 7, codePermision = "AUDIT_VIEW", description = "Visualizar registros de auditoría" },

                    // Conexiones aeropuerto-aerolínea (AIRPORTADMIN)
                    new Permission { Id = 8, codePermision = "AIRPORT_CONN_VIEW", description = "Visualizar conexiones aeropuerto-aerolínea" },
                    new Permission { Id = 9, codePermision = "AIRPORT_CONN_CREATE", description = "Crear conexiones aeropuerto-aerolínea" },
                    new Permission { Id = 10, codePermision = "AIRPORT_CONN_EDIT", description = "Editar conexiones aeropuerto-aerolínea" },
                    new Permission { Id = 11, codePermision = "AIRPORT_CONN_DEACTIVATE", description = "Desactivar conexiones aeropuerto-aerolínea" },

                    // Operaciones (OPERATIONAIRPORT)
                    new Permission { Id = 12, codePermision = "OP_REGISTER", description = "Registrar cambios operacionales" },
                    new Permission { Id = 13, codePermision = "OP_VIEW", description = "Visualizar cambios operacionales" },
                    new Permission { Id = 14, codePermision = "FLIGHT_VIEW", description = "Visualizar vuelos" }
            );
        }
    }
}
