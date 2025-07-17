using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Site.Domain._shared;
using Site.Domain.Agents;
using Site.Domain.Projects;

namespace Site.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Ignore<BaseDomainEvent>();  // اگر از domain event استفاده می‌کنی

            builder.Entity<Agent>(agent =>
            {
                agent.OwnsOne(a => a.PhoneNumber, phone =>
                {
                    phone.Property(p => p.Value)
                        .HasColumnName("PhoneNumber")
                        .HasMaxLength(11)
                        .IsRequired();
                });

                agent.OwnsMany(a => a.AgentFeatures, group =>
                {
                    group.ToTable("AgentFeatures");

                    group.WithOwner().HasForeignKey("AgentId");

                    group.Property(g => g.Title)
                        .HasMaxLength(200)
                        .IsRequired();

                    group.HasKey("Id");
                });

                agent.Property(a => a.FullName).IsRequired().HasMaxLength(100);
                agent.Property(a => a.GithubLink).HasMaxLength(300);
                agent.Property(a => a.ImageName).HasMaxLength(200);
                agent.Property(a => a.Description).HasMaxLength(1000);
                agent.Property(a => a.Email).HasMaxLength(200);
                agent.Property(a => a.Slug).HasMaxLength(200);
            });
        

        builder.Entity<Project>(project =>
            {
                project.Property(p => p.Title).IsRequired().HasMaxLength(200);
                project.Property(p => p.Slug).IsRequired().HasMaxLength(200);
                project.Property(p => p.Description).HasMaxLength(1000);
                project.Property(p => p.MainImageName).HasMaxLength(300);

                project.HasMany(p => p.Images)
                    .WithOne(pi => pi.Project)
                    .HasForeignKey(pi => pi.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);

                project.HasMany(p => p.Agents)
                    .WithMany();
            });

            builder.Entity<ProjectImage>(img =>
            {
                img.Property(i => i.ImageName)
                    .IsRequired()
                    .HasMaxLength(200);
            });
        }

    }
}
