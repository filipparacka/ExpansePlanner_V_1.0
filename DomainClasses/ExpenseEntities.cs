namespace DomainClasses
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ExpenseEntities : DbContext
    {
        public ExpenseEntities()
            : base("name=ExpenseEntities")
        {
        }

        public virtual DbSet<CATEGORY> CATEGORY { get; set; }
        public virtual DbSet<EXPENSES> EXPENSES { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CATEGORY>()
                .Property(e => e.ID);

            modelBuilder.Entity<CATEGORY>()
                .Property(e => e.CATEGORYNAME)
                .IsUnicode(false);

            modelBuilder.Entity<CATEGORY>()
                .HasMany(e => e.EXPENSES)
                .WithRequired(e => e.CATEGORY)
                .HasForeignKey(e => e.CATEGORY_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXPENSES>()
                .Property(e => e.ID);

            modelBuilder.Entity<EXPENSES>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<EXPENSES>()
                .Property(e => e.PRICE)
                .HasPrecision(1, 0);

            modelBuilder.Entity<EXPENSES>()
                .Property(e => e.USERS_ID);

            modelBuilder.Entity<EXPENSES>()
                .Property(e => e.CATEGORY_ID);

            modelBuilder.Entity<USERS>()
                .HasKey<int>(e => e.ID);

            modelBuilder.Entity<USERS>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<USERS>()
                .HasMany(e => e.EXPENSES)
                .WithRequired(e => e.USERS)
                .HasForeignKey(e => e.USERS_ID)
                .WillCascadeOnDelete(false);
        }
    }
}
