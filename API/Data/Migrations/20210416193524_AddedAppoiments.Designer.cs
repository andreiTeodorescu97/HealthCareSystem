﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace API.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210416193524_AddedAppoiments")]
    partial class AddedAppoiments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("API.Entities.Appoinment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("AppoinmentDate")
                        .HasColumnType("text");

                    b.Property<int>("AppoinmentEndSpan")
                        .HasColumnType("integer");

                    b.Property<string>("AppoinmentHour")
                        .HasColumnType("text");

                    b.Property<int>("AppoinmentStartSpan")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<int>("PacientId")
                        .HasColumnType("integer");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PacientId");

                    b.ToTable("Appoinments");
                });

            modelBuilder.Entity("API.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("RegionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("API.Entities.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("HasWorkDays")
                        .HasColumnType("boolean");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("API.Entities.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<string>("Route")
                        .HasColumnType("text");

                    b.Property<string>("Section")
                        .HasColumnType("text");

                    b.Property<string>("StackTrace")
                        .HasColumnType("text");

                    b.Property<int>("StatusCode")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("API.Entities.Pacient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CNP")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("IdentityNumber")
                        .HasColumnType("text");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.Property<string>("Series")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Pacients");
                });

            modelBuilder.Entity("API.Entities.PacientContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("CityId")
                        .HasColumnType("integer");

                    b.Property<string>("FirstPhone")
                        .HasColumnType("text");

                    b.Property<int>("PacientId")
                        .HasColumnType("integer");

                    b.Property<string>("SecondPhone")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<int?>("StreetNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("PacientId")
                        .IsUnique();

                    b.ToTable("PacientContact");
                });

            modelBuilder.Entity("API.Entities.PacientGeneralMedicalData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("BloodType")
                        .HasColumnType("text");

                    b.Property<string>("GeneticDiseases")
                        .HasColumnType("text");

                    b.Property<float>("HeightBirth")
                        .HasColumnType("real");

                    b.Property<bool>("IsSmoker")
                        .HasColumnType("boolean");

                    b.Property<int>("NumberOfAvortions")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfBirths")
                        .HasColumnType("integer");

                    b.Property<int>("PacientId")
                        .HasColumnType("integer");

                    b.Property<float>("WeightBirth")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("PacientId")
                        .IsUnique();

                    b.ToTable("PacientGeneralMedicalData");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<string>("PublicId")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("API.Entities.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("API.Entities.StudiesAndExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("StudiesAndExperiences");
                });

            modelBuilder.Entity("API.Entities.WorkDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Day")
                        .HasColumnType("text");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndHour")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("EndTimeSpan")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartHour")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("StartTimeSpan")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("WorkDays");
                });

            modelBuilder.Entity("API.Entities.Appoinment", b =>
                {
                    b.HasOne("API.Entities.Doctor", "Doctor")
                        .WithMany("Appoinments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Pacient", "Pacient")
                        .WithMany("Appoinments")
                        .HasForeignKey("PacientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Pacient");
                });

            modelBuilder.Entity("API.Entities.City", b =>
                {
                    b.HasOne("API.Entities.Region", "Region")
                        .WithMany("Cities")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("API.Entities.Doctor", b =>
                {
                    b.HasOne("API.Entities.AppUser", "User")
                        .WithOne("Doctor")
                        .HasForeignKey("API.Entities.Doctor", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.Pacient", b =>
                {
                    b.HasOne("API.Entities.AppUser", "User")
                        .WithOne("Pacient")
                        .HasForeignKey("API.Entities.Pacient", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("API.Entities.PacientContact", b =>
                {
                    b.HasOne("API.Entities.City", "City")
                        .WithMany("PacientContacts")
                        .HasForeignKey("CityId");

                    b.HasOne("API.Entities.Pacient", "Pacient")
                        .WithOne("PacientContact")
                        .HasForeignKey("API.Entities.PacientContact", "PacientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Pacient");
                });

            modelBuilder.Entity("API.Entities.PacientGeneralMedicalData", b =>
                {
                    b.HasOne("API.Entities.Pacient", "Pacient")
                        .WithOne("PacientGeneralMedicalData")
                        .HasForeignKey("API.Entities.PacientGeneralMedicalData", "PacientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pacient");
                });

            modelBuilder.Entity("API.Entities.Photo", b =>
                {
                    b.HasOne("API.Entities.Doctor", "Doctor")
                        .WithMany("Photos")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("API.Entities.StudiesAndExperience", b =>
                {
                    b.HasOne("API.Entities.Doctor", "Doctor")
                        .WithMany("StudiesAndExperience")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("API.Entities.WorkDay", b =>
                {
                    b.HasOne("API.Entities.Doctor", "Doctor")
                        .WithMany("WorkDays")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Navigation("Doctor");

                    b.Navigation("Pacient");
                });

            modelBuilder.Entity("API.Entities.City", b =>
                {
                    b.Navigation("PacientContacts");
                });

            modelBuilder.Entity("API.Entities.Doctor", b =>
                {
                    b.Navigation("Appoinments");

                    b.Navigation("Photos");

                    b.Navigation("StudiesAndExperience");

                    b.Navigation("WorkDays");
                });

            modelBuilder.Entity("API.Entities.Pacient", b =>
                {
                    b.Navigation("Appoinments");

                    b.Navigation("PacientContact");

                    b.Navigation("PacientGeneralMedicalData");
                });

            modelBuilder.Entity("API.Entities.Region", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
