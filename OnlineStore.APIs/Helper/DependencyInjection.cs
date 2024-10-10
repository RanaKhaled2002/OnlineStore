﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.APIs.Error;
using OnlineStore.Core.Mapping.Products;
using OnlineStore.Core.Services.Contract;
using OnlineStore.Core.UnitOfWork.Contract;
using OnlineStore.Repository.Data.Contexts;
using OnlineStore.Repository.Unit_Of_Work;
using OnlineStore.Service.Services.Products;

namespace OnlineStore.APIs.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddBuiltInSerivce();
            service.AddSwaggerSerivce();
            service.AddSDBSerivce(configuration);
            service.AddValidationErrorSerivce();
            service.AddScopedSerivce();
            service.AddAutoMapperSerivce(configuration);
            return service;
        }

        private static IServiceCollection AddBuiltInSerivce(this IServiceCollection service)
        {
            // Add services to the container.

            service.AddControllers();
            return service;
        }

        private static IServiceCollection AddSwaggerSerivce(this IServiceCollection service)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();
            return service;
        }

        private static IServiceCollection AddSDBSerivce(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return service;
        }

        private static IServiceCollection AddValidationErrorSerivce(this IServiceCollection service)
        {

            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                            .SelectMany(P => P.Value.Errors)
                                            .Select(E => E.ErrorMessage)
                                            .ToArray();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return service;
        }

        private static IServiceCollection AddScopedSerivce(this IServiceCollection service)
        {

            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IProductService, ProductService>();
            return service;
        }

        private static IServiceCollection AddAutoMapperSerivce(this IServiceCollection service, IConfiguration configuration)
        {

            service.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));

            return service;
        }


    }
}
