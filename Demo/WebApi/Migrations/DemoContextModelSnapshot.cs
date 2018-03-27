﻿// <auto-generated />
using Demo.BackgroundJobs;
using Demo.Messaging.Events;
using Demo.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebApi.Model;

namespace WebApi.Migrations
{
    [DbContext(typeof(DemoContext))]
    partial class DemoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Demo.BackgroundJobs.BackgroundJobInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<bool>("IsAbandoned");

                    b.Property<string>("JobArgs")
                        .IsRequired()
                        .HasMaxLength(1048576);

                    b.Property<string>("JobType")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<DateTimeOffset?>("LastTryTime");

                    b.Property<DateTimeOffset>("NextTryTime");

                    b.Property<byte>("Priority");

                    b.Property<short>("TryCount");

                    b.HasKey("Id");

                    b.ToTable("BackgroundJobs");
                });

            modelBuilder.Entity("Demo.IdentityServer4.PersistedGrantEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Data");

                    b.Property<DateTime?>("Expiration");

                    b.Property<string>("SubjectId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("PersistedGrants");
                });

            modelBuilder.Entity("Demo.Messaging.Events.EventLogEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationTime");

                    b.Property<Guid>("EventId");

                    b.Property<string>("EventTypeName");

                    b.Property<int>("State");

                    b.Property<int>("TimesSend");

                    b.HasKey("Id");

                    b.ToTable("EventLogEntries");
                });

            modelBuilder.Entity("Demo.Notifications.NotificationInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Data")
                        .HasMaxLength(1048576);

                    b.Property<string>("DataTypeName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityId")
                        .HasMaxLength(96);

                    b.Property<string>("EntityTypeAssemblyQualifiedName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityTypeName")
                        .HasMaxLength(250);

                    b.Property<string>("ExcludedUserIds")
                        .HasMaxLength(131072);

                    b.Property<string>("NotificationDataTypeAssemblyQualifiedName")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<string>("NotificationName")
                        .IsRequired()
                        .HasMaxLength(96);

                    b.Property<byte>("Severity");

                    b.Property<string>("UserIds")
                        .HasMaxLength(131072);

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Demo.Notifications.NotificationSubscriptionInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("EntityId")
                        .HasMaxLength(96);

                    b.Property<string>("EntityTypeAssemblyQualifiedName")
                        .HasMaxLength(512);

                    b.Property<string>("EntityTypeName")
                        .HasMaxLength(250);

                    b.Property<string>("NotificationName")
                        .HasMaxLength(96);

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("NotificationName", "EntityTypeName", "EntityId", "UserId");

                    b.ToTable("NotificationSubscriptions");
                });

            modelBuilder.Entity("Demo.Notifications.UserNotificationInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<Guid>("NotificationId");

                    b.Property<int>("State");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "State", "CreationTime");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("Demo.Security.Permissions.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("ParentId");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("Demo.Security.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Descriptions");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("NormalizedRoleName");

                    b.Property<string>("RoleName");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("DeleterUserId");

                    b.HasIndex("LastModifierUserId");

                    b.HasIndex("NormalizedRoleName");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Demo.Security.RoleClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("ClaimType");

                    b.HasIndex("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Demo.Security.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsLockoutEnabled");

                    b.Property<DateTime?>("LockoutEndDateUtc");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash")
                        .IsRequired();

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail");

                    b.HasIndex("NormalizedUserName");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Demo.Security.UserLogin", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider")
                        .IsRequired();

                    b.Property<string>("ProviderDisplayName")
                        .IsRequired();

                    b.Property<string>("ProviderKey")
                        .IsRequired();

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("LoginProvider", "ProviderKey");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Demo.Security.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("RoleId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Demo.Settings.Setting", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("WebApi.Model.CommentIcon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descriptions");

                    b.Property<int>("MapId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.ToTable("CommentIcons");
                });

            modelBuilder.Entity("WebApi.Model.GoogleRoad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("Direction");

                    b.Property<double>("Distance");

                    b.Property<int>("MapId");

                    b.Property<string>("Name");

                    b.Property<string>("Paths");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.ToTable("GoogleRoads");
                });

            modelBuilder.Entity("WebApi.Model.GoogleRoadIcon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descriptions");

                    b.Property<int>("GoogleRoadId");

                    b.Property<double>("Lat");

                    b.Property<double>("Lng");

                    b.Property<string>("Location");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("GoogleRoadId");

                    b.ToTable("GoogleRoadIcons");
                });

            modelBuilder.Entity("WebApi.Model.Map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descriptions");

                    b.Property<int>("MapTypeId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("MapTypeId");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("WebApi.Model.MapRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MapId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.HasIndex("RoleId");

                    b.ToTable("MapRoles");
                });

            modelBuilder.Entity("WebApi.Model.MapType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.ToTable("MapTypes");
                });

            modelBuilder.Entity("WebApi.Model.PermissionRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PermissionId");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("PermissionRoles");
                });

            modelBuilder.Entity("Demo.Security.Permissions.Permission", b =>
                {
                    b.HasOne("Demo.Security.Permissions.Permission", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Demo.Security.Role", b =>
                {
                    b.HasOne("Demo.Security.User", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.HasOne("Demo.Security.User", "DeleterUser")
                        .WithMany()
                        .HasForeignKey("DeleterUserId");

                    b.HasOne("Demo.Security.User", "LastModifierUser")
                        .WithMany()
                        .HasForeignKey("LastModifierUserId");
                });

            modelBuilder.Entity("Demo.Security.RoleClaim", b =>
                {
                    b.HasOne("Demo.Security.Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Demo.Security.UserLogin", b =>
                {
                    b.HasOne("Demo.Security.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Demo.Security.UserRole", b =>
                {
                    b.HasOne("Demo.Security.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApi.Model.CommentIcon", b =>
                {
                    b.HasOne("WebApi.Model.Map", "Map")
                        .WithMany("CommentIcons")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApi.Model.GoogleRoad", b =>
                {
                    b.HasOne("WebApi.Model.Map", "Map")
                        .WithMany("GoogleRoads")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApi.Model.GoogleRoadIcon", b =>
                {
                    b.HasOne("WebApi.Model.GoogleRoad", "GoogleRoad")
                        .WithMany()
                        .HasForeignKey("GoogleRoadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApi.Model.Map", b =>
                {
                    b.HasOne("WebApi.Model.MapType", "MapType")
                        .WithMany()
                        .HasForeignKey("MapTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApi.Model.MapRole", b =>
                {
                    b.HasOne("WebApi.Model.Map", "Map")
                        .WithMany()
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Demo.Security.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApi.Model.PermissionRole", b =>
                {
                    b.HasOne("Demo.Security.Permissions.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Demo.Security.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
