using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample_webapi.Models
{
    public class ProductDBContext : IdentityDbContext<ApplicationUser>
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options)
            :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
