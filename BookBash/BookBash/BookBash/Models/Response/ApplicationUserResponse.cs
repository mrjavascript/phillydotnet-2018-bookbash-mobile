namespace BookBash.Models.Response
{
    public class ApplicationUserResponse
    {
        public string emailAddress { get; set; }
        public string jwt { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
    }
}