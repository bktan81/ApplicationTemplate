using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Web.Identity
{
    //Enable-Migrations
    //Add-Migration
    //Update-Database

    //Enable-Migrations -ContextTypeName CommonLib.Web.Identity.ApplicationDbContext
    //Add-Migration
    //Update-Database

    public class TUser : IdentityUser<long, TUserLogin, TUserRole, TUserClaim>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Status { get; set; }

        public String Culture { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }
    }

    public class TUserRole : IdentityUserRole<long> { }

    public class TRole : IdentityRole<long, TUserRole> { }

    public class TUserClaim : IdentityUserClaim<long> { }

    public class TUserLogin : IdentityUserLogin<long> { }



    public class ApplicationDbContext : IdentityDbContext<TUser, TRole, long, TUserLogin, TUserRole, TUserClaim>
    {
        public ApplicationDbContext()
            : base("IdentityDb")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TUser>().ToTable("TUsers").Property(p => p.Id).HasColumnName("UserId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // modelBuilder.Entity<IdentityUser>()
            //     .ToTable("Users");

            modelBuilder.Entity<TRole>()
                .ToTable("TRoles").Property(p => p.Id).HasColumnName("RoleId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); ;

            modelBuilder.Entity<TUserRole>()
                .ToTable("TUserRoles");

            modelBuilder.Entity<TUserLogin>()
                .ToTable("TUserLogins");

            modelBuilder.Entity<TUserClaim>()
                .ToTable("TUserClaims").Property(p => p.Id).HasColumnName("ClaimId");




            //modelBuilder.Entity<MyUser>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //modelBuilder.Entity<MyClaim>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // modelBuilder.Entity<MyRole>().Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }



}
