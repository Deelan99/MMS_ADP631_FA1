﻿// <auto-generated />
using System;
using MMS_ADP631_FA1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MMS_ADP631_FA1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250329121701_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("MMS_ADP631_FA1.Models.Citizen", b =>
                {
                    b.Property<int>("CitizenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("TEXT");

                    b.HasKey("CitizenID");

                    b.ToTable("Citizens");

                    b.HasData(
                        new
                        {
                            CitizenID = 1,
                            Address = "84 Foggy Hollow Rd",
                            DateOfBirth = new DateTime(1993, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "zara.windcroft@mail.com",
                            FullName = "Zara Windcroft",
                            PhoneNumber = "5554321987",
                            RegistrationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CitizenID = 2,
                            Address = "22 Maple Creek",
                            DateOfBirth = new DateTime(1987, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "elton.bravo@mail.com",
                            FullName = "Elton Braverman",
                            PhoneNumber = "5559872345",
                            RegistrationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CitizenID = 3,
                            Address = "310 Sunset Boulevard",
                            DateOfBirth = new DateTime(1995, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lyra.monty@mail.com",
                            FullName = "Lyra Montclair",
                            PhoneNumber = "5551239087",
                            RegistrationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CitizenID = 4,
                            Address = "15 Oakridge Lane",
                            DateOfBirth = new DateTime(1990, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "dexter.flan@mail.com",
                            FullName = "Dexter Flannigan",
                            PhoneNumber = "5556781234",
                            RegistrationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MMS_ADP631_FA1.Models.Report", b =>
                {
                    b.Property<int>("ReportID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CitizenID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ReportType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ReportID");

                    b.HasIndex("CitizenID");

                    b.ToTable("Reports");

                    b.HasData(
                        new
                        {
                            ReportID = 1,
                            CitizenID = 1,
                            Details = "Broken street light on Foggy Hollow Rd",
                            ReportType = "Complaint",
                            Status = "Under Review",
                            SubmissionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ReportID = 2,
                            CitizenID = 3,
                            Details = "Water overflow at Sunset Boulevard",
                            ReportType = "Incident Report",
                            Status = "Resolved",
                            SubmissionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ReportID = 3,
                            CitizenID = 4,
                            Details = "Quick response on pothole repair",
                            ReportType = "Feedback",
                            Status = "Closed",
                            SubmissionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MMS_ADP631_FA1.Models.ServiceRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CitizenID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("RequestID");

                    b.HasIndex("CitizenID");

                    b.ToTable("ServiceRequests");

                    b.HasData(
                        new
                        {
                            RequestID = 1,
                            CitizenID = 1,
                            RequestDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceType = "Street Light Repair",
                            Status = "Pending"
                        },
                        new
                        {
                            RequestID = 2,
                            CitizenID = 2,
                            RequestDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceType = "Garbage Collection Delay",
                            Status = "Completed"
                        },
                        new
                        {
                            RequestID = 3,
                            CitizenID = 3,
                            RequestDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceType = "Water Pressure Issue",
                            Status = "In Progress"
                        },
                        new
                        {
                            RequestID = 4,
                            CitizenID = 4,
                            RequestDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceType = "Road Pothole Repair",
                            Status = "Pending"
                        });
                });

            modelBuilder.Entity("MMS_ADP631_FA1.Models.Staff", b =>
                {
                    b.Property<int>("StaffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StaffID");

                    b.ToTable("Staff");

                    b.HasData(
                        new
                        {
                            StaffID = 1,
                            Department = "Infrastructure",
                            Email = "marcel.thornby@municipality.com",
                            FullName = "Marcel Thornby",
                            HireDate = new DateTime(2015, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhoneNumber = "5556549821",
                            Position = "Chief Engineer"
                        },
                        new
                        {
                            StaffID = 2,
                            Department = "Communications",
                            Email = "lila.hawthorne@municipality.com",
                            FullName = "Lila Hawthorne",
                            HireDate = new DateTime(2018, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhoneNumber = "5558912345",
                            Position = "Public Relations Officer"
                        },
                        new
                        {
                            StaffID = 3,
                            Department = "Finance",
                            Email = "eugene.whitlock@municipality.com",
                            FullName = "Eugene Whitlock",
                            HireDate = new DateTime(2012, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhoneNumber = "5557612984",
                            Position = "Finance Manager"
                        });
                });

            modelBuilder.Entity("MMS_ADP631_FA1.Models.Report", b =>
                {
                    b.HasOne("MMS_ADP631_FA1.Models.Citizen", "Citizen")
                        .WithMany("Reports")
                        .HasForeignKey("CitizenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");
                });

            modelBuilder.Entity("MMS_ADP631_FA1.Models.ServiceRequest", b =>
                {
                    b.HasOne("MMS_ADP631_FA1.Models.Citizen", "Citizen")
                        .WithMany("ServiceRequests")
                        .HasForeignKey("CitizenID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Citizen");
                });

            modelBuilder.Entity("MMS_ADP631_FA1.Models.Citizen", b =>
                {
                    b.Navigation("Reports");

                    b.Navigation("ServiceRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
