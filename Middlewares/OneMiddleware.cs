namespace cms.Middlewares
{
   public class OneMiddleware : IMiddleware
   {
      // create constructor
      public OneMiddleware()
      {

      }

      public async Task InvokeAsync(HttpContext context, RequestDelegate next)
      {
         Console.WriteLine("masuk ke one middleware");
         await next.Invoke(context);
         Console.WriteLine("keluar dari one middleware");
      }
   }
}
