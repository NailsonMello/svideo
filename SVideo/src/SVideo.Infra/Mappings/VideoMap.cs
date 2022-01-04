using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SVideo.Domain.Entities;

namespace SVideo.Infra.Mappings
{
    public class VideoMap : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(e => e.Server)
            .WithMany(f => f.Videos)
            .HasForeignKey(e => e.IdServer);
        }
    }
}
