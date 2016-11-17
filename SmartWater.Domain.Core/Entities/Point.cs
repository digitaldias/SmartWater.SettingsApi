using Newtonsoft.Json;

namespace SmartWater.Domain.Core.Entities
{
    public class Point
    {
        [JsonProperty("type")]
        public string Type { get; set; }


        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }
    }
}
