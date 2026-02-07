using System;

namespace JWTHLAPI.ModelLayer.Entities.Auth
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }
        public bool IsDefault { get; set; }

        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        public short IsActive { get; set; }
    }
}