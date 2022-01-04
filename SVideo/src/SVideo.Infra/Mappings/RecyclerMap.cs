using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SVideo.Domain.Entities;

namespace SVideo.Infra.Mappings
{
    class RecyclerMap : IEntityTypeConfiguration<Recycler>
    {
        public void Configure(EntityTypeBuilder<Recycler> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever();
        }
    }
}
