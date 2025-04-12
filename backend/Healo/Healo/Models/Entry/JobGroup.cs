using System.Text.Json.Serialization;

namespace Healo.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum JobGroup
{
    Physician,
    Pharmacy,
    Dentistry,
    PhysicalTherapy,
    Nursing
}