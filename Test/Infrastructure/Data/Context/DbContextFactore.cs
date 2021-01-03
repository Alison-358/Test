using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Context
{
    public class DbContextFactore : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            //Config to migrations
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer(this.GetStringConnectionConfig());

            return new DatabaseContext(optionsBuilder.Options);
        }

        private string GetStringConnectionConfig()
        {
            return "Server=DESKTOP-TVN178P\\SQLEXPRESS;database=ALISON;Trusted_connection=true;MultipleActiveResultSets=True;";
        }
    }
}
