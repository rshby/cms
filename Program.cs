using cms.Context;
using cms.Middlewares;
using cms.Models.DTO;
using cms.Repositories;
using cms.Services;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// add connection database
builder.Services.AddSingleton<MySqlConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString("mysql")));

// add DB context
builder.Services.AddDbContext<ContextCMS>(options => options.UseMySQL(builder.Configuration.GetConnectionString("mysql")));

// add repository layer
builder.Services.AddScoped<accountRepository>();

// add service layer
builder.Services.AddScoped<accountService>();
builder.Services.AddScoped<OneMiddleware>();
builder.Services.AddScoped<TwoMiddleware>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// exception handler
//app.UseExceptionHandler(excApp =>
//{
//   excApp.Run(async ctx =>
//   {
//      //ctx.Response.StatusCode = StatusCodes.Status404NotFound;

//      var errStatusCode = ctx.Response.StatusCode;
//      Console.WriteLine( $"error statuscode = " + errStatusCode.ToString());

//      // if response error not found
//      if (errStatusCode == (int)HttpStatusCode.NotFound)
//      {
//         ctx.Response.StatusCode = (int)HttpStatusCode.NotFound;
//         ctx.Response.ContentType = "application/json";
//         await ctx.Response.WriteAsJsonAsync<APIMessage<object>>(new APIMessage<object>()
//         {
//            StatusCode = (int)HttpStatusCode.NotFound,
//            Status = "not found",
//            Message = "Endpoint Not Found"
//         });
//         return;
//      }

//      // response error internal server
//      ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//      ctx.Response.ContentType = "application/json";  
//      await ctx.Response.WriteAsJsonAsync(new APIMessage<object>()
//      {
//         StatusCode = (int)HttpStatusCode.InternalServerError,
//         Status = "internal server error",
//         Message = "unexpected error"
//      });
//   });
//});

// app.UseHsts();
// app.UseStatusCodePages();

// user handler exception ketika endpoint not found/method not allowed/forbidden
app.Use(async (ctx, next) =>
{
   Console.WriteLine("masuk ke middleware handler");
   await next.Invoke();
   Console.WriteLine("keluar dari middleware handler");
   // ctx.Response.ContentType = "application/json";
   if (ctx.Response.StatusCode != (int)HttpStatusCode.OK && ctx.Response.ContentType != "application/json")
   {
      ctx.Response.ContentType = "application/json";

      // if error not found
      if (ctx.Response.StatusCode == (int)HttpStatusCode.NotFound)
      {
         ctx.Response.StatusCode = (int)HttpStatusCode.NotFound;
         await ctx.Response.WriteAsJsonAsync(new APIMessage<object>()
         {
            StatusCode = (int)HttpStatusCode.NotFound,
            Status = "not found",
            Message = "endpoint not found"
         });
         return;
      }

      // if error method not allowed
      if (ctx.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed)
      {
         ctx.Response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
         await ctx.Response.WriteAsJsonAsync(new APIMessage<object>()
         {
            StatusCode = (int)HttpStatusCode.MethodNotAllowed,
            Status = "method not allowed",
            Message = "method not allowed"
         });
         return;
      }

      // if error internal server error
      ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      await ctx.Response.WriteAsJsonAsync(new APIMessage<object>()
      {
         StatusCode = (int)HttpStatusCode.InternalServerError,
         Status = "internal server error",
         Message = "unexpected error"
      });
      return;
   }
});

// register middleware
app.UseMiddleware<OneMiddleware>();
app.UseMiddleware<TwoMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
