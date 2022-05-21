using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Todo.Domain
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TodoStatus
    {
        Pending = 5,
        Done = 6,
    }
}
