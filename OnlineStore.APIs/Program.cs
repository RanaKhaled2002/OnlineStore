
using Microsoft.EntityFrameworkCore;
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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IProductService,ProductService>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new ProductProfile(builder.Configuration)));

            var app = builder.Build();

            #region Update Database
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<StoreDbContext>();
            var loggerFactoey = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = loggerFactoey.CreateLogger<Program>();
                logger.LogError(ex, "There Are Problems During Apply Migrations !!");
            } 
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
