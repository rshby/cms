using cms.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace cms.Context
{
   public class ContextCMS : DbContext
   {
      // create constructor
      public ContextCMS()
      {

      }

      // create entity/table in database
      public DbSet<account> account { get; set; }
   }
}
