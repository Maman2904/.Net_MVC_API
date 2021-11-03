﻿// <auto-generated />
using Asp.netCoreMvcCrud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Asp.netCoreMvcCrud.Migrations
{
    [DbContext(typeof(KendaraanContext))]
    partial class KendaraanContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Asp.netCoreMvcCrud.Models.Kendaraan", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nama")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("model")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("merek")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("transmisi")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("tahun")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("harga")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("data_kendaraan");
                });
#pragma warning restore 612, 618
        }
    }
}
