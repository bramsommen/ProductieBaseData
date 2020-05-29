using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.EntityFrameworkCore;
using BLL;
using DAL;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.StaticFiles;
using REST_API.Hubs;
using REST_API.SQLDependency;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;

namespace REST_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //// DI ==> Databse Context en Connecion String
            //    services.AddDbContext<ProductieBaseDataContext>(options => options.UseSqlServer("Server=SIRIUSDB;Database=ProductieBaseData;User Id=ErvoAppDB;Password=#-247-App-usr!;"));

            services.AddDbContext<ProductieBaseDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            // DI Eigenschap
            services.AddTransient<IEigenschapService, EigenschapService>(); // BLL
            services.AddTransient<IEigenschapRepository, EigenschapRepository>(); // DAL

            // DI Cyclus Type
            services.AddTransient<ICyclusTypeService, CyclusTypeService>(); // BLL
            services.AddTransient<ICyclusTypeRepository, CyclusTypeRepository>(); // DAL

            // DI Cyclus
            services.AddTransient<ICyclusService, CyclusService>(); // BLL
            services.AddTransient<ICyclusRepository, CyclusRepository>(); // DAL

            // DI Maak INstellingen
            services.AddTransient<IMaakInstellingenService, MaakInstellingenService>(); // BLL
            services.AddTransient<IMaakInstellingenRepository, MaakInstellingenRepository>(); // DAL

            // DI Cyclus maak instellingen
            services.AddTransient<ICyclusMaakInstellingService, CyclusMaakInstellingService>(); // BLL
            services.AddTransient<ICyclusMaakInstellingenRepository, CyclusMaakInstellingenRepository>(); // DAL

            // DI Cyclus Product Versie
            services.AddTransient<IProductVersieService, ProductVersieService>(); // BLL
            services.AddTransient<IProductVersieRepository, ProductVersieRepository>(); // DAL

            // DI Cyclus Product
            services.AddTransient<IProductService, ProductService>(); // BLL
            services.AddTransient<IProductRepository, ProductRepository>(); // DAL

            // DI Product Eigenshchappen
            services.AddTransient<IProductEigenschapService, ProductEigenschapService>(); // BLL
            services.AddTransient<IProductEigenschapRepository, ProductEigenschapRepository>(); // DAL

            // DI Product Cyclus Maak Instelling
            services.AddTransient<IProductVersieCyclusService, ProductVersieCyclusService>(); // BLL
            services.AddTransient<IProductVersieCyclusRepository, ProductVersieCyclusRepository>(); // DAL

            // DI Machine Onderdeel
            services.AddTransient<IMachineOnderdeelService, MachineOnderdeelService>(); // BLL
            services.AddTransient<IMachineOnderdeelRepository, MachineOnderdeelRepository>(); // DAL

            // DI Hmi vs Mgmt Exchange Tabel
            services.AddTransient<IHmiMgmtExchangeService, HmiMgmtExchangeService>(); // BLL
            services.AddTransient<IHmiMgmtExchangeRepository, HmiMgmtExchangeRepository>(); // DAL


            // DI GLOBAL PRODUCT
            services.AddTransient<IGlobalProductService, GlobalProductService>(); // BLL
            services.AddTransient<IGlobalProductRepository, GlobalProductRepository>(); // DAL

            // DI GLOBAL PRODUCT EIGENSCHAP
            services.AddTransient<IGlobalProductEigenschapService, GlobalProductEigenschapService>(); // BLL
            services.AddTransient<IGlobalProductEigenschapRepository, GlobalProductEigenschapRepository>(); // DAL

            services.AddControllers();

            // Add service and create Policy with options
            // Toestaan dat applicaties op andere Servers toegang hebben tot deze API (BV Angular APP op andere localhost)
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    {
                        builder

                      .AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()

                      // Specifier Login
                      .AllowCredentials()
                     .WithOrigins("http://localhost:4200") // LOCAL Angular UI
                     .WithOrigins("http://digi.robberechts.local"); // LIVE Angular UI
                    });
            });

            // Auto Doc Generator

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Base Data API", Version = "1" });
            });

            // Client to server data stream 
            services.AddSignalR();

            // DB to server notification
            services.AddSingleton<HmiMgmtExchangeDatabaseSubscription, HmiMgmtExchangeDatabaseSubscription>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Activeer API documentatie
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1 (BS 09/03/2020)");

                c.RoutePrefix = string.Empty;
            });

            //   Activeer SignalR Client to server data stream(HUBS)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<HmiMgmtExchangeHub>("/HmiMgmtExchangeHub");
            });

            // Activeer SQL table notification system (SQL Table Dependency)
            app.UseSqlTableDependency<HmiMgmtExchangeDatabaseSubscription>(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}

