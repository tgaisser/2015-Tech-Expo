
#region

using System.Data.Entity.Migrations;
using System.Linq;
using TechExpo.Data.Models;

#endregion

namespace TechExpo.Data.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataContext context)
        {
            context.Carriers.AddOrUpdate(new Carrier
            {
                Email = "{0}@tmomail.net",
                Id = 1,
                Name = "T-Mobile"
            });
            context.Carriers.AddOrUpdate(new Carrier
            {
                Email = "{0}@vmobl.com",
                Id = 2,
                Name = "Virgin Mobile"
            });
            context.Carriers.AddOrUpdate(new Carrier
            {
                Email = "{0}@cingularme.com",
                Id = 3,
                Name = "Cingular"
            });
            context.Carriers.AddOrUpdate(new Carrier
            {
                Email = "{0}@messaging.sprintpcs.com",
                Id = 4,
                Name = "Sprint"
            });
            context.Carriers.AddOrUpdate(new Carrier
            {
                Email = "{0}@vtext.com",
                Id = 5,
                Name = "Verizon"
            });
            context.Carriers.AddOrUpdate(new Carrier
            {
                Email = "{0}@messaging.nextel.com",
                Id = 6,
                Name = "Nextel"
            });
            context.Carriers.AddOrUpdate(new Carrier
            {
                Email = "{0}@txt.att.net",
                Id = 7,
                Name = "AT&T"
            });
            context.Carriers.AddOrUpdate(new Carrier
            {
                Email = "{0}@MyMetroPcs.com",
                Id = 8,
                Name = "Metro PCS"
            });
            context.SaveChanges();
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 1,
                Name = "Alexander, Steve",
                PhoneNumber = "7274577167"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 2,
                Name = "Brown, Mark",
                PhoneNumber = "2489150267"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 3,
                Name = "Campbell, Colin",
                PhoneNumber = "7346466696"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 4,
                Name = "Crowley, Jim",
                PhoneNumber = "7346459480"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 5,
                Name = "Day, Jeff",
                PhoneNumber = "2487876463"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 6,
                Name = "Faussett, Brad",
                PhoneNumber = "6147873200"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 7,
                Name = "Fox, Mark",
                PhoneNumber = "3174086880"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 8,
                Name = "Hines, Chad",
                PhoneNumber = "2484705583"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 9,
                Name = "Imhoff, Jeff",
                PhoneNumber = "6162926403"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 10,
                Name = "Lomax, Paula",
                PhoneNumber = "2488905890"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 11,
                Name = "Lorenz, Dan",
                PhoneNumber = "2485148547"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 12,
                Name = "Lusk, Gary",
                PhoneNumber = "5862014323"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 13,
                Name = "Marino, Nick",
                PhoneNumber = "2482142137"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 14,
                Name = "Masri, May",
                PhoneNumber = "2485152969"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 15,
                Name = "O'Melia, Tim",
                PhoneNumber = "2485148552"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 16,
                Name = "Pearson, James",
                PhoneNumber = "6164019815"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 17,
                Name = "Ring, Rob",
                PhoneNumber = "2487630014"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 18,
                Name = "Rose, John",
                PhoneNumber = "2485148558"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 19,
                Name = "Schoeneberger, Chanel",
                PhoneNumber = "2488940789"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 20,
                Name = "Schoeneberger, Scott",
                PhoneNumber = "2484960541"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 21,
                Name = "Silverstein, Mike",
                PhoneNumber = "6164468006"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 22,
                Name = "Smith, Eric",
                PhoneNumber = "2698082530"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 23,
                Name = "Stone, Karyn",
                PhoneNumber = "2485148529"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 24,
                Name = "Tungl, Tobi",
                PhoneNumber = "6162950478"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 25,
                Name = "Vehar, Jason",
                PhoneNumber = "2485148525"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("AT&T")),
                Id = 26,
                Name = "Waldofsky, Thomas",
                PhoneNumber = "7737914785"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 27,
                Name = "Wilson, Mark",
                PhoneNumber = "6165604108"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 28,
                Name = "Yaklin, Eric",
                PhoneNumber = "2484107486"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 29,
                Name = "Yardley, Rick",
                PhoneNumber = "2483512227"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 30,
                Name = "Yerkes, Charlie",
                PhoneNumber = "8108694979"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("Verizon")),
                Id = 31,
                Name = "Ymker, Paula",
                PhoneNumber = "6164505943"
            });
            context.SalesPeople.AddOrUpdate(new SalesPerson
            {
                Carrier = context.Carriers.FirstOrDefault(c => c.Name.Equals("AT&T")),
                Id = 32,
                Name = "Gaisser, Tony",
                PhoneNumber = "7345551212"
            });
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}