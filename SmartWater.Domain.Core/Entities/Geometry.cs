using Newtonsoft.Json;

namespace SmartWater.Domain.Core.Entities
{
    public class Geometry
    {
        public Geometry()
        {

        }


        public Geometry(string type)
        {
            Type = type;
        }

        [JsonProperty("type")]
        public string Type { get;  set; }
    }
}