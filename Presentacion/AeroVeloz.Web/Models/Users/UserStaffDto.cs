namespace AeroVeloz.Web.Models.Users
{
    public class UserStaffDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public short RoleId { get; set; }
    }

    public class CreateStaffDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
        public short RoleId { get; set; }
    }
}
