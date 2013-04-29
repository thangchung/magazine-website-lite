using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Objects;
using System.Reflection;

namespace SharpLite.EntityFrameworkProvider
{
    public interface IDbContextBuilder<T> where T : System.Data.Entity.DbContext
    {
        T BuildDbContext();
    }

    public sealed class DbContextBuilder<T> : DbModelBuilder, IDbContextBuilder<T> where T : System.Data.Entity.DbContext
    {
        private readonly DbProviderFactory _factory;
        private readonly ConnectionStringSettings _cnStringSettings;
        private readonly bool _recreateDatabaseIfExists;
        private readonly bool _lazyLoadingEnabled;

        public DbContextBuilder(string connectionStringName, ICollection<string> mappingAssemblies, bool recreateDatabaseIfExists, bool lazyLoadingEnabled) 
        {
            Conventions.Remove<IncludeMetadataConvention>();
            
            _cnStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
            _factory = DbProviderFactories.GetFactory(_cnStringSettings.ProviderName);
            _recreateDatabaseIfExists = recreateDatabaseIfExists;
            _lazyLoadingEnabled = lazyLoadingEnabled;           

            AddConfigurations(mappingAssemblies);
        }

        /// <summary>
        /// Creates a new <see cref="ObjectContext"/>.
        /// </summary>
        /// <returns></returns>
        public T BuildDbContext()
        {
            var cn = _factory.CreateConnection();
            cn.ConnectionString = _cnStringSettings.ConnectionString;

            var dbModel = Build(cn);

            var ctx = dbModel.Compile().CreateObjectContext<ObjectContext>(cn);
            ctx.ContextOptions.LazyLoadingEnabled = _lazyLoadingEnabled;

            if (!ctx.DatabaseExists())
            {
                ctx.CreateDatabase();
            }
            else if (_recreateDatabaseIfExists)
            {
                ctx.DeleteDatabase();
                ctx.CreateDatabase();
            }

            return (T)new System.Data.Entity.DbContext(ctx, true);
        }

        /// <summary>
        /// Adds mapping classes contained in provided assemblies and register entities as well
        /// </summary>
        /// <param name="mappingAssemblies"></param>
        private void AddConfigurations(ICollection<string> mappingAssemblies)
        {
            if (mappingAssemblies == null || mappingAssemblies.Count == 0)
            {
                throw new ArgumentNullException("mappingAssemblies", "You must specify at least one mapping assembly");
            }

            var hasMappingClass = false;
            foreach (var mappingAssembly in mappingAssemblies)
            {
                var asm = Assembly.LoadFrom(MakeLoadReadyAssemblyName(mappingAssembly));                

                foreach (var type in asm.GetTypes())
                {
                    if (!type.IsAbstract)
                    {                        
                        if (type.BaseType != null && (type.BaseType.IsGenericType && IsMappingClass(type.BaseType)))
                        {
                            hasMappingClass = true;

                            // http://areaofinterest.wordpress.com/2010/12/08/dynamically-load-entity-configurations-in-ef-codefirst-ctp5/
                            dynamic configurationInstance = Activator.CreateInstance(type);
                            Configurations.Add(configurationInstance);
                        }
                    }
                }
            }

            if (!hasMappingClass)
            {
                throw new ArgumentException("No mapping class found!");
            }
        }

        /// <summary>
        /// Determines whether a type is a subclass of entity mapping type
        /// </summary>
        /// <param name="mappingType">Type of the mapping.</param>
        /// <returns>
        /// 	<c>true</c> if it is mapping class; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMappingClass(Type mappingType)
        {
            var baseType = typeof(EntityTypeConfiguration<>);
            if (mappingType.GetGenericTypeDefinition() == baseType)
            {
                return true;
            }
            if ((mappingType.BaseType != null) &&
                !mappingType.BaseType.IsAbstract &&
                mappingType.BaseType.IsGenericType)
            {
                return IsMappingClass(mappingType.BaseType);
            }
            return false;
        }
        
        /// <summary>
        /// Ensures the assembly name is qualified
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private static string MakeLoadReadyAssemblyName(string assemblyName)
        {
            return (assemblyName.IndexOf(".dll", StringComparison.Ordinal) == -1)
                ? assemblyName.Trim() + ".dll"
                : assemblyName.Trim();
        }

    }
}
