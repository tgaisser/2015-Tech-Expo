
#region

using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using TechExpo.Data.Interfaces;
using TechExpo.Data.Models;

#endregion

namespace TechExpo.Data
{

    public class DataContext
        : DbContext,
          IDataContext
    {

        public DataContext()
            : base("TechExpo2014")
        {
        }

        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public IDbSet<SalesPerson> SalesPeople
        {
            get;
            set;
        }

        public IDbSet<Carrier> Carriers
        {
            get;
            set;
        }

        public IDbSet<LogMessage> LogMessages
        {
            get;
            set;
        }

        public IDbSet<Registrant> Registrants
        {
            get;
            set;
        }

        public IDbSet<Device> Devices
        {
            get;
            set;
        }

        public IDbSet<Participation> Participations
        {
            get;
            set;
        }

        public IDbSet<Location> Locations
        {
            get;
            set;
        }

        public new IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            return base.GetValidationErrors();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Registrant>()
                        .HasMany(r => r.Participations)
                        .WithRequired(p => p.Registrant)
                        .WillCascadeOnDelete(true);
            modelBuilder.Entity<Participation>()
                        .HasRequired(p => p.Registrant);
            modelBuilder.Entity<Participation>()
                        .HasRequired(p => p.Location);
            modelBuilder.Entity<SalesPerson>()
                        .HasOptional(s => s.Carrier);
        }
    }
}