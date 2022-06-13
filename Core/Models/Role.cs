using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Aumc.Core.Models
{
    public class Role: IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}