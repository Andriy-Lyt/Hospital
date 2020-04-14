using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalProject.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HospitalProject.Models
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

    /// <summary>
    /// Project database
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Table for publications (patients, visitors, ...) 
        /// </summary>
        public DbSet<Publication> Publications { get; set; }

        /// <summary>
        /// Table for reports (Annual Reports, By-Laws, ... )
        /// </summary>
        public DbSet<Report> Reports { get; set; }


        /// <summary>
        /// Table for files that are connected to reports (for instance: "General By-Law, March 2019" for report "By-Laws")
        /// </summary>
        public DbSet<ReportFile> ReportFiles { get; set; }

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