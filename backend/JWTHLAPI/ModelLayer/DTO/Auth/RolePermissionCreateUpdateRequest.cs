namespace JWTHLAPI.ModelLayer.DTO.Auth
{
    public class RolePermissionCreateUpdateRequest
    {
        public int RoleId { get; set; }
        public int FormId { get; set; }

        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}