namespace url_shortener.Models;

public class ShortUrlBody
{
    public string originalUrl { get; set; }
    public int userId { get; set; }
}