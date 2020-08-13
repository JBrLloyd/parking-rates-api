using System;
using Carpark.Register.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;

namespace Carpark.Register.Infrastructure.Persistence.Configurations
{
    public class SpecialRateConfiguration : IEntityTypeConfiguration<SpecialRate>
    {
        public void Configure(EntityTypeBuilder<SpecialRate> builder)
        {
            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            var valueConverter = new ValueConverter<ICollection<DayOfWeek>, string>
            (
                r => string.Join(',', r),
                r => r.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), s))
                        .ToList()
            );

            builder.Property(r => r.ApplicableDaysOfWeek)
                .HasConversion(valueConverter);

            builder.Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
