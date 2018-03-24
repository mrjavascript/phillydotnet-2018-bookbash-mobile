using SQLite;

namespace BookBash.Models
{
    public class Preference
    {
        [PrimaryKey, AutoIncrement]
        public int preference_id { get; set; }

        public string preference_text { get; set; }

        public int preference_value { get; set; }
    }
}