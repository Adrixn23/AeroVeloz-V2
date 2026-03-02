namespace AeroVeloz.Domain.Models.UserSystem
{
    public class UserSystemModel
    {
        public Guid userId { get; }
        public string? nameUser { get; }
        public int idOrganization { get; }
        public string? typeOrganization { get; }
        public string? emailOrganization { get; }
        public bool isActiveUser { get; }
        public bool isActiveOrganization { get; }
    }
}
