using Microsoft.EntityFrameworkCore;
using PashaVacancyProject.Domain.EntitiesMapping.Base;
using System.Reflection;

namespace PashaVacancyProject.Domain.DInfrastucture
{
    public class BoxDbContent : DbContext
    {
        protected static string ConnectionString { set; get; }

        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(ConnectionString, npgsqlOptionsAction: options =>
            //{
            //    //options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            //});
            optionsBuilder.UseSqlServer(ConnectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("dbo");


            var TypesForToConfigurations = Assembly.GetExecutingAssembly()
                                                   .GetTypes()
                                                   .Where(type => type.BaseType != null && type.BaseType.IsGenericType)
                                                   .Where(type => (type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityTypeConfiguration<>) || type.BaseType.GetGenericTypeDefinition() ==
                                                   typeof(IEntityTypeConfiguration<>))).ToList();


            foreach (var Type in TypesForToConfigurations)
            {
                modelBuilder.ApplyConfigurationsFromAssembly(Type.Assembly);

            }
        }


        public static void SetConnectionString()
        {
            ConnectionString = "Server=DESKTOP-41BCSBQ;Database=PashaInsuranceDB;Trusted_Connection=True";


#if (DEBUG)



#endif



        }
    }
}
