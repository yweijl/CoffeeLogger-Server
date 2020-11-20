using Core.Commands.Handlers;
using Core.Commands.Objects;
using Core.DTOs;
using Core.Interfaces;
using Core.Queries.Handlers;
using Core.Queries.Objects;
using Infrastructure;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace API
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
            RegisterContainer(services);

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMediatR(typeof(Startup));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var exceptionRoute = env.IsDevelopment() 
                ? "/error-local-development" 
                : "/error";
            
            app.UseExceptionHandler(exceptionRoute);

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void RegisterContainer(IServiceCollection services)
        {
            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddScoped<IRepository, Repository>();

            //Handlers
            
            // Queries
            services.AddScoped<IRequestHandler<GetBrandQuery, BrandDto>, GetBrandQueryHandler>();
            services.AddScoped<IRequestHandler<GetBrandsQuery, List<BrandDto>>, GetBrandsQueryHandler>();
            services.AddScoped<IRequestHandler<GetRecordQuery, RecordDto>, GetRecordQueryHandler>();
            services.AddScoped<IRequestHandler<GetRecordsQuery, List<RecordDto>>, GetRecordsQueryHandler>();
            services.AddScoped<IRequestHandler<GetCoffeeQuery, CoffeeDto>, GetCoffeeQueryHandler>();
            services.AddScoped<IRequestHandler<GetCoffeesQuery, List<CoffeeDto>>, GetCoffeesQueryHandler>();

            // Commands
            services.AddScoped<IRequestHandler<NewBrandCommand, BrandDto>, NewBrandCommandHandler>();
            services.AddScoped<IRequestHandler<NewCoffeeCommand, CoffeeDto>, NewCoffeeCommandHandler>();
            services.AddScoped<IRequestHandler<NewRecordCommand, RecordDto>, NewRecordCommandHandler>();
        }
    }
}