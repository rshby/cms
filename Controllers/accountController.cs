using cms.Models.DTO;
using cms.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace cms.Controllers
{
   [ApiController]
   [Route("api")]
   public class accountController : ControllerBase
   {
      // define variabel yang digunakan di controller
      private readonly accountService accService;

      // create constructor
      public accountController(accountService accService)
      {
         this.accService = accService;
      }

      [HttpGet]
      [Route("accounts")]
      public async Task<ActionResult<List<accountResponse?>?>> GetAll()
      {
         try
         {
            // call procedure GetALl in service
            var (accounts, err) = await accService.GetAll();
            if (err != null)
            {
               // jika error not found
               if(err.Message.Contains("not found"))
               {
                  return NotFound(new APIMessage<object>()
                  {
                     StatusCode = (int)HttpStatusCode.NotFound,
                     Status = "not found",
                     Message = err.Message
                  });
               }

               // jika error internal server error
               return StatusCode(StatusCodes.Status500InternalServerError, new APIMessage<object>()
               {
                  StatusCode = (int)HttpStatusCode.InternalServerError,
                  Status = "internal server error",
                  Message = $"error : {err.Message}, with inner exception : {err.InnerException}"
               });
            }

            // success get data
            return Ok(new APIMessage<List<accountResponse>>()
            {
               StatusCode = (int)HttpStatusCode.OK,
               Status = "ok",
               Message = "success get all data accounts",
               Data = accounts
            });
         }
         catch (Exception err)
         {
            // jika ada error ketika proses get all data
            return StatusCode(StatusCodes.Status500InternalServerError, new APIMessage<object>()
            {
               StatusCode = (int)HttpStatusCode.InternalServerError,
               Status = "internal server error",
               Message = $"error : {err.Message}, with inner exception : {err.InnerException}"
            });
         }
      }

   }
}
