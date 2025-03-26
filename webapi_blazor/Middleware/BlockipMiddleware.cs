// using System;
// using System.Text.Json;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Http;

// namespace Middleware.Middleware
// {
//     // REMEMBER: Add `services.AddTransient<NameMiddleware>();` to Startup.ConfigureServices() method
//     public class BlockipMiddleware : IMiddleware
//     {
//         // IMiddleware is activated per request, 
//         // so scoped services can be injected into the middleware's constructor.
//         public BlockipMiddleware()
//         {
//         }

//         public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//         {
//             //---------------------------
//             // var ip = context.Connection.RemoteIpAddress?.ToString(); // ::1
//             // if (!string.IsNullOrEmpty(ip) && ip == "::1")
//             // {
//             //     context.Response.StatusCode = StatusCodes.Status403Forbidden;
//             //     await context.Response.WriteAsync("Access denied from your region.");
//             //     return;
//             // }
//             // await next(context);
//             //---------------------------
//             // context.Response.ContentType = "application/json";
//             // context.Response.StatusCode = StatusCodes.Status400BadRequest;

//             // var result = new { message = context.Request.Body };
//             // await context.Response.WriteAsync(JsonSerializer.Serialize(result));


//             // await next(context);
//             // Check if the request is coming from a blocked IP address
//             await next(context);
//         }
//     }
// }
