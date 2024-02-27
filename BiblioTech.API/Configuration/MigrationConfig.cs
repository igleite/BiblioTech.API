using BiblioTech.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BiblioTech.API.Configuration
{
    public static class MigrationConfig
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<BiblioTechDbContext>();
                db.Database.Migrate();
            }
 
        }
    }
}
