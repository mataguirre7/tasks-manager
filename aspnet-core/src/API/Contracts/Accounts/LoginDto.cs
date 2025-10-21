using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Definitions.Entities;

namespace API.Contracts.Accounts
{
    public class LoginDto : EntityDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? RememberMe { get; set; }
    }
}