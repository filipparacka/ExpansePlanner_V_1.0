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

        public virtual DbSet<ExpenseCategory> ExpenseCategory { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Incomes> Incomes { get; set; }
        public virtual DbSet<IncomeCategory> Incomecategory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseCategory>()
                .HasKey<int>(e => e.Id);

            modelBuilder.Entity<ExpenseCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<ExpenseCategory>()
                .HasMany(e => e.Expenses)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.Category_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Expenses>()
                .HasKey<int>(e => e.Id);

            modelBuilder.Entity<Expenses>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Expenses>()
                .Property(e => e.Price);

            modelBuilder.Entity<Expenses>()
                .Property(e => e.Users_ID);

            modelBuilder.Entity<Expenses>()
                .Property(e => e.Category_ID);

            modelBuilder.Entity<Users>()
                .HasKey<int>(e => e.Id);

            modelBuilder.Entity<Users>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Expenses)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.Users_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
               .HasMany(e => e.Incomes)
               .WithRequired(e => e.Users)
               .HasForeignKey(e => e.Users_ID)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<IncomeCategory>()
               .HasKey<int>(e => e.Id);

            modelBuilder.Entity<IncomeCategory>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<IncomeCategory>()
                .HasMany(e => e.Incomes)
                .WithRequired(e => e.Category)
                .HasForeignKey(e => e.Category_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Incomes>()
                .HasKey<int>(e => e.Id);

            modelBuilder.Entity<Incomes>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Incomes>()
                .Property(e => e.Price);

            modelBuilder.Entity<Incomes>()
                .Property(e => e.Users_ID);

            modelBuilder.Entity<Incomes>()
                .Property(e => e.Category_ID);
        }
    }
}
