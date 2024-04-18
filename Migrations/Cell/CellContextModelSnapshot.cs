﻿// <auto-generated />
using ApiMember.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace ApiMember.Migrations.Cell;

[DbContext(typeof(CellContext))]
partial class CellContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.4")
            .HasAnnotation("Relational:MaxIdentifierLength", 64);

        MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

        modelBuilder.Entity("ApiMember.Models.Cell", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Day")
                    .IsRequired()
                    .HasColumnType("longtext");

                b.Property<string>("Hour")
                    .IsRequired()
                    .HasColumnType("longtext");

                b.Property<int>("LeaderId")
                    .HasColumnType("int");

                b.Property<string>("MembersId")
                    .IsRequired()
                    .HasColumnType("longtext");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("longtext");

                b.HasKey("Id");

                b.ToTable("Cells");
            });
#pragma warning restore 612, 618
    }
}