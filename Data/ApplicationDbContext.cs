using Granthology.Enums;
using Granthology.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Granthology.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Relationship> Relationships { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParentChildRelationship>()
                .HasBaseType<Relationship>()
                .HasDiscriminator<string>("RelationshipTypeDiscriminator")
                .HasValue(RelationshipType.ParentChild.ToString());

            modelBuilder.Entity<PartnerRelationship>()
                .HasBaseType<Relationship>()
                .HasDiscriminator<string>("RelationshipTypeDiscriminator")
                .HasValue(RelationshipType.Partner.ToString());

            modelBuilder.Entity<Relationship>()
                                .HasOne(r => r.PersonA)
                                .WithMany(p => p.Relationships)
                                .HasForeignKey(r => r.PersonAId)
                                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
