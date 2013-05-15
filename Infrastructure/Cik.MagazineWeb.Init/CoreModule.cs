using Autofac;
using Cik.MagazineWeb.Application.Magazines;
using Cik.MagazineWeb.Application.Magazines.Services;
using Cik.MagazineWeb.Application.UserAuthentications;
using Cik.MagazineWeb.EntityFrameworkProvider;
using Cik.MagazineWeb.Utilities.Configurations.Impl;
using Cik.MagazineWeb.Utilities.Encyption.Impl;
using SharpLite.Domain.DataInterfaces;
using SharpLite.EntityFrameworkProvider;

namespace Cik.MagazineWeb.Init
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // registering all things needed for building data context
            builder.RegisterInstance(new CoreDbContext("DefaultDb"))
                   .AsSelf();

            builder.RegisterGeneric(typeof (Repository<>))
                   .As(typeof (IRepository<>))
                   .WithParameter((pi, c) => pi.ParameterType == typeof(System.Data.Entity.DbContext),
                                  (pi, c) => c.Resolve<CoreDbContext>());

            // registering all utility objects
            builder.RegisterType<SimpleEncryptor>()
                   .AsImplementedInterfaces();

            builder.RegisterType<EntityDuplicateChecker>()
                   .AsImplementedInterfaces();

            builder.RegisterType<ConfigurationManager>()
                   .AsImplementedInterfaces();

            // registering all application instances
            builder.RegisterType<MagazineApplication>()
                   .AsImplementedInterfaces();

            builder.RegisterType<UserApplication>()
                   .AsImplementedInterfaces();
                    
            // registering all services
            builder.RegisterType<ItemSummaryService>()
                   .AsImplementedInterfaces();
        }
    }
}
