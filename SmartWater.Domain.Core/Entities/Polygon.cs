using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartWater.Domain.Core.Entities
{
    public class Polygon : Geometry
    {

        public Polygon()
            : base("Polygon")
        {

        }


        [JsonProperty("coordinates")]
        public double[][][] Coordinates { get; set; }
    }
}
