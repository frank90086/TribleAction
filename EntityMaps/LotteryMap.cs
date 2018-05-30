using Microsoft.EntityFrameworkCore;

namespace TribleAction.Entities
{
    public class LotteryMap
    {
        public LotteryMap(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<Lottery>();

            builder.ToTable("Lottery");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").IsRequired();
            builder.Property(x => x.MemberId).HasColumnName("MemberId").IsRequired();
            builder.Property(x => x.Number).HasColumnName("Number").IsRequired();
        }
    }
}