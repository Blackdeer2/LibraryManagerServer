using LibraryManagerServer.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NLog;
using System.Reflection;

namespace LibraryManagerServer
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);

         LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

         // Add services to the container.

         builder.Services.ConfigureCors();
         builder.Services.ConfigureIISIntegration();
         builder.Services.ConfigureLoggerService();


         builder.Services.ConfigureMySqlContext(builder.Configuration);
         builder.Services.ConfigureRepositoryWrapper();
         builder.Services.AddControllers();
         builder.Services.AddAutoMapper(typeof(Program));

         builder.Services.AddApiVersioning(options =>
         {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
         });
         var app = builder.Build();

         // Configure the HTTP request pipeline.

         app.UseSwagger();
         app.UseSwaggerUI(options =>
         {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Manager");
            options.RoutePrefix = string.Empty; // Set the Swagger UI at the root URL
         });

         if (app.Environment.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();

         }
         else
            app.UseHsts();

         app.UseHttpsRedirection();

         app.UseStaticFiles();

         app.UseForwardedHeaders(new ForwardedHeadersOptions
         {
            ForwardedHeaders = ForwardedHeaders.All
         });
         app.UseCors("CorsPolicy");

         app.UseAuthorization();


         app.MapControllers();

         app.Run();
      }
   }
}
