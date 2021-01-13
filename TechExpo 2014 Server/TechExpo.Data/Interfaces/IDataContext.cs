
#region

using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using TechExpo.Data.Models;

#endregion

namespace TechExpo.Data.Interfaces
{

    internal interface IDataContext
    {
        IDbSet<LogMessage> LogMessages
        {
            get;
            set;
        }
        IDbSet<SalesPerson> SalesPeople
        {
            get;
            set;
        }
        IDbSet<Carrier> Carriers
        {
            get;
            set;
        }
        IDbSet<Registrant> Registrants
        {
            get;
            set;
        }
        IDbSet<Device> Devices
        {
            get;
            set;
        }
        IDbSet<Participation> Participations
        {
            get;
            set;
        }
        IDbSet<Location> Locations
        {
            get;
            set;
        }
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}