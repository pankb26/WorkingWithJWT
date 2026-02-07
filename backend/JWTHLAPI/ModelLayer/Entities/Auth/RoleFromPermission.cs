namespace JWTHLAPI.ModelLayer.Entities.Auth
{
    public class RoleFormPermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int FormId { get; set; }

        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }

        public int IsActive { get; set; }
    }
}