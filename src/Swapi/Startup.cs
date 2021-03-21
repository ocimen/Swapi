using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using Serilog;
using Swapi.Extensions;
using Swapi.Service;
using Swapi.Service.Interfaces;
using Swapi.Service.Mappings;
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
            
            var serviceProvider = services.BuildServiceProvider();
            var audience = serviceProvider.GetRequiredService<IOptions<Audience>>().Value;
            var swapApi = serviceProvider.GetRequiredService<IOptions<SwapApi>>().Value;
            
            //Authentication
            services.AddJwtAuthentication(audience);

            //AutoMapper
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile(swapApi));
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddControllers().AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
            });
            services.AddHttpClient();
            services.AddHttpClient(swapApi.ClientName, c =>
            {
                c.BaseAddress = new Uri(swapApi.BaseUrl);
            });
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
