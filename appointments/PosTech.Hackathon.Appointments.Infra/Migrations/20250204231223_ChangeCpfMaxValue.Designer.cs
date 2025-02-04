﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PosTech.Hackathon.Appointments.Infra.Context;

#nullable disable

namespace PosTech.Hackathon.Appointments.Infra.Migrations
{
    [DbContext(typeof(AppointmentsDBContext))]
    [Migration("20250204231223_ChangeCpfMaxValue")]
    partial class ChangeCpfMaxValue
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PosTech.Hackathon.Appointments.Domain.Entities.AvailabilitySlot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Slot")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AvailabilitySlots");
                });

            modelBuilder.Entity("PosTech.Hackathon.Appointments.Domain.Entities.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR(14)")
                        .HasColumnName("CPF");

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR(20)")
                        .HasColumnName("CRM");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Doctors", (string)null);
                });

            modelBuilder.Entity("PosTech.Hackathon.Appointments.Domain.Entities.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR(14)")
                        .HasColumnName("CPF");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("NVARCHAR(250)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Patients", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
