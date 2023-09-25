namespace cms.Middlewares
{
   public class TwoMiddleware : IMiddleware
   {
      public async Task InvokeAsync(HttpContext context, RequestDelegate next)
      {
         Console.WriteLine("masuk ke two middleware");

         await next.Invoke(context);

         Console.WriteLine("keluar dari two middleware");
      }
   }
}
