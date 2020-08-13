using Carpark.Register.Domain.Enums;

namespace Carpark.Register.Domain.Common
{
    public class BaseRate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RateType RateType { get; set; }
        public double Rate { get; set; }
    }
}
