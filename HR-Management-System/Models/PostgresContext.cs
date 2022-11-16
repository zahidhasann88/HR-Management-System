using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace HR_Management_System.Models
{
    public partial class PostgresContext : DbContext
    {
        public PostgresContext()
        {
        }

        public PostgresContext(DbContextOptions<PostgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public object Employee { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=15Nov1998");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasAnnotation("Relational:Collation", "English_United States.1252");
            
            //u dont need to write fluent api , write data annotation, if something data annonation
            // does not have, use fluent api then
            //modelBuilder.Entity<Employee>(entity =>
            //{
            //    //entity.HasNoKey();

            //    entity.HasKey(e => e.Employeeid);

            //    entity.ToTable("employee");

            //    entity.Property(e => e.Employeeid).ValueGeneratedOnAdd().HasColumnType("int4").HasColumnName("employeeid");

            //    entity.Property(e => e.EmployeName)
            //        .HasColumnType("character varying")
            //        .HasColumnName("employename");

            //    entity.Property(e => e.Age)
            //          .HasColumnType("int4")
            //          .HasColumnName("age");


            //    entity.Property(e => e.Department)
            //          .HasColumnType("character varying")
            //          .HasColumnName("department");

            //    entity.Property(e => e.Dependentid)
            //          .HasColumnType("int4")
            //          .HasColumnName("dependentid");

            //    entity.Property(e => e.DependentName)
            //          .HasColumnType("character varying")
            //          .HasColumnName("dependentname");

            //    entity.Property(e => e.Position)
            //          .HasColumnType("character varying")
            //          .HasColumnName("position");

            //    entity.Property(e => e.Joindate)
            //          .HasColumnType("date")
            //          .HasColumnName("joindate");

            //    entity.Property(e => e.Salary)
            //          .HasColumnType("character varying")
            //          .HasColumnName("salary");

            //});

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
