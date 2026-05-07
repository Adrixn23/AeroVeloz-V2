using AeroVeloz.Domain.Entities.Users.RolesPermision;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AeroVeloz.Infraestructure.Persistence.Configurations
{
    public class RolPermissionConfiguration : IEntityTypeConfiguration<RolPermission>
    {
        public void Configure(EntityTypeBuilder<RolPermission> builder)
        {
            builder.HasData(
                // SYSTEMADMIN: organizaciones + usuarios + auditoría
                new RolPermission { idRolPermission = 1, idRol = 1, idPermission = 1 },   // ORG_CREATE
                new RolPermission { idRolPermission = 2, idRol = 1, idPermission = 2 },   // ORG_EDIT
                new RolPermission { idRolPermission = 3, idRol = 1, idPermission = 3 },   // ORG_DEACTIVATE
                new RolPermission { idRolPermission = 4, idRol = 1, idPermission = 4 },   // USER_CREATE
                new RolPermission { idRolPermission = 5, idRol = 1, idPermission = 5 },   // USER_EDIT
                new RolPermission { idRolPermission = 6, idRol = 1, idPermission = 6 },   // USER_DEACTIVATE
                new RolPermission { idRolPermission = 7, idRol = 1, idPermission = 7 },   // AUDIT_VIEW

                // AIRPORTADMIN: usuarios + auditoría + conexiones aeropuerto
                new RolPermission { idRolPermission = 8, idRol = 2, idPermission = 4 },   // USER_CREATE
                new RolPermission { idRolPermission = 9, idRol = 2, idPermission = 5 },   // USER_EDIT
                new RolPermission { idRolPermission = 10, idRol = 2, idPermission = 6 },  // USER_DEACTIVATE
                new RolPermission { idRolPermission = 11, idRol = 2, idPermission = 7 },  // AUDIT_VIEW
                new RolPermission { idRolPermission = 12, idRol = 2, idPermission = 8 },  // AIRPORT_CONN_VIEW
                new RolPermission { idRolPermission = 13, idRol = 2, idPermission = 9 },  // AIRPORT_CONN_CREATE
                new RolPermission { idRolPermission = 14, idRol = 2, idPermission = 10 }, // AIRPORT_CONN_EDIT
                new RolPermission { idRolPermission = 15, idRol = 2, idPermission = 11 }, // AIRPORT_CONN_DEACTIVATE

                // AIRLINEADMIN: usuarios + auditoría
                new RolPermission { idRolPermission = 16, idRol = 3, idPermission = 4 },  // USER_CREATE
                new RolPermission { idRolPermission = 17, idRol = 3, idPermission = 5 },  // USER_EDIT
                new RolPermission { idRolPermission = 18, idRol = 3, idPermission = 6 },  // USER_DEACTIVATE
                new RolPermission { idRolPermission = 19, idRol = 3, idPermission = 7 },  // AUDIT_VIEW

                // OPERATIONAIRPORT: operaciones + vuelos
                new RolPermission { idRolPermission = 20, idRol = 4, idPermission = 12 }, // OP_REGISTER
                new RolPermission { idRolPermission = 21, idRol = 4, idPermission = 13 }, // OP_VIEW
                new RolPermission { idRolPermission = 22, idRol = 4, idPermission = 14 }  // FLIGHT_VIEW
            );
        }
    }
}
