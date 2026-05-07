using Microsoft.EntityFrameworkCore.Migrations;


namespace AeroVeloz.Infraestructure.Persistence.Migrations
{
    public partial class SeedRolesPermissionsAuditOperationalTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Audits");

            migrationBuilder.EnsureSchema(
                name: "Operations");

            migrationBuilder.EnsureSchema(
                name: "Identitys");

            migrationBuilder.Sql(
                """
                IF NOT EXISTS (SELECT 1 FROM [Audits].[AuditType] WHERE [idAuditType] = 1) INSERT INTO [Audits].[AuditType] ([idAuditType],[nameAudit]) VALUES (1,'ENTITY_CREATE');
                IF NOT EXISTS (SELECT 1 FROM [Audits].[AuditType] WHERE [idAuditType] = 2) INSERT INTO [Audits].[AuditType] ([idAuditType],[nameAudit]) VALUES (2,'ENTITY_UPDATE');
                IF NOT EXISTS (SELECT 1 FROM [Audits].[AuditType] WHERE [idAuditType] = 3) INSERT INTO [Audits].[AuditType] ([idAuditType],[nameAudit]) VALUES (3,'ENTITY_DEACTIVATE');
                """);

            migrationBuilder.Sql(
                """
                IF NOT EXISTS (SELECT 1 FROM [Operations].[OperationalChangeType] WHERE [Id] = 1) INSERT INTO [Operations].[OperationalChangeType] ([Id],[name]) VALUES (1,'GATE_CHANGE');
                IF NOT EXISTS (SELECT 1 FROM [Operations].[OperationalChangeType] WHERE [Id] = 2) INSERT INTO [Operations].[OperationalChangeType] ([Id],[name]) VALUES (2,'FLIGHT_DELAY');
                IF NOT EXISTS (SELECT 1 FROM [Operations].[OperationalChangeType] WHERE [Id] = 3) INSERT INTO [Operations].[OperationalChangeType] ([Id],[name]) VALUES (3,'FLIGHT_CANCELLATION');
                """);

            migrationBuilder.Sql(
                """
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 1) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (1,'ORG_CREATE','Crear organizaciones');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 2) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (2,'ORG_EDIT','Editar organizaciones');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 3) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (3,'ORG_DEACTIVATE','Desactivar organizaciones');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 4) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (4,'USER_CREATE','Crear usuarios');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 5) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (5,'USER_EDIT','Editar usuarios');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 6) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (6,'USER_DEACTIVATE','Desactivar usuarios');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 7) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (7,'AUDIT_VIEW','Visualizar registros de auditoría');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 8) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (8,'AIRPORT_CONN_VIEW','Visualizar conexiones aeropuerto-aerolínea');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 9) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (9,'AIRPORT_CONN_CREATE','Crear conexiones aeropuerto-aerolínea');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 10) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (10,'AIRPORT_CONN_EDIT','Editar conexiones aeropuerto-aerolínea');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 11) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (11,'AIRPORT_CONN_DEACTIVATE','Desactivar conexiones aeropuerto-aerolínea');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 12) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (12,'OP_REGISTER','Registrar cambios operacionales');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 13) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (13,'OP_VIEW','Visualizar cambios operacionales');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Permissions] WHERE [Id] = 14) INSERT INTO [Identitys].[Permissions] ([Id],[codePermision],[description]) VALUES (14,'FLIGHT_VIEW','Visualizar vuelos');
                """);

            migrationBuilder.Sql(
                """
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Rol] WHERE [Id] = 1) INSERT INTO [Identitys].[Rol] ([Id],[nameRol]) VALUES (1,'SYSTEMADMIN');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Rol] WHERE [Id] = 2) INSERT INTO [Identitys].[Rol] ([Id],[nameRol]) VALUES (2,'AIRPORTADMIN');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Rol] WHERE [Id] = 3) INSERT INTO [Identitys].[Rol] ([Id],[nameRol]) VALUES (3,'AIRLINEADMIN');
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[Rol] WHERE [Id] = 4) INSERT INTO [Identitys].[Rol] ([Id],[nameRol]) VALUES (4,'OPERATIONAIRPORT');
                """);

            migrationBuilder.Sql(
                """
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 1) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (1,1,1);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 2) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (2,1,2);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 3) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (3,1,3);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 4) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (4,1,4);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 5) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (5,1,5);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 6) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (6,1,6);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 7) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (7,1,7);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 8) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (8,2,4);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 9) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (9,2,5);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 10) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (10,2,6);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 11) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (11,2,7);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 12) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (12,2,8);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 13) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (13,2,9);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 14) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (14,2,10);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 15) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (15,2,11);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 16) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (16,3,4);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 17) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (17,3,5);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 18) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (18,3,6);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 19) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (19,3,7);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 20) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (20,4,12);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 21) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (21,4,13);
                IF NOT EXISTS (SELECT 1 FROM [Identitys].[RolPermissions] WHERE [idRolPermission] = 22) INSERT INTO [Identitys].[RolPermissions] ([idRolPermission],[idRol],[idPermission]) VALUES (22,4,14);
                """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DELETE FROM [Identitys].[RolPermissions] WHERE [idRolPermission] BETWEEN 1 AND 22;
                DELETE FROM [Identitys].[Rol] WHERE [Id] IN (1,2,3,4);
                DELETE FROM [Identitys].[Permissions] WHERE [Id] BETWEEN 1 AND 14;
                DELETE FROM [Audits].[AuditType] WHERE [idAuditType] IN (1,2,3);
                DELETE FROM [Operations].[OperationalChangeType] WHERE [Id] IN (1,2,3);
                """);
        }
    }
}
