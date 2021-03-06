using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {

            services.AddControllersWithViews();

            //// used to initialize and test build, run on startup after creating project
            //services.AddIdentityServer()
            //        .AddInMemoryClients(new List<Client>())
            //        .AddInMemoryIdentityResources(new List<IdentityResource>())
            //        .AddInMemoryApiResources(new List<ApiResource>())
            //        .AddInMemoryApiScopes(new List<ApiScope>())                    
            //        .AddTestUsers(new List<TestUser>())
            //        .AddDeveloperSigningCredential();

            // ApiResource                      resources you want to protect
            // ApiScopes                        what a client app is allowed to do
            // Clients                          what clients are allowed to use identityServer
            // IdentityResources                user information and assigned claim types
            // TestUsers                        create test users used to test
            // DeveloperSigningCredential       create temporary credentials at start up time

            services.AddIdentityServer()
                .AddInMemoryClients(Config.Clients)                                
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddTestUsers(Config.TestUsers)
                .AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
