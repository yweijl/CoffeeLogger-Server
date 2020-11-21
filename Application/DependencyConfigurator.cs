﻿using Application.Commands.Handlers;
using Application.Commands.Objects;
using Application.DTOs;
using Application.Queries.Handlers;
using Application.Queries.Objects;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Application
{
    public static class DependencyConfigurator
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

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

            return services;
        } 
    }
}