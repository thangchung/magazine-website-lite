using Autofac;
using Cik.MagazineWeb.Application;
using Cik.MagazineWeb.EntityFrameworkProvider;
using Cik.MagazineWeb.EntityFrameworkProvider.Queries;
using Cik.MagazineWeb.Utilities.Configurations.Impl;
using SharpLite.Domain.DataInterfaces;
using SharpLite.EntityFrameworkProvider;

namespace Cik.MagazineWeb.Init
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(new CoreDbContext("DefaultDb")).AsSelf().SingleInstance();

            builder.RegisterGeneric(typeof (Repository<>))
                   .As(typeof (IRepository<>))
                   .WithParameter((pi, c) => pi.ParameterType == typeof(System.Data.Entity.DbContext),
                                  (pi, c) => c.Resolve<CoreDbContext>());

            builder.RegisterType<EntityDuplicateChecker>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ConfigurationManager>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MagazineApplication>().AsImplementedInterfaces().InstancePerLifetimeScope();

            // register query
            builder.RegisterType<QueryForItemSummaries>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
