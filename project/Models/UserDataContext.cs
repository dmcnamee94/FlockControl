namespace project.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class UserDataContext : DbContext
    {
        public UserDataContext()
            : base("name=UserDataContext")
        {
        }

        public virtual DbSet<usertable> usertables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<usertable>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.secondname)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.Postcode)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.Password)
                .IsUnicode(false);

           

        }
    }
}
