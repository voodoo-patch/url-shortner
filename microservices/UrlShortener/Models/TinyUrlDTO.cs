namespace UrlShortener.Models
{
    public class TinyUrlDTO
    {
        public string Id {get;set;}
        public string ShortUrl {get;set;}
        public string OriginalUrl {get;set;}
        public string UserId {get;set;}
    }
}