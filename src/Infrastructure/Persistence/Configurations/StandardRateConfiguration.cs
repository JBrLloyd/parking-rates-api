using Carpark.Register.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carpark.Register.Infrastructure.Persistence.Configurations
{
    public class StandardRateConfiguration : IEntityTypeConfiguration<StandardRate>
    {
        public void Configure(EntityTypeBuilder<StandardRate> builder)
        {
            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder.Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
