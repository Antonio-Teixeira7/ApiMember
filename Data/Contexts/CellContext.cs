using ApiMember.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMember.Data.Contexts;

public class CellContext : DbContext
{
    public CellContext(DbContextOptions<CellContext> opts) : base(opts)
    {
        
    }

    public DbSet<Cell> Cells { get; set; }
}
