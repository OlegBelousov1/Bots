﻿using InstaDirect.Data;
using Microsoft.EntityFrameworkCore;

namespace InstaDirect.Extensions
{
    public static class DataExtensions
    {
        public static WebApplication ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
            db.Database.Migrate();
            return app;
        }
    }
}
