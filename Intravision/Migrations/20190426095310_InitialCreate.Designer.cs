﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using server.Models;

namespace server.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190426095310_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("server.Models.Coin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.ToTable("Coins");
                });

            modelBuilder.Entity("server.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdminCode");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("server.Models.DeviceToCoin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CoinId");

                    b.Property<int?>("DeviceId");

                    b.Property<int>("Quantity");

                    b.Property<bool>("isBlocked");

                    b.HasKey("Id");

                    b.HasIndex("CoinId");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceToCoin");
                });

            modelBuilder.Entity("server.Models.DeviceToDrink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeviceId");

                    b.Property<int?>("DrinkId");

                    b.Property<int>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("DrinkId");

                    b.ToTable("DeviceToDrink");
                });

            modelBuilder.Entity("server.Models.Drink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("server.Models.DeviceToCoin", b =>
                {
                    b.HasOne("server.Models.Coin", "Coin")
                        .WithMany()
                        .HasForeignKey("CoinId");

                    b.HasOne("server.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId");
                });

            modelBuilder.Entity("server.Models.DeviceToDrink", b =>
                {
                    b.HasOne("server.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId");

                    b.HasOne("server.Models.Drink", "Drink")
                        .WithMany()
                        .HasForeignKey("DrinkId");
                });
#pragma warning restore 612, 618
        }
    }
}
