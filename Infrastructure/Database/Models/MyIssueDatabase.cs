using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MyIssue.Infrastructure.Database.Models
{
    public partial class MyIssueDatabase : DbContext
    {
        public MyIssueDatabase(string connectionString)
            : base(connectionString)
        {

        }

        public virtual DbSet<C__RefactorLog> C__RefactorLog { get; set; }
        public virtual DbSet<CLIENT> CLIENTS { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEES { get; set; }
        public virtual DbSet<POSITION> POSITIONS { get; set; }
        public virtual DbSet<TASK> TASKS { get; set; }
        public virtual DbSet<TASKTYPE> TASKTYPES { get; set; }
        public virtual DbSet<USER> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CLIENT>()
                .Property(e => e.clientId)
                .HasPrecision(5, 0);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.clientName)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.clientCountry)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.clientNo)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.clientStreet)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.clientStreetNo)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.clientFlatNo)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .Property(e => e.clientDesc)
                .IsUnicode(false);

            modelBuilder.Entity<CLIENT>()
                .HasMany(e => e.TASKS)
                .WithOptional(e => e.CLIENT)
                .HasForeignKey(e => e.taskClient);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.employeeLogin)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.employeeName)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.employeeSurname)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.employeeNo)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.employeePosition)
                .HasPrecision(3, 0);

            modelBuilder.Entity<EMPLOYEE>()
                .HasMany(e => e.TASKS)
                .WithOptional(e => e.EMPLOYEE)
                .HasForeignKey(e => e.taskAssignment);

            modelBuilder.Entity<EMPLOYEE>()
                .HasMany(e => e.TASKS1)
                .WithOptional(e => e.EMPLOYEE1)
                .HasForeignKey(e => e.taskOwner);

            modelBuilder.Entity<POSITION>()
                .Property(e => e.positionId)
                .HasPrecision(3, 0);

            modelBuilder.Entity<POSITION>()
                .Property(e => e.positionName)
                .IsUnicode(false);

            modelBuilder.Entity<POSITION>()
                .HasMany(e => e.EMPLOYEES)
                .WithOptional(e => e.POSITION)
                .HasForeignKey(e => e.employeePosition);

            modelBuilder.Entity<TASK>()
                .Property(e => e.taskId)
                .HasPrecision(12, 0);

            modelBuilder.Entity<TASK>()
                .Property(e => e.taskType)
                .HasPrecision(3, 0);

            modelBuilder.Entity<TASK>()
                .Property(e => e.taskTitle)
                .IsUnicode(false);

            modelBuilder.Entity<TASK>()
                .Property(e => e.taskDesc)
                .IsUnicode(false);

            modelBuilder.Entity<TASK>()
                .Property(e => e.taskOwner)
                .IsUnicode(false);

            modelBuilder.Entity<TASK>()
                .Property(e => e.taskAssignment)
                .IsUnicode(false);

            modelBuilder.Entity<TASK>()
                .Property(e => e.taskClient)
                .HasPrecision(5, 0);

            modelBuilder.Entity<TASK>()
                .Property(e => e.mailId)
                .IsUnicode(false);

            modelBuilder.Entity<TASKTYPE>()
                .Property(e => e.typeId)
                .HasPrecision(3, 0);

            modelBuilder.Entity<TASKTYPE>()
                .Property(e => e.typeName)
                .IsUnicode(false);

            modelBuilder.Entity<TASKTYPE>()
                .HasMany(e => e.TASKS)
                .WithRequired(e => e.TASKTYPE1)
                .HasForeignKey(e => e.taskType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.userLogin)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.type)
                .HasPrecision(1, 0);

            modelBuilder.Entity<USER>()
                .HasOptional(e => e.EMPLOYEE)
                .WithRequired(e => e.USER);
        }
    }
}
