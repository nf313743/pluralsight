﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Entities;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;

namespace CityInfo.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(
            //IHostingEnvironment env
            IConfiguration configuration)
        {
            // var builder = new ConfigurationBuilder()
            //                 .SetBasePath(env.ContentRootPath)
            //                 .AddJsonFile("appSettings.json", 
            //                                 optional:false,
            //                                 reloadOnChange:true);
            // Configuration = builder.Build();
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddMvcOptions(
                        x => x.OutputFormatters.Add(
                            new XmlDataContractSerializerOutputFormatter()));
            services.AddScoped<IMailService, LocalMailService>();
            services.AddDbContext<CityInfoContext>(
                x => x.UseSqlite(Configuration.GetConnectionString("cityInfoDBConnectionString")));
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            CityInfoContext context)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            context.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(x => 
            {
                x.CreateMap<Entities.City, Models.CityWithoutPointsOfInterestDto>();
                x.CreateMap<Entities.City, Models.CityDto>();
                x.CreateMap<Entities.PointOfInterest, Models.PointsOfInterestDto>();
                x.CreateMap<Models.PointsOfInterestForCreationDto, Entities.PointOfInterest>();
                x.CreateMap<Models.PointsOfInterestForUpdateDto, Entities.PointOfInterest>();
                x.CreateMap<Entities.PointOfInterest, Models.PointsOfInterestForUpdateDto>();
            });

            app.UseMvc();

            // app.Run(async (context) =>
            // {
            //     throw new Exception("Oh no");
            //     await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}
