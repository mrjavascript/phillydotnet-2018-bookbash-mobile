namespace BookBash.Models.Request
{
    public class UserCreateRequest
    {
        public string userName { get; set; }
        public string emailAddress { get; set; }
        public string password { get; set; }
        public string passwordConfirm { get; set; }
        public bool sendConfirmationEmail { get; set; }
    }
}