namespace JWTHLAPI.ModelLayer.DTO.Auth
{
    public class FormPermissionDto
    {
        public string FormName { get; set; }

        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }
}