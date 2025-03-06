using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SIBSAPI.DTOs;
using SIBSAPI.Models;
using SIBSAPI.Services;
//using SIBSAPI.Models;

namespace SIBSAPI.Data
{
    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    //{
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //        : base(options)
    //    { }

    //    public DbSet<EmployeeDetail> EmployeeDetail { get; set; }
    //    public DbSet<TLogin> Login { get; set; }


    //}
    //public class ApplicationDbContext : DbContext
    //{
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    //        : base(options)
    //    { }

    //    public DbSet<EmployeeDetail> EmployeeDetail { get; set; }
    //    public DbSet<TLogin> Login { get; set; }


    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<TLogin>().ToTable("Login"); // Explicitly specify table name
    //    }
    //}

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<EmployeeDetail> EmployeeDetail { get; set; }
        public DbSet<TLogin> Login { get; set; }
        public DbSet<PersonalMst> PersonalMst { get; set; }
        public DbSet<EmpMst> EmpMst { get; set; }
        public DbSet<ApplicationIncome> ApplicationIncome { get; set; }
        public DbSet<PAY_MASTER> PAY_MASTER { get; set; }
        public DbSet<DAYS_MST> DAYS_MST { get; set; }
        public DbSet<Deduction> Deduction { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TLogin>().ToTable("Login");
            modelBuilder.Entity<PersonalMst>().ToTable("PERSONAL_MST");
            modelBuilder.Entity<EmpMst>().ToTable("emp_mst");
            modelBuilder.Entity<ApplicationIncome>().ToTable("Applicable_Incomes");
            modelBuilder.Entity<PAY_MASTER>().ToTable("PAY_MASTER").HasKey("grade");
            modelBuilder.Entity<DAYS_MST>().ToTable("DAYS_MST").HasKey("emp_no");
            modelBuilder.Entity<Deduction>().ToTable("Deduction").HasKey("emp_no");

        }

    }




}
