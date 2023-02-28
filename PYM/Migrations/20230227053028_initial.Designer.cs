﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PYM;

#nullable disable

namespace PYM.Migrations
{
    [DbContext(typeof(PYMContext))]
    [Migration("20230227053028_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IssueLabel", b =>
                {
                    b.Property<int>("IssuesIssueId")
                        .HasColumnType("int");

                    b.Property<int>("LabelsLabelId")
                        .HasColumnType("int");

                    b.HasKey("IssuesIssueId", "LabelsLabelId");

                    b.HasIndex("LabelsLabelId");

                    b.ToTable("IssueLabel");
                });

            modelBuilder.Entity("PYM.models.Issue", b =>
                {
                    b.Property<int>("IssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AssigneeUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("ReporterUserId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IssueId");

                    b.HasIndex("AssigneeUserId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ReporterUserId");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("PYM.Models.Label", b =>
                {
                    b.Property<int>("LabelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LabelName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("LabelId");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("PYM.models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatorUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ProjectId");

                    b.HasIndex("CreatorUserId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("PYM.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("PYM.models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesRoleId")
                        .HasColumnType("int");

                    b.Property<int>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("RolesRoleId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("IssueLabel", b =>
                {
                    b.HasOne("PYM.models.Issue", null)
                        .WithMany()
                        .HasForeignKey("IssuesIssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PYM.Models.Label", null)
                        .WithMany()
                        .HasForeignKey("LabelsLabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PYM.models.Issue", b =>
                {
                    b.HasOne("PYM.models.User", "Assignee")
                        .WithMany("IssuesAssigned")
                        .HasForeignKey("AssigneeUserId");

                    b.HasOne("PYM.models.Project", "Project")
                        .WithMany("Issue")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PYM.models.User", "Reporter")
                        .WithMany("IssuesCreated")
                        .HasForeignKey("ReporterUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignee");

                    b.Navigation("Project");

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("PYM.models.Project", b =>
                {
                    b.HasOne("PYM.models.User", "Creator")
                        .WithMany("Projects")
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("PYM.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PYM.models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PYM.models.Project", b =>
                {
                    b.Navigation("Issue");
                });

            modelBuilder.Entity("PYM.models.User", b =>
                {
                    b.Navigation("IssuesAssigned");

                    b.Navigation("IssuesCreated");

                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
