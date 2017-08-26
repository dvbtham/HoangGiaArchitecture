using System.Data.Entity;
using HoangGia.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using HoangGia.Model.Entities;

namespace HoangGia.Data
{
    public class HoangGiaDbContext : IdentityDbContext<ApplicationUser>
    {
        public HoangGiaDbContext()
            : base("HoangGiaConnection")
        {
        }
        public static HoangGiaDbContext Create()
        {
            return new HoangGiaDbContext();
        }

        public DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { get; set; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Follower> Followers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>()
                .HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");

            modelBuilder.Entity<IdentityUserLogin>()
                .HasKey(i => i.UserId).ToTable("ApplicationUserLogins");

            modelBuilder.Entity<IdentityRole>().ToTable("ApplicationRoles");

            modelBuilder.Entity<IdentityUserClaim>()
                .HasKey(i => i.UserId).ToTable("ApplicationUserClaims");

            modelBuilder.Entity<ProjectImage>().HasRequired(x => x.Project).WithMany(x => x.ProjectImages).WillCascadeOnDelete(false);
           
            modelBuilder.Entity<PostTag>().HasRequired(x => x.Post).WithMany(x => x.PostTags).WillCascadeOnDelete(false);
           
            modelBuilder.Entity<PostTag>().HasRequired(x => x.Tag).WithMany(x => x.PostTags).WillCascadeOnDelete(false);
            
        }
    }
}
