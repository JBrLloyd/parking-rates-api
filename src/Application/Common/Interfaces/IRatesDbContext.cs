using Carpark.Register.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Carpark.Register.Application.Common.Interfaces
{
    public interface IRatesDbContext
    {
        DbSet<StandardRate> StandardRates { get; set; }

        DbSet<SpecialRate> SpecialRates { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
