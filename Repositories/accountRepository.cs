using cms.Context;
using cms.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace cms.Repositories
{
   public class accountRepository
   {
      // define variabel yang diperlukan
      private readonly ContextCMS db;

      // create constructor
      public accountRepository(ContextCMS db)
      {
         this.db = db;
      }

      // method get all data
      public async Task<(List<account?>?, Exception?)> GetAll()
      {
         try
         {
            // get all data menggunakan Entity Framework
            List<account?>? results = await db.account.ToListAsync();

            if (results == null || results.Count() == 0) // cek jika data kosong atau tidak ditemukan
            {
               return (null, new Exception("record not found"));
            }

            // success get all data accounts
            return (results, null);
         }
         catch (Exception err)
         {
            // jika ada error ketika proses get all data
            return (null, err);
         }
      }
   }
}
