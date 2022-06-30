using KolokwiumPoprawa.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Kolokwium.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<File> Files { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<Membership> Memberships { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<File>(p =>
            {
                p.HasKey(e => new { e.FileID, e.TeamID });
                p.Property(e => e.FileName).IsRequired();
                p.Property(e => e.FileExtension).IsRequired();
                p.Property(e => e.FileSize).IsRequired();
                p.HasOne(e => e.Team)
                    .WithMany(e => e.Files)
                    .HasForeignKey(e => e.TeamID)
                    .IsRequired();

                p.HasData(
                    new File { FileID = 1, FileName = "1", FileExtension = "ext1", FileSize = 340, TeamID = 1 },
                    new File { FileID = 2, FileName = "2", FileExtension = "ext2", FileSize = 320, TeamID = 3 },
                    new File { FileID = 3, FileName = "3", FileExtension = "ext1", FileSize = 402, TeamID = 4 },
                    new File { FileID = 4, FileName = "4", FileExtension = "ext2", FileSize = 502, TeamID = 1 },
                    new File { FileID = 5, FileName = "5", FileExtension = "ext3", FileSize = 224, TeamID = 2 },
                    new File { FileID = 6, FileName = "6", FileExtension = "ext4", FileSize = 19, TeamID = 4 },
                    new File { FileID = 7, FileName = "7", FileExtension = "ext1", FileSize = 84, TeamID = 2 },
                    new File { FileID = 8, FileName = "8", FileExtension = "ext2", FileSize = 921, TeamID = 1 }
               );
            });

            modelBuilder.Entity<Team>(p =>
            {
                p.HasKey(e => e.TeamID);
                p.Property(e => e.TeamName).IsRequired();
                p.Property(e => e.TeamDescription);
                p.HasOne(e => e.Organization)
                    .WithMany(e => e.Teams)
                    .HasForeignKey(e => e.OrganizationID)
                    .IsRequired();

                p.HasData(
                    new Team { TeamID = 1, TeamName = "1", TeamDescription = "1", OrganizationID = 1 },
                    new Team { TeamID = 2, TeamName = "2", TeamDescription = "2", OrganizationID = 2 },
                    new Team { TeamID = 3, TeamName = "3", TeamDescription = "3", OrganizationID = 1 },
                    new Team { TeamID = 4, TeamName = "4", TeamDescription = "4", OrganizationID = 2 }
                );
            });

            modelBuilder.Entity<Organization>(p =>
            {
                p.HasKey(e => e.OrganizationID);
                p.Property(e => e.OrganizationName).IsRequired();
                p.Property(e => e.OrganizationDomain);
                p.HasData(
                    new Organization { OrganizationID = 1, OrganizationName = "1", OrganizationDomain = "1" },
                    new Organization { OrganizationID = 2, OrganizationName = "2", OrganizationDomain = "2" }
                );
            });

            modelBuilder.Entity<Member>(p =>
            {
                p.HasKey(e => e.MemberID);
                p.Property(e => e.MemberName).IsRequired();
                p.Property(e => e.MemberSurname).IsRequired();
                p.Property(e => e.MemberNickName);
                p.HasOne(e => e.Organization)
                    .WithMany(e => e.Members)
                    .HasForeignKey(e => e.OrganizationID)
                    .IsRequired();

                p.HasData(
                    new Member { MemberID = 1, MemberName = "1", MemberSurname = "1", OrganizationID = 1 },
                    new Member { MemberID = 2, MemberName = "2", MemberSurname = "2", OrganizationID = 2 },
                    new Member { MemberID = 3, MemberName = "3", MemberSurname = "3", OrganizationID = 1 },
                    new Member { MemberID = 4, MemberName = "4", MemberSurname = "4", OrganizationID = 2 }
                );
            });

            modelBuilder.Entity<Membership>(p =>
            {
                p.HasKey(e => new { e.MemberID, e.TeamID });
                p.Property(e => e.MembershipDate).IsRequired();

                p.HasOne(e => e.Team)
                    .WithMany(e => e.Memberships)
                    .HasForeignKey(e => e.TeamID)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);

                p.HasOne(e => e.Member)
                    .WithMany(e => e.Memberships)
                    .HasForeignKey(e => e.MemberID)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);

                p.HasData(
                    new Membership { MemberID = 3, TeamID = 3, MembershipDate = DateTime.Parse("2019-07-10") },
                    new Membership { MemberID = 2, TeamID = 4, MembershipDate = DateTime.Parse("2017-03-09") },
                    new Membership { MemberID = 3, TeamID = 2, MembershipDate = DateTime.Parse("2018-06-15") },
                    new Membership { MemberID = 1, TeamID = 1, MembershipDate = DateTime.Parse("2015-08-10") },
                    new Membership { MemberID = 4, TeamID = 3, MembershipDate = DateTime.Parse("2019-04-13") },
                    new Membership { MemberID = 2, TeamID = 1, MembershipDate = DateTime.Parse("2022-12-02") }
                );
            });
        }
    }
}
