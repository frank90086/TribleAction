using Microsoft.EntityFrameworkCore;

namespace TribleAction.Entities
{
    public class TeamMap
    {
        public TeamMap(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Team>();

            builder.ToTable("Team");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
            builder.Property(x => x.Name).HasColumnName("Name").IsRequired();
            builder.Property(x => x.Score).HasColumnName("Score").IsRequired();

            builder.HasMany(x => x.Member).WithOne(x => x.Team).HasForeignKey(x => x.TeamId);
        }
    }
}