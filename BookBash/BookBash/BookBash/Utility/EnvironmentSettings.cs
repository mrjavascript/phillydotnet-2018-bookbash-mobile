namespace BookBash.Utility
{
    public static class EnvironmentSettings
    {
        //            "http://melusky.org:8080/";

        public static string ApiUrl =>
#if PRODUCTION_BUILD
            return "http://bookbash.no-ip.org:5050/bookbash/";
#else
            "http://bookbash.no-ip.org:5050/bookbash/";
#endif
    }
}