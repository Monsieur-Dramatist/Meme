using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLogic
{
    public class SimpleConfigModule:NinjectModule
    {
        public override void Load()
        {
            Bind<DbContextOptions<AppDbContext>>().ToMethod(context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Monsieur-Dramatist\Meme\DataAccessLayer\MemeDatabase.mdf;Integrated Security=True");
                return optionsBuilder.Options;
            }).InSingletonScope();

            Bind<AppDbContext>().ToSelf().InSingletonScope();
            Bind<IRepository>().To<EntityRepository>().InSingletonScope();
        }
    }
}
