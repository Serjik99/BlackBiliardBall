using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace SimpleWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    string page = File.ReadAllText("Site/htmlpage.html");
                    await context.Response.WriteAsync(page);
                });

                endpoints.MapGet("/adminPage", async context =>
                {
                    string page = File.ReadAllText("Site/adminPage.html");
                    await context.Response.WriteAsync(page);
                });

                endpoints.MapGet("/password", async context => 
                {
                    string password = "vodichka";

                    await context.Response.WriteAsync(password);
                });

                endpoints.MapGet("/login", async context =>
                {
                   string login = "1234";

                   await context.Response.WriteAsync(login);
                });

                endpoints.MapGet("/answersPage", async context =>
                {
                    string page = File.ReadAllText("Site/answersPage.html");
                    await context.Response.WriteAsync(page);
                });

                endpoints.MapGet("/randomAnswer", async context =>
                {
                    string randomAnswer = "рандомный ответ";
                    await context.Response.WriteAsync(randomAnswer);
                });

                endpoints.MapGet("/predictionsPage", async context =>
                {
                    string page = File.ReadAllText("Site/predictionsPage.html");
                    await context.Response.WriteAsync(page);
                });

                endpoints.MapGet("/randomPrediction", async context=>
                {
                    PredictionManager pm = new PredictionManager();
                    var query = context.Request.Query;
                    pm.AddPrediction("New string");
                    //var s = pm.GetRandomPrediction();
                   // await context.Response.WriteAsync(s);
                });

                endpoints.MapGet("/addprediction", async context =>
                {
                    PredictionManager pm = new PredictionManager();
                    string query = context.Request.Query["newPrediction"];
                    string qqa = context.Request.Query["newParam"];
                    pm.AddPrediction("New string");
                    //var s = pm.GetRandomPrediction();
                    // await context.Response.WriteAsync(s);
                });
            });
        }
    }
}
