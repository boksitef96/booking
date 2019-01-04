using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Booking.Models;   

namespace Booking.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<City> Cities { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Accomodation>().HasKey(x => x.Id)
        //                                 .HasMany(x => x.Rooms)
        //                                 .WithRequired(r=>r.Accomodation)
        //                                 .WillCascadeOnDelete();

        //    modelBuilder.Entity<Reservation>().HasKey(x => x.Id)
        //                                .HasRequired(r=>r.Room)
        //                                .WithMany()
        //                                 //.HasForeignKey(x => x.Room)
        //                                .WillCascadeOnDelete();

        //    modelBuilder.Entity<Room>().HasKey(x => x.Id);

        //    //modelBuilder.Entity<IdentityUserRole>()
        //    //                           .HasRequired(r => r.UserId)
        //    //                            .WithMany()
        //    //                             //  .HasForeignKey(x => x.UserId)
        //    //                            .WillCascadeOnDelete();

        //    //modelBuilder.Entity<IdentityUserRole>()
        //    //                                .HasRequired(r => r.RoleId)
        //    //                                 .WithMany()
        //    //                               //     .HasForeignKey(x => x.RoleId)
        //    //                                 .WillCascadeOnDelete();

        //    //modelBuilder.Entity<IdentityUserLogin>()
        //    //                               .HasRequired(r => r.UserId)
        //    //                                .WithMany()
        //    //                               // .HasForeignKey(x=>x.UserId)
        //    //                                .WillCascadeOnDelete();

        //    //modelBuilder.Entity<IdentityUserClaim>().HasKey(x=>x.Id)
        //    //                            .HasRequired(r => r.UserId)
        //    //                             .WithMany()
        //    //                             //.HasForeignKey(x => x.UserId)
        //    //                             .WillCascadeOnDelete();

        //    modelBuilder.Entity<IdentityRole>().HasKey(x => x.Id);

        //    modelBuilder.Entity<IdentityUser>().HasKey(x => x.Id);

        //    base.OnModelCreating(modelBuilder);
        //}

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}