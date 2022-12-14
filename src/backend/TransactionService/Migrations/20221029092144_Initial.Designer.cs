// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransactionService.Data;

#nullable disable

namespace TransactionService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221029092144_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TransactionService.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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

            modelBuilder.Entity("TransactionService.Models.Storage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Storages");

                    b.HasData(
                        new
                        {
                            Id = new Guid("15ec584d-b6fb-4fd6-b064-76cc9e71f2e5")
                        },
                        new
                        {
                            Id = new Guid("a872e805-9c4e-4589-af4c-8ed0af421b4c")
                        });
                });

            modelBuilder.Entity("TransactionService.Models.TransactionModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StorageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("StorageId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1cf2eaab-050f-471c-b45f-cd082efcf8bd"),
                            Amount = 204,
                            ItemId = new Guid("03dec121-ac0e-4189-bd9e-68013cd175f9"),
                            StorageId = new Guid("15ec584d-b6fb-4fd6-b064-76cc9e71f2e5"),
                            Type = 0
                        },
                        new
                        {
                            Id = new Guid("84dbec22-7dc7-4319-9735-88029331fc8d"),
                            Amount = 103,
                            ItemId = new Guid("7c164a1e-84ab-44b9-9542-9c0a681172a7"),
                            StorageId = new Guid("a872e805-9c4e-4589-af4c-8ed0af421b4c"),
                            Type = 0
                        },
                        new
                        {
                            Id = new Guid("e6517314-65d4-407e-9d77-f148300701bc"),
                            Amount = 20,
                            ItemId = new Guid("8f515945-5f20-4acb-95fc-b0aa2eb91497"),
                            StorageId = new Guid("15ec584d-b6fb-4fd6-b064-76cc9e71f2e5"),
                            Type = 1
                        });
                });

            modelBuilder.Entity("TransactionService.Models.TransactionModel", b =>
                {
                    b.HasOne("TransactionService.Models.Item", "Item")
                        .WithMany("Transactions")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TransactionService.Models.Storage", "Storage")
                        .WithMany("Transactions")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("TransactionService.Models.Item", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("TransactionService.Models.Storage", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
