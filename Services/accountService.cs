using cms.Models.DTO;
using cms.Models.Entity;
using cms.Repositories;

namespace cms.Services
{
   public class accountService
   {
      // define variabel yang digunakan
      private readonly accountRepository accRepo;

      // create contructor
      public accountService(accountRepository accRepo)
      {
         this.accRepo = accRepo;
      }

      // method get all data account
      public async Task<(List<accountResponse?>?, Exception?)> GetAll()
      {
         try
         {
            // call procedure GetAll in repository layer
            var (results, err) = await accRepo.GetAll();
            if (err != null) // cek jika ada error ketika proses query
            {
               return (null, err);
            }

            // success get data -> mapping ke object DTO
            List<accountResponse?>? response = new List<accountResponse>();

            Task[] tasks = Array.Empty<Task>(); // create arrah kosong

            foreach(account data in results) // looping data hasil query
            {
               var _ = Task.Run(() =>
               {
                  accountResponse item = new accountResponse() // create object tipe data accountResponse
                  {
                     Id = data?.Id,
                     Email = data?.Email,
                     Username = data?.Username,
                     Password = data?.Password,
                     Otp = data?.Otp,
                     //ExpiredOtp = ((DateTime)data?.ExpiredOtp).ToString("yyyy-MM-dd HH:mm:ss")
                     CreatedAt = ((DateTime)data?.CreatedAt).ToString("yyyy-MM-dd HH:mm:ss"),
                     UserId = data?.UserId
                  };

                  if (data.ExpiredOtp != null) // cek jika data expired_otp tidak kosong
                  {
                     item.ExpiredOtp = ((DateTime)data?.ExpiredOtp).ToString("yyyy-MM-dd HH:mm:ss");
                  }

                  // append ke variabel response
                  response.Add(item);
               });

               tasks.Append(_);
            }

            // wait all taks done
            await Task.WhenAll(tasks);

            // return response ke controller
            return (response, null);
         }
         catch (Exception err)
         {
            return (null, err);
         }
      }
   }
}
