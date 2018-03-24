using System;
using Newtonsoft.Json;

namespace BookBash.Models
{
    public class Book
    {
        [JsonProperty("bookId")]
        public long BookId { get; set; }
        [JsonProperty("isbn")]
        public string Isbn { get; set; }
        [JsonProperty("bookTitle")]
        public string BookTitle { get; set; }
        [JsonProperty("authorName")]
        public string AuthorName { get; set; }
        [JsonProperty("datePublished")]
        public string DatePublished { get; set; }
        [JsonProperty("numberOfPages")]
        public int? NumberOfPages { get; set; }
    }
}