﻿// <auto-generated />
using System;
using FileSharing.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FileSharing.Api.Migrations
{
    [DbContext(typeof(FileSharingDbContext))]
    [Migration("20240407194150_DBContextModification")]
    partial class DBContextModification
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FileSharing.Api.Models.Chunk", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("BlobName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FileRecordId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("FileRecordId");

                    b.ToTable("Chunks");
                });

            modelBuilder.Entity("FileSharing.Api.Models.FileRecord", b =>
                {
                    b.Property<int>("FileRecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileRecordId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("FileRecordId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("FileSharing.Api.Models.MetaData", b =>
                {
                    b.Property<int>("MetaDataId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FileSize")
                        .HasColumnType("int");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreviewURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("MetaDataId");

                    b.ToTable("MetaDatas");
                });

            modelBuilder.Entity("FileSharing.Api.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FileSharing.Api.Models.Chunk", b =>
                {
                    b.HasOne("FileSharing.Api.Models.FileRecord", null)
                        .WithMany("Chunks")
                        .HasForeignKey("FileRecordId");
                });

            modelBuilder.Entity("FileSharing.Api.Models.MetaData", b =>
                {
                    b.HasOne("FileSharing.Api.Models.FileRecord", "File")
                        .WithOne("MetaData")
                        .HasForeignKey("FileSharing.Api.Models.MetaData", "MetaDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("File");
                });

            modelBuilder.Entity("FileSharing.Api.Models.FileRecord", b =>
                {
                    b.Navigation("Chunks");

                    b.Navigation("MetaData")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}