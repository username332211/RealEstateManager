using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Agencija.Model;

namespace Agencija.DAL
{

    // dotnet ef migrations add AnotherMigration --startup-project ../Agencija.Web --context RealEstateManagerDbContext
    // dotnet ef database update --startup-project ../Agencija.Web --context RealEstateManagerDbContext
    public class RealEstateManagerDbContext : IdentityDbContext<AppUser>
	{
		public RealEstateManagerDbContext(DbContextOptions<RealEstateManagerDbContext> options)
			: base(options)
		{

		}

		public DbSet<RealEstate> RealEstates { get; set; }
		public DbSet<Neighborhood> Neighborhoods { get; set; }
		public DbSet<Owner> Owners { get; set; }
        public DbSet<Agent> Agents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

        }

    }
}
