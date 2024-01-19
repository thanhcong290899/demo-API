using demoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace demoAPI.Data;

    public class ApiDbContext : DbContext
    {
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    #region DbSet
    public DbSet<SinhVienDB> SinhViens { get; set; }
    #endregion
}

