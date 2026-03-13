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

            migrationBuilder.InsertData(
                schema: "Audits",
                table: "AuditType",
                columns: new[] { "Id", "nameAudit" },
                values: new object[,]
                {
                    { (short)1, "ENTITY_CREATE" },
                    { (short)2, "ENTITY_UPDATE" },
                    { (short)3, "ENTITY_DEACTIVATE" }
                });

            migrationBuilder.InsertData(
                schema: "Operations",
                table: "OperationalChangeType",
                columns: new[] { "Id", "name" },
                values: new object[,]
                {
                    { (short)1, "GATE_CHANGE" },
                    { (short)2, "FLIGHT_DELAY" },
                    { (short)3, "FLIGHT_CANCELLATION" }
                });

            migrationBuilder.InsertData(
                schema: "Identitys",
                table: "Permissions",
                columns: new[] { "Id", "codePermision", "description" },
                values: new object[,]
                {
                    { (byte)1, "ORG_CREATE", "Crear organizaciones" },
                    { (byte)2, "ORG_EDIT", "Editar organizaciones" },
                    { (byte)3, "ORG_DEACTIVATE", "Desactivar organizaciones" },
                    { (byte)4, "USER_CREATE", "Crear usuarios" },
                    { (byte)5, "USER_EDIT", "Editar usuarios" },
                    { (byte)6, "USER_DEACTIVATE", "Desactivar usuarios" },
                    { (byte)7, "AUDIT_VIEW", "Visualizar registros de auditoría" },
                    { (byte)8, "AIRPORT_CONN_VIEW", "Visualizar conexiones aeropuerto-aerolínea" },
                    { (byte)9, "AIRPORT_CONN_CREATE", "Crear conexiones aeropuerto-aerolínea" },
                    { (byte)10, "AIRPORT_CONN_EDIT", "Editar conexiones aeropuerto-aerolínea" },
                    { (byte)11, "AIRPORT_CONN_DEACTIVATE", "Desactivar conexiones aeropuerto-aerolínea" },
                    { (byte)12, "OP_REGISTER", "Registrar cambios operacionales" },
                    { (byte)13, "OP_VIEW", "Visualizar cambios operacionales" },
                    { (byte)14, "FLIGHT_VIEW", "Visualizar vuelos" }
                });

            migrationBuilder.InsertData(
                schema: "Identitys",
                table: "RolPermissions",
                columns: new[] { "Id", "idPermission", "idRol" },
                values: new object[,]
                {
                    { (short)1, (short)1, (short)1 },
                    { (short)2, (short)2, (short)1 },
                    { (short)3, (short)3, (short)1 },
                    { (short)4, (short)4, (short)1 },
                    { (short)5, (short)5, (short)1 },
                    { (short)6, (short)6, (short)1 },
                    { (short)7, (short)7, (short)1 },
                    { (short)8, (short)4, (short)2 },
                    { (short)9, (short)5, (short)2 },
                    { (short)10, (short)6, (short)2 },
                    { (short)11, (short)7, (short)2 },
                    { (short)12, (short)8, (short)2 },
                    { (short)13, (short)9, (short)2 },
                    { (short)14, (short)10, (short)2 },
                    { (short)15, (short)11, (short)2 },
                    { (short)16, (short)4, (short)3 },
                    { (short)17, (short)5, (short)3 },
                    { (short)18, (short)6, (short)3 },
                    { (short)19, (short)7, (short)3 },
                    { (short)20, (short)12, (short)4 },
                    { (short)21, (short)13, (short)4 },
                    { (short)22, (short)14, (short)4 }
                });

            migrationBuilder.InsertData(
                schema: "Identitys",
                table: "Rol",
                columns: new[] { "Id", "nameRol" },
                values: new object[,]
                {
                    { (short)1, "SYSTEMADMIN" },
                    { (short)2, "AIRPORTADMIN" },
                    { (short)3, "AIRLINEADMIN" },
                    { (short)4, "OPERATIONAIRPORT" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)6);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)7);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)8);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)9);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)10);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)11);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)12);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)13);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)14);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)15);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)16);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)17);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)18);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)19);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)20);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)21);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "RolPermissions",
                keyColumn: "Id",
                keyValue: (short)22);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Rol",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Rol",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Rol",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Rol",
                keyColumn: "Id",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)3);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)4);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)5);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)6);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)7);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)8);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)9);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)10);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)11);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)12);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)13);

            migrationBuilder.DeleteData(
                schema: "Identitys",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: (byte)14);

            migrationBuilder.DeleteData(
                schema: "Audits",
                table: "AuditType",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                schema: "Audits",
                table: "AuditType",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                schema: "Audits",
                table: "AuditType",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                schema: "Operations",
                table: "OperationalChangeType",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                schema: "Operations",
                table: "OperationalChangeType",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                schema: "Operations",
                table: "OperationalChangeType",
                keyColumn: "Id",
                keyValue: (short)3);
        }
    }
}
