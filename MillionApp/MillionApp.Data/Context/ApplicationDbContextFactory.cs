using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionApp.Data.Context;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Reemplaza con tu cadena de conexión real
        optionsBuilder.UseSqlServer("Data Source=ANDRES\\SQLEXPRESS;Initial Catalog=MillionAppDb;Persist Security Info=True;User ID=sa;Password=Y0k0gawA_19929495;Trust Server Certificate=True;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
