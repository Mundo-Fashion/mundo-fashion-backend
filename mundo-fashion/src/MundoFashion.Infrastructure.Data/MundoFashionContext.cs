using Microsoft.EntityFrameworkCore;

namespace MundoFashion.Infrastructure.Data
{
    public class MundoFashionContext : DbContext
    {
        public MundoFashionContext(DbContextOptions<MundoFashionContext> options) : base(options) { }
    }
}
