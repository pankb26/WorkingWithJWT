using System;
using System.Collections.Generic;

namespace JWTHLAPI.ModelLayer.DTO.Auth
{
    public class LoginResponse
    {
        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }

      //  public List<string> Roles { get; set; }
      public int RoleId { get; set; }
    }
}