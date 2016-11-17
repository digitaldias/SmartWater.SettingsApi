using System.Collections.Generic;

namespace SmartWater.Domain.Core.Contracts.Readers
{
    public interface IConfigurationReader
    { 
        string this[string key] { get; set; }
    }
}
