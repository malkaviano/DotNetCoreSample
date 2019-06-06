using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;

namespace LanguageFeatures
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddMvcOptions(options =>
                    {
                        //options.RespectBrowserAcceptHeader = true;
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(statusCode: 302, sslPort: 5001));

            // Meself: GetValues use a Default(T), it may be specified.
            // var v = Configuration.GetSection("ShortCircuitMiddleware").GetValue<int>("EnableBrowserShortCircuit");

            // app.UseStatusCodePages();
            app.UseStatusCodePagesWithReExecute("/Home/Caralho/{0}");

            app.UseExceptionHandler("/Home/errorhandler");

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                // routes.MapRoute(
                //     name: "default",
                //     template: "{controller=Home}/{action=Index}/{id?}"
                // );

                // routes.MapRoute(
                //     name: "weird",
                //     template: "{action=Index}/{controller=Home}/{id:int}"
                // );

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
