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
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace SimpleWebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<PredictionManager>();
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => options.LoginPath = new PathString("/Auth"));
            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/auth", async context =>
                {
                    string page = File.ReadAllText("Site/loginPage.html");
                    await context.Response.WriteAsync(page);
                });
                endpoints.MapGet("/password", async context =>
                {
                    string password = "vodichka";

                    await context.Response.WriteAsync(password);
                });

                endpoints.MapPost("/login", async context =>
                {
                    var credentials = await context.Request.ReadFromJsonAsync<Credential>();
                    // с заданным логином и паролем мы пойдем в базу
                    // если в базе есть пользователь, то всё ок, если нет, то ничего не делаем
                    var fakeUser = new Credential { Login = "superlogin", Password = "superpassword" };
                    if (credentials.Login == fakeUser.Login && credentials.Password == fakeUser.Password)
                    {
                        List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, credentials.Login)
                           
                            
                        };
                       
                        // создаем объект ClaimsIdentity
                        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                        //создаём куки пользователю
                        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

                        // перенаправляем на нужную сраницу
                        context.Response.Redirect("/adminPage");
                    }

                    await context.Response.WriteAsync(credentials.Login);
                });

                endpoints.MapGet("/adminPage", async context =>
                {
                    string page = File.ReadAllText("Site/adminPage.html");
                    await context.Response.WriteAsync(page);
                }).RequireAuthorization();

                

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

                endpoints.MapGet("/", async context =>
                {
                    string page = File.ReadAllText("Site/predictionsPage.html");
                    await context.Response.WriteAsync(page);
                });

                endpoints.MapGet("/randomPrediction", async context=>
                {
                    var pm = app.ApplicationServices.GetService<PredictionManager>();
                    var s = pm.GetRandomPrediction();
                    await context.Response.WriteAsync(s.PredictionString);
             
                });

                endpoints.MapPost("/addPrediction", async context =>
                {
                    var pm = app.ApplicationServices.GetService<PredictionManager>();
                    var query = await context.Request.ReadFromJsonAsync<Prediction>();
                    pm.AddPrediction(query.PredictionString);
                 
                });

                endpoints.MapGet("/allPredictions", async context =>
                 {
                     var pm = app.ApplicationServices.GetService<PredictionManager>();
                     await context.Response.WriteAsJsonAsync(pm.GetAllPredictions());
                     
                     
                 });

                endpoints.MapPost("/deletPrediction", async context =>
                {
                    Prediction p = await context.Request.ReadFromJsonAsync<Prediction>();
                    app.ApplicationServices.GetService<PredictionManager>().DeletPrediction(p);

                });

                endpoints.MapPost("/updatePrediction", async context =>
                {
                     string[] s;
                    using (var sr = new StreamReader(context.Request.BodyReader.AsStream()))
                        s = sr.ReadToEnd().Split("::");
                    app.ApplicationServices.GetService<PredictionManager>().UpdatePrediction(int.Parse(s[0]), s[1]);
                });
            });
        }
    }
}
