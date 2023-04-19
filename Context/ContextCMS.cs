using cms.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace cms.Context
{
   public class ContextCMS : DbContext
   {
      // create constructor
      public ContextCMS(DbContextOptions<ContextCMS> options) : base(options)
      {

      }

      // create entity/table in database
      public DbSet<account> account { get; set; }
   }
}
