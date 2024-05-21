using Microsoft.EntityFrameworkCore;

namespace Persistance.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
}
