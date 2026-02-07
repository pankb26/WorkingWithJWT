using System;

namespace JWTHLAPI.ModelLayer.Entities.Auth
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        
        public int IsActive { get; set; }
    }
}