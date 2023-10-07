using Avtobus1TestTask.Data;
using Avtobus1TestTask.Models;

namespace Avtobus1TestTask.Services.Interfaces
{
    public interface IUrlShortenerService
    {
        Task<string> ShortenUrlAsync(string longUrl);
        Task<string> ExpandUrlAsync(string shortUrl);
    }
}
