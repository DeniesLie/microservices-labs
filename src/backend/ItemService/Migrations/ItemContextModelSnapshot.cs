﻿// <auto-generated />
using System;
using ItemService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ItemService.Migrations
{
    [DbContext(typeof(ItemContext))]
    partial class ItemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ItemService.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("MeasureUnitId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = new Guid("03dec121-ac0e-4189-bd9e-68013cd175f9"),
                            Name = "Javelin"
                        },
                        new
                        {
                            Id = new Guid("7c164a1e-84ab-44b9-9542-9c0a681172a7"),
                            Name = "NLAW"
                        },
                        new
                        {
                            Id = new Guid("8f515945-5f20-4acb-95fc-b0aa2eb91497"),
                            Name = "Switchblade 300"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
