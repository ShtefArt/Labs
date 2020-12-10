using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lab3.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Lab3
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class DBMiddleware
    {
        private readonly RequestDelegate _next;

        public DBMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, PublicUtilitiesContext db)
        {
            try

            {
                httpContext.Response.ContentType = "text/html; charset=utf-8";
               
                await httpContext.Response.WriteAsync("<img src='/images/cat.png' width=100 heigh=100 alt='My image' />");
                await httpContext.Response.WriteAsync("<h3> База даних: Комунальні послуги</h3>");

                var tenants = db.Tenants.ToList();

                await httpContext.Response.WriteAsync("</br>База даних повинна містити інформацію:</br></br></br>");
                await httpContext.Response.WriteAsync("<h5>10 квартиронаймачiв:</h5></br>");
   
                await httpContext.Response.WriteAsync(String.Join("</br>", tenants));
                await httpContext.Response.WriteAsync("</br></br>=========================================</br></br>");
                
                await httpContext.Response.WriteAsync("</br><h5>5 видiв послуг. Вартiсть одних послуг повинна визначатись площею квартири, iнших - к-стю осiб, що проживає:</h5></br>");
                var services = db.Services.ToList();
                await httpContext.Response.WriteAsync(string.Join("</br>", services));

                await httpContext.Response.WriteAsync("</br></br>=========================================</br></br>");
                await httpContext.Response.WriteAsync("</br><h5>Передбачити, що кожен квартиронаймач користується 3+ послугами:</h5></br>");
                var tenant_service = db.Tenants
                        .Join(db.Payment, tenants => tenants.ApartmentId, payment => payment.ApartmentId,
                        (t, p) => new { IdTenent = t.Id, Name = t.FirstName, Surname = t.LastName, p.ApartmentId })
                        .ToList()
                        .GroupBy(table => new { table.IdTenent, table.Name, table.Surname })
                        .Where(g => g.Count() >= 3);

                foreach (var t in tenant_service)
                {
                    await httpContext.Response.WriteAsync($"{t.Key.IdTenent}   {t.Key.Name}    {t.Key.Surname} " + $" користується {t.Count()} послугами</br>");
                }                
            }
            catch (Exception ex)
            {
                await httpContext.Response.WriteAsync($"Error: {ex.Message}");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DBMiddleware>();
        }
    }
}
