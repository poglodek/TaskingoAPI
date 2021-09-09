﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskingoAPI.Database;

namespace TaskingoAPI.Database.Migrations
{
    [DbContext(typeof(TaskingoDbContext))]
    [Migration("20210909181801_AddingAssignedUser")]
    partial class AddingAssignedUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaskingoAPI.Database.Entity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TaskingoAPI.Database.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActualStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHashed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskingoAPI.Database.Entity.WorkTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AssignedUserId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAssigned")
                        .HasColumnType("bit");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WhoCreatedId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("WhoCreatedId");

                    b.ToTable("WorkTasks");
                });

            modelBuilder.Entity("TaskingoAPI.Database.Entity.WorkTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BreakTimeInMinutes")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WorkTimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WorkTimeStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("WorkTimes");
                });

            modelBuilder.Entity("TaskingoAPI.Database.Entity.User", b =>
                {
                    b.HasOne("TaskingoAPI.Database.Entity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TaskingoAPI.Database.Entity.WorkTask", b =>
                {
                    b.HasOne("TaskingoAPI.Database.Entity.User", "AssignedUser")
                        .WithMany("WorkTasksAssigned")
                        .HasForeignKey("AssignedUserId");

                    b.HasOne("TaskingoAPI.Database.Entity.User", "WhoCreated")
                        .WithMany("WorkTasks")
                        .HasForeignKey("WhoCreatedId");

                    b.Navigation("AssignedUser");

                    b.Navigation("WhoCreated");
                });

            modelBuilder.Entity("TaskingoAPI.Database.Entity.WorkTime", b =>
                {
                    b.HasOne("TaskingoAPI.Database.Entity.User", "User")
                        .WithMany("WorkTimes")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskingoAPI.Database.Entity.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TaskingoAPI.Database.Entity.User", b =>
                {
                    b.Navigation("WorkTasks");

                    b.Navigation("WorkTasksAssigned");

                    b.Navigation("WorkTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
