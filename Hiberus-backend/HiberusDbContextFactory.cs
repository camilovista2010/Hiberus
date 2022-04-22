using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using Hiberus.DataAccessLayer.DataContext;
using Hiberus.Model.Models.GloblalSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiberusBackend
{
    public class HiberusDbContextFactory : IDesignTimeDbContextFactory<HiberusDbContext>
    {
        /// <summary>
        /// Con la consola del administrador de paquetes, podremos ejecutar Add-Migration "Drop-Database ,Get-DbContext ,Scaffold-DbContext
        ///Script-Migrations ,Update-Database", Recibe variables de entorno indicando en optionsbuilder Environment.GetEnvironmentVariable(""),
        ///o solo pasar la cadena de conexion local estrutura ->"Server=localhost;Port=5432;Database=namedatabase;User Id=user;Password=password;"
        /// </summary>
        /// Add-Migration InitialCreate
        /// Add-Migration
        /// Update-Database
        /// <param name="args"></param>
        /// <returns></returns>
        public HiberusDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HiberusDbContext>();
            optionsBuilder.UseNpgsql("Server=hiberus.postgres.database.azure.com;Database=hiberus;Port=5432;User Id=hiberus;Password=8i6er$s#80;");
            return new HiberusDbContext(optionsBuilder.Options);
        }
    }
}
