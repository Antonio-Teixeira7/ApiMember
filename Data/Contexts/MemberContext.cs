using ApiMember.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiMember.Data.Contexts;

public class MemberContext : DbContext
{
    public MemberContext(DbContextOptions<MemberContext> opts) : base(opts)
    {
        
    }

    public DbSet<Member> Members { get; set; }
}
