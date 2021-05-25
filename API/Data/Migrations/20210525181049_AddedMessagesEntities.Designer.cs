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
    [Migration("20210525181049_AddedMessagesEntities")]
    partial class AddedMessagesEntities
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

                    b.Property<int>("DateId")
                        .HasColumnType("integer");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsConsultationAdded")
                        .HasColumnType("boolean");

                    b.Property<int>("PacientId")
                        .HasColumnType("integer");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PacientId");

                    b.HasIndex("StatusId");

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

            modelBuilder.Entity("API.Entities.Consultation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("AppoinmentId")
                        .HasColumnType("integer");

                    b.Property<float?>("BMI")
                        .HasColumnType("real");

                    b.Property<float?>("BloodSugar")
                        .HasColumnType("real");

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<float?>("DiastolicBp")
                        .HasColumnType("real");

                    b.Property<string>("GeneralFeeling")
                        .HasColumnType("text");

                    b.Property<bool?>("HasRecipe")
                        .HasColumnType("boolean");

                    b.Property<int>("HeartRate")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int?>("NumberOfCigarettesPerDay")
                        .HasColumnType("integer");

                    b.Property<int>("PacientId")
                        .HasColumnType("integer");

                    b.Property<int?>("RespiratoryRate")
                        .HasColumnType("integer");

                    b.Property<float?>("SystolicBp")
                        .HasColumnType("real");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppoinmentId")
                        .IsUnique();

                    b.HasIndex("PacientId");

                    b.ToTable("Consultations");
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

                    b.Property<string>("InnerMessage")
                        .HasColumnType("text");

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

            modelBuilder.Entity("API.Entities.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("CimCode")
                        .HasColumnType("text");

                    b.Property<string>("CommercialName")
                        .HasColumnType("text");

                    b.Property<string>("Concentration")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PharmaceuticalForm")
                        .HasColumnType("text");

                    b.Property<string>("Producer")
                        .HasColumnType("text");

                    b.Property<string>("TerapeuticalAction")
                        .HasColumnType("text");

                    b.Property<string>("Valability")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("API.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateRead")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("MessageSent")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("RecipientDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("RecipientId")
                        .HasColumnType("integer");

                    b.Property<string>("RecipientUsername")
                        .HasColumnType("text");

                    b.Property<bool>("SenderDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.Property<string>("SenderUsername")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
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

            modelBuilder.Entity("API.Entities.Prescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DosageNumberPerDay")
                        .HasColumnType("integer");

                    b.Property<string>("DosageType")
                        .HasColumnType("text");

                    b.Property<string>("FoodRelation")
                        .HasColumnType("text");

                    b.Property<int>("Frequency")
                        .HasColumnType("integer");

                    b.Property<string>("Instructions")
                        .HasColumnType("text");

                    b.Property<int>("MedicineId")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfDays")
                        .HasColumnType("integer");

                    b.Property<int>("RecipeId")
                        .HasColumnType("integer");

                    b.Property<string>("Route")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MedicineId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("API.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("ConsultationId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PacientId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UniqueId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ConsultationId")
                        .IsUnique();

                    b.ToTable("Recipes");
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

            modelBuilder.Entity("API.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
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

            modelBuilder.Entity("API.Entities.Vaccine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("For")
                        .HasColumnType("text");

                    b.Property<bool>("IsCustomForUser")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("RecommendedAge")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("API.Entities.VaccineXPacient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PacientId")
                        .HasColumnType("integer");

                    b.Property<int>("VaccineId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PacientId");

                    b.HasIndex("VaccineId");

                    b.ToTable("VaccineXPacients");
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

                    b.HasOne("API.Entities.Status", "Status")
                        .WithMany("Appoinments")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Pacient");

                    b.Navigation("Status");
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

            modelBuilder.Entity("API.Entities.Consultation", b =>
                {
                    b.HasOne("API.Entities.Appoinment", "Appoinment")
                        .WithOne("Consultation")
                        .HasForeignKey("API.Entities.Consultation", "AppoinmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Pacient", "Pacient")
                        .WithMany("Consultations")
                        .HasForeignKey("PacientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appoinment");

                    b.Navigation("Pacient");
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

            modelBuilder.Entity("API.Entities.Message", b =>
                {
                    b.HasOne("API.Entities.AppUser", "Recipient")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "Sender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
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

            modelBuilder.Entity("API.Entities.Prescription", b =>
                {
                    b.HasOne("API.Entities.Medicine", "Medicine")
                        .WithMany("Prescriptions")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Recipe", "Recipe")
                        .WithMany("Prescriptions")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("API.Entities.Recipe", b =>
                {
                    b.HasOne("API.Entities.Consultation", "Consultation")
                        .WithOne("Recipe")
                        .HasForeignKey("API.Entities.Recipe", "ConsultationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consultation");
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

            modelBuilder.Entity("API.Entities.VaccineXPacient", b =>
                {
                    b.HasOne("API.Entities.Pacient", "Pacient")
                        .WithMany("ReceivedVaccines")
                        .HasForeignKey("PacientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.Vaccine", "Vaccine")
                        .WithMany("Pacients")
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pacient");

                    b.Navigation("Vaccine");
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

                    b.Navigation("MessagesReceived");

                    b.Navigation("MessagesSent");

                    b.Navigation("Pacient");
                });

            modelBuilder.Entity("API.Entities.Appoinment", b =>
                {
                    b.Navigation("Consultation");
                });

            modelBuilder.Entity("API.Entities.City", b =>
                {
                    b.Navigation("PacientContacts");
                });

            modelBuilder.Entity("API.Entities.Consultation", b =>
                {
                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("API.Entities.Doctor", b =>
                {
                    b.Navigation("Appoinments");

                    b.Navigation("Photos");

                    b.Navigation("StudiesAndExperience");

                    b.Navigation("WorkDays");
                });

            modelBuilder.Entity("API.Entities.Medicine", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("API.Entities.Pacient", b =>
                {
                    b.Navigation("Appoinments");

                    b.Navigation("Consultations");

                    b.Navigation("PacientContact");

                    b.Navigation("PacientGeneralMedicalData");

                    b.Navigation("ReceivedVaccines");
                });

            modelBuilder.Entity("API.Entities.Recipe", b =>
                {
                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("API.Entities.Region", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("API.Entities.Status", b =>
                {
                    b.Navigation("Appoinments");
                });

            modelBuilder.Entity("API.Entities.Vaccine", b =>
                {
                    b.Navigation("Pacients");
                });
#pragma warning restore 612, 618
        }
    }
}
