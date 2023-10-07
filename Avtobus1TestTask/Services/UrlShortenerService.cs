using Avtobus1TestTask.Data;
using Avtobus1TestTask.Models;
using Avtobus1TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Avtobus1TestTask.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly ApplicationDbContext _context;

        public UrlShortenerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> ShortenUrlAsync(string longUrl)
        {
            string shortUrl = GenerateShortUrl();

            var shortUrlDB = await _context.URL_Statistics.FirstOrDefaultAsync(u => u.ShortURL == shortUrl);

            if (shortUrlDB != null)
                return await ShortenUrlAsync(longUrl);

            return shortUrl;
        }
        public async Task<string> ExpandUrlAsync(string shortUrl)
        {
            var urlEntry = await _context.URL_Statistics.FirstOrDefaultAsync(u => u.ShortURL == shortUrl);

            if (urlEntry != null)
            {
                urlEntry.NumberTransitions++;
                await _context.SaveChangesAsync();

                return urlEntry.LongURL;
            }

            return null;
        }
        private string GenerateShortUrl()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
