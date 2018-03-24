using Newtonsoft.Json;

namespace BookBash.Models
{
    public class BacklogStatus
    {
        [JsonProperty("typeId")]
        public long TypeId { get; set; }
        [JsonProperty("typeName")]
        public string TypeName { get; set; }
        [JsonProperty("typeDescription")]
        public string TypeDescription { get; set; }
    }
}