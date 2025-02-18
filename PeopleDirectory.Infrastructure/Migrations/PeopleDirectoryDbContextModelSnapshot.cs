﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PeopleDirectory.Infrastructure.Data;

#nullable disable

namespace PeopleDirectory.Infrastructure.Migrations
{
    [DbContext(typeof(PeopleDirectoryDbContext))]
    partial class PeopleDirectoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PeopleDirectory.Domain.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsDeleted = false,
                            Name = "Tbilisi"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsDeleted = false,
                            Name = "Kutaisi"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsDeleted = false,
                            Name = "Batumi"
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsDeleted = false,
                            Name = "Kobuleti"
                        },
                        new
                        {
                            Id = 5,
                            CreatedDate = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            IsDeleted = false,
                            Name = "Poti"
                        });
                });

            modelBuilder.Entity("PeopleDirectory.Domain.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersonalId")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PeopleDirectory.Domain.Entities.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("PhoneNumbers");
                });

            modelBuilder.Entity("PeopleDirectory.Domain.Entities.RelatedPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConnectionType")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("RelatedPersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("RelatedPersonId");

                    b.ToTable("RelatedPersons");
                });

            modelBuilder.Entity("PeopleDirectory.Domain.Entities.Person", b =>
                {
                    b.HasOne("PeopleDirectory.Domain.Entities.City", "City")
                        .WithMany("Persons")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("PeopleDirectory.Domain.Entities.PhoneNumber", b =>
                {
                    b.HasOne("PeopleDirectory.Domain.Entities.Person", "Person")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("PeopleDirectory.Domain.Entities.RelatedPerson", b =>
                {
                    b.HasOne("PeopleDirectory.Domain.Entities.Person", "Person")
                        .WithMany("RelatedPersons")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PeopleDirectory.Domain.Entities.Person", "Related")
                        .WithMany()
                        .HasForeignKey("RelatedPersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Related");
                });

            modelBuilder.Entity("PeopleDirectory.Domain.Entities.City", b =>
                {
                    b.Navigation("Persons");
                });

            modelBuilder.Entity("PeopleDirectory.Domain.Entities.Person", b =>
                {
                    b.Navigation("PhoneNumbers");

                    b.Navigation("RelatedPersons");
                });
#pragma warning restore 612, 618
        }
    }
}
