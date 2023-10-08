﻿// <auto-generated />
using System;
using Avtobus1TestTask.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Avtobus1TestTask.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231008182105_Migration1")]
    partial class Migration1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Avtobus1TestTask.Models.URL_Statistics", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LongURL")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("NumberTransitions")
                        .HasColumnType("bigint");

                    b.Property<string>("ShortURL")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("URL_Statistics");
                });
#pragma warning restore 612, 618
        }
    }
}