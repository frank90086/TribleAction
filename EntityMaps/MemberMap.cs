using Microsoft.EntityFrameworkCore;

namespace TribleAction.Entities
{
    public class MemberMap
    {
        public MemberMap(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Member>();

            builder.ToTable("Member");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
            builder.Property(x => x.TeamId).HasColumnName("TeamId").IsRequired();
            builder.Property(x => x.Name).HasColumnName("Name").IsRequired();
            builder.Property(x => x.Score).HasColumnName("Score").IsRequired();

            builder.HasMany(x => x.Lottery).WithOne(x => x.Member).HasForeignKey(x => x.MemberId);
        }
    }
}