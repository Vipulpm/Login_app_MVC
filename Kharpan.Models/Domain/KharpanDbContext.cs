using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kharpan.Models.Domain
{
    public class KharpanDbContext : IdentityDbContext<ApplicationUser>
    {
        public KharpanDbContext(DbContextOptions<KharpanDbContext>options) : base(options)
        {
            
        }
    }
}
