using cms.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace cms.Controllers
{
   [ApiController]
   [Route("test")]
   public class TestController : ControllerBase
   {
      // create constructor
      public TestController()
      {

      }

      [HttpGet]
      [Route("api")]
      public async Task<ActionResult<APIMessage<object>>> TestAPI()
      {
         try
         {
            return Ok(new APIMessage<object>()
            {
               StatusCode = (int)HttpStatusCode.OK,
               Status = "ok",
               Message = "success test api"
            });
         }
         catch (Exception err)
         {
            // jika ada error ketika test api
            return StatusCode((int)HttpStatusCode.InternalServerError, new APIMessage<object>()
            {
               StatusCode = (int)HttpStatusCode.InternalServerError,
               Status = "internal server error",
               Message = $"error : {err.Message}, with inner exception : {err.InnerException}"
            });
         }
      }
   }
}
