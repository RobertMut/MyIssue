﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyIssue.API.Infrastructure;

namespace MyIssue.API.Infrastructure.Migrations
{
    [DbContext(typeof(MyIssueContext))]
    partial class MyIssueContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyIssue.API.Model.Client", b =>
                {
                    b.Property<decimal>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(6)
                        .HasColumnType("decimal(6,0)")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientCountry")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("ClientDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientFlatNo")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ClientNo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ClientStreet")
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("ClientStreetNo")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("MyIssue.API.Model.ClientEmployee", b =>
                {
                    b.Property<decimal>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(10)
                        .HasColumnType("decimal(10,0)")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Client")
                        .HasColumnType("decimal(6,0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("EmployeeId");

                    b.HasIndex("Client");

                    b.ToTable("ClientEmployees");
                });

            modelBuilder.Entity("MyIssue.API.Model.Employee", b =>
                {
                    b.Property<string>("EmployeeLogin")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("EmployeeNo")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<decimal>("EmployeePosition")
                        .HasColumnType("decimal(3,0)");

                    b.Property<string>("EmployeeSurname")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("EmployeeLogin");

                    b.HasIndex("EmployeePosition");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("MyIssue.API.Model.Position", b =>
                {
                    b.Property<decimal>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("decimal(3,0)")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PositionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PositionId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("MyIssue.API.Model.Task", b =>
                {
                    b.Property<decimal>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(10)
                        .HasColumnType("decimal(10,0)")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmployeeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MailId")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TaskAssignment")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal>("TaskClient")
                        .HasColumnType("decimal(6,0)");

                    b.Property<DateTime>("TaskCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("TaskDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TaskEnd")
                        .HasColumnType("datetime2");

                    b.Property<string>("TaskOwner")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("TaskStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("TaskTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("TaskType")
                        .HasColumnType("decimal(4,0)");

                    b.HasKey("TaskId");

                    b.HasIndex("TaskAssignment");

                    b.HasIndex("TaskClient");

                    b.HasIndex("TaskType");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("MyIssue.API.Model.TaskType", b =>
                {
                    b.Property<decimal>("TypeId")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(4)
                        .HasColumnType("decimal(4,0)")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TypeId");

                    b.ToTable("TaskTypes");

                    b.HasData(
                        new
                        {
                            TypeId = 1m,
                            TypeName = "Low priority"
                        },
                        new
                        {
                            TypeId = 2m,
                            TypeName = "Normal"
                        },
                        new
                        {
                            TypeId = 3m,
                            TypeName = "Urgent"
                        });
                });

            modelBuilder.Entity("MyIssue.API.Model.User", b =>
                {
                    b.Property<string>("UserLogin")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("EmployeeLogin")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<decimal>("UserType")
                        .HasColumnType("decimal(3,0)");

                    b.HasKey("UserLogin");

                    b.HasIndex("EmployeeLogin");

                    b.HasIndex("UserType");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserLogin = "Admin",
                            Password = "1234",
                            UserType = 3m
                        });
                });

            modelBuilder.Entity("MyIssue.API.Model.UserType", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(3)
                        .HasColumnType("decimal(3,0)")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");

                    b.HasData(
                        new
                        {
                            Id = 1m,
                            Name = "Locked"
                        },
                        new
                        {
                            Id = 2m,
                            Name = "User"
                        },
                        new
                        {
                            Id = 3m,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("MyIssue.API.Model.ClientEmployee", b =>
                {
                    b.HasOne("MyIssue.API.Model.Client", "Clients")
                        .WithMany("ClientEmployees")
                        .HasForeignKey("Client")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clients");
                });

            modelBuilder.Entity("MyIssue.API.Model.Employee", b =>
                {
                    b.HasOne("MyIssue.API.Model.User", "EmployeeLogins")
                        .WithOne()
                        .HasForeignKey("MyIssue.API.Model.Employee", "EmployeeLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyIssue.API.Model.Position", "Positions")
                        .WithMany("Employees")
                        .HasForeignKey("EmployeePosition")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeLogins");

                    b.Navigation("Positions");
                });

            modelBuilder.Entity("MyIssue.API.Model.Task", b =>
                {
                    b.HasOne("MyIssue.API.Model.Employee", "Employees")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskAssignment");

                    b.HasOne("MyIssue.API.Model.Client", "Clients")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyIssue.API.Model.TaskType", "TaskTypes")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clients");

                    b.Navigation("Employees");

                    b.Navigation("TaskTypes");
                });

            modelBuilder.Entity("MyIssue.API.Model.User", b =>
                {
                    b.HasOne("MyIssue.API.Model.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeLogin");

                    b.HasOne("MyIssue.API.Model.UserType", "UserTypes")
                        .WithMany("Users")
                        .HasForeignKey("UserType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("UserTypes");
                });

            modelBuilder.Entity("MyIssue.API.Model.Client", b =>
                {
                    b.Navigation("ClientEmployees");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("MyIssue.API.Model.Employee", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("MyIssue.API.Model.Position", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("MyIssue.API.Model.TaskType", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("MyIssue.API.Model.UserType", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
