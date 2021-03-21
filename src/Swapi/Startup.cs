using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swapi.Extensions;
using Swapi.Service;
using Swapi.Service.Interfaces;
using Swapi.Service.Models;

namespace Swapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .Enrich.WithMachineName()
                //.MinimumLevel.Override("Swapi", LogEventLevel.Information)
                //.MinimumLevel.Error()
                .WriteTo.Console()
                .WriteTo.File("logs.txt")
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<SwapApi>(Configuration.GetSection("SwapApi"));
            services.Configure<Audience>(Configuration.GetSection("Audience"));
            
            //Authentication
            var serviceProvider = services.BuildServiceProvider();
            var audience = serviceProvider.GetRequiredService<IOptions<Audience>>().Value;
            services.AddJwtAuthentication(audience);

            services.AddControllers().AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
            });
            services.AddHttpClient();
            services.AddSwaggerDocumentation();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IPeopleService, PeopleService>();
            services.AddTransient<IPlanetService, PlanetService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDocumentation();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
