
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using OnlineStore.APIs.Error;
using OnlineStore.APIs.Helper;
using OnlineStore.APIs.MiddleWares;
using OnlineStore.Core.Mapping.Products;
using OnlineStore.Core.Repositories.Contract;
using OnlineStore.Core.Services.Contract;
using OnlineStore.Core.UnitOfWork.Contract;
using OnlineStore.Repository.Data;
using OnlineStore.Repository.Data.Contexts;
using OnlineStore.Repository.Repositories;
using OnlineStore.Repository.Unit_Of_Work;
using OnlineStore.Service.Services.Products;

namespace OnlineStore.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDependency(builder.Configuration);

            var app = builder.Build();

            await app.ConfigureMiddelwareAsync();

            app.Run();
        }
    }
}
