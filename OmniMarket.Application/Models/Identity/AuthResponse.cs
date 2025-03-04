using System;
using System.Collections.Generic;
using System.Text;

namespace OmniMarket.Application.Models.Identity
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
