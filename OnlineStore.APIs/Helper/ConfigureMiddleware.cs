using OnlineStore.APIs.MiddleWares;
using OnlineStore.Repository.Data.Contexts;
using OnlineStore.Repository.Data;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Repository.Identity.Contexts;
using OnlineStore.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using OnlineStore.Core.Entities.Identity;

namespace OnlineStore.APIs.Helper
{
    public static class ConfigureMiddleware
    {
        public static async Task<WebApplication> ConfigureMiddelwareAsync(this WebApplication app)
        {
            #region Update Database
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<StoreDbContext>();
            var Identitycontext = services.GetRequiredService<StoreIdentityDbContext>();
            var userManger = services.GetRequiredService<UserManager<AppUser>>();
            var loggerFactoey = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
                await Identitycontext.Database.MigrateAsync(); // Update Database
                await StoreIdentitySeed.SeedAsync(userManger);
            }
            catch (Exception ex)
            {
                var logger = loggerFactoey.CreateLogger<Program>();
                logger.LogError(ex, "There Are Problems During Apply Migrations !!");
            }
            #endregion

            app.UseMiddleware<ExceptionMiddleware>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            return app;
        }
    }
}
