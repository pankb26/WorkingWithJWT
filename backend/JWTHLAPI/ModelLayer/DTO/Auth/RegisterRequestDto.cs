namespace JWTHLAPI.ModelLayer.DTO.Auth
{
    public class RegisterRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }   
    }
}