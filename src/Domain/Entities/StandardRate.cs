using Carpark.Register.Domain.Common;

namespace Carpark.Register.Domain.Entities
{
    public class StandardRate : BaseRate
    {
        public double MaximumRate { get; set; }
    }
}
