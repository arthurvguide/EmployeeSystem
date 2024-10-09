using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSystem.Data
{
    // This is your application database context for Identity
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        // Constructor accepting options and passing to the base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add any DbSets here if needed (for example, if you want to extend Identity models or add more tables)
    }
}