﻿// <auto-generated />
using System;
using LHDTV.Models.DbEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LHDTV.Migrations
{
    [DbContext(typeof(LHDTVContext))]
    partial class LHDTVContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LHDTV.Models.DbEntity.Extra", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("extras")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.ToTable("Extra");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.FileTags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FolderDbId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FolderDbId");

                    b.ToTable("FileTags");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.FolderDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AutoStart")
                        .HasColumnType("bit");

                    b.Property<int?>("DefaultPhotoId")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("ShowTime")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Transition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DefaultPhotoId");

                    b.HasIndex("UserId");

                    b.ToTable("Folder");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.PhotoDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RealDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Size")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.PhotoFolderMap", b =>
                {
                    b.Property<int>("PhotoId")
                        .HasColumnType("int");

                    b.Property<int>("FolderId")
                        .HasColumnType("int");

                    b.HasKey("PhotoId", "FolderId");

                    b.HasIndex("FolderId");

                    b.ToTable("PhotoFolderMap");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.PhotoTagsTypes", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Extra1Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Extra2Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Extra3Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Name");

                    b.HasIndex("Extra1Name");

                    b.HasIndex("Extra2Name");

                    b.HasIndex("Extra3Name");

                    b.ToTable("TagTypeMaster");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.PhotoTransition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("DefaultTransitionAutoStart")
                        .HasColumnType("bit");

                    b.Property<int>("DefaultTransitionTime")
                        .HasColumnType("int");

                    b.Property<string>("TransitionName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransitionUserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PhotoTransition");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.TagDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Extra1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Extra2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Extra3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhotoDbId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PhotoDbId");

                    b.ToTable("TagDb");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.UserDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Dni")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpirationTokenDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProfilePhotoId")
                        .HasColumnType("int");

                    b.Property<string>("RecovertyToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProfilePhotoId")
                        .IsUnique()
                        .HasFilter("[ProfilePhotoId] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.FileTags", b =>
                {
                    b.HasOne("LHDTV.Models.DbEntity.FolderDb", null)
                        .WithMany("PhotosTags")
                        .HasForeignKey("FolderDbId");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.FolderDb", b =>
                {
                    b.HasOne("LHDTV.Models.DbEntity.PhotoDb", "DefaultPhoto")
                        .WithMany()
                        .HasForeignKey("DefaultPhotoId");

                    b.HasOne("LHDTV.Models.DbEntity.UserDb", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.PhotoFolderMap", b =>
                {
                    b.HasOne("LHDTV.Models.DbEntity.FolderDb", "Folder")
                        .WithMany("PhotosFolder")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LHDTV.Models.DbEntity.PhotoDb", "Photo")
                        .WithMany("PhotosFolder")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.PhotoTagsTypes", b =>
                {
                    b.HasOne("LHDTV.Models.DbEntity.Extra", "Extra1")
                        .WithMany()
                        .HasForeignKey("Extra1Name");

                    b.HasOne("LHDTV.Models.DbEntity.Extra", "Extra2")
                        .WithMany()
                        .HasForeignKey("Extra2Name");

                    b.HasOne("LHDTV.Models.DbEntity.Extra", "Extra3")
                        .WithMany()
                        .HasForeignKey("Extra3Name");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.TagDb", b =>
                {
                    b.HasOne("LHDTV.Models.DbEntity.PhotoDb", null)
                        .WithMany("Tag")
                        .HasForeignKey("PhotoDbId");
                });

            modelBuilder.Entity("LHDTV.Models.DbEntity.UserDb", b =>
                {
                    b.HasOne("LHDTV.Models.DbEntity.PhotoDb", "ProfilePhoto")
                        .WithOne("User")
                        .HasForeignKey("LHDTV.Models.DbEntity.UserDb", "ProfilePhotoId");
                });
#pragma warning restore 612, 618
        }
    }
}
