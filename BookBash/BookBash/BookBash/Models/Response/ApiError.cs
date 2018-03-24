namespace BookBash.Models.Response
{
    public class ApiError
    {
        public int status {get;set;}
        public int code { get; set; }
        public string message { get; set; }
    }
}