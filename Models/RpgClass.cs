using System.Text.Json.Serialization;

namespace MessingAroundWithDotNet.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Zombie = 1,
        Vampire = 2,
        Werewolf = 3
    }
}