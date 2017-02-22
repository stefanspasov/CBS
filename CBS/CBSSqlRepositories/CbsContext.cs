namespace CBSSqlRepositories
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using CBS.DAL.Models;

    public class CbsContext : DbContext
    {
        static CbsContext()
        {
            Database.SetInitializer(new ReservationsInitializer());
        }

        public CbsContext() : base("CbsConnection")
        {
        }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
