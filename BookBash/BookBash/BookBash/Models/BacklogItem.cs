using Newtonsoft.Json;

namespace BookBash.Models
{
    public class BacklogItem
    {
        [JsonProperty("recordId")]
        public long RecordId { get; set; }
        [JsonProperty("book")]
        public Book Book { get; set; }
        [JsonProperty("status")]
        public BacklogStatus Status { get; set; }
        [JsonProperty("rating")]
        public double Rating { get; set; }
    }
}