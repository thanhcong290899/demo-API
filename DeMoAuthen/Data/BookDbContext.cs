using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeMoAuthen.Data
{
    public class BookDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookDbContext(DbContextOptions<BookDbContext> otp) : base (otp) { 
        
        }

        #region DbSet
        public DbSet<BookDB> BookDBs { get; set; }
        #endregion
    }
}
