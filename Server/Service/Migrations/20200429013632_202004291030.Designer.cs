﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.Databases.Sql;

namespace Service.Migrations
{
    [DbContext(typeof(ServerContext))]
    [Migration("20200429013632_202004291030")]
    partial class _202004291030
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Service.Databases.Sql.Models.Cube", b =>
                {
                    b.Property<int>("CubeId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<byte>("Lv")
                        .HasColumnType("tinyint");

                    b.Property<int>("Parts")
                        .HasColumnType("int");

                    b.HasKey("CubeId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Cubes");
                });

            modelBuilder.Entity("Service.Databases.Sql.Models.CubeData", b =>
                {
                    b.Property<int>("CubeId")
                        .HasColumnType("int");

                    b.Property<float>("AD")
                        .HasColumnType("real");

                    b.Property<float>("AS")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("CubeId");

                    b.ToTable("CubeDatas");
                });

            modelBuilder.Entity("Service.Databases.Sql.Models.Entry", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("SlotsJoin")
                        .IsRequired()
                        .HasColumnName("Slots")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("Service.Databases.Sql.Models.Manager", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("Service.Databases.Sql.Models.SkillData", b =>
                {
                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.Property<float>("Duration")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Percent")
                        .HasColumnType("real");

                    b.HasKey("SkillId");

                    b.ToTable("SkillDatas");
                });

            modelBuilder.Entity("Service.Databases.Sql.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<int>("Money")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Service.Databases.Sql.Models.Cube", b =>
                {
                    b.HasOne("Service.Databases.Sql.Models.CubeData", "CubeData")
                        .WithMany()
                        .HasForeignKey("CubeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.Databases.Sql.Models.User", "User")
                        .WithMany("Cubes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Service.Databases.Sql.Models.Entry", b =>
                {
                    b.HasOne("Service.Databases.Sql.Models.User", "User")
                        .WithOne("Entry")
                        .HasForeignKey("Service.Databases.Sql.Models.Entry", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
