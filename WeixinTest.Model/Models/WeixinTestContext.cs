using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WeixinTest.Model.Models.Mapping;

namespace WeixinTest.Model.Models
{
    public partial class WeixinTestContext : DbContext
    {
        static WeixinTestContext()
        {
            Database.SetInitializer<WeixinTestContext>(null);
        }

        public WeixinTestContext()
            : base("Name=WeixinTestContext")
        {
        }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LogMap());
        }
    }
}
