using Avtobus1TestTask.Data;
using Avtobus1TestTask.Models;
using Avtobus1TestTask.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Avtobus1TestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IUrlShortenerService _urlShortenerService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IUrlShortenerService urlShortenerService)
        {
            _logger = logger;
            _context = context;
            _urlShortenerService = urlShortenerService;
            context.Database.Migrate();
        }

        public async Task<IActionResult> Index()
        {
            var URL_Statistics = await _context.URL_Statistics.ToListAsync();
            if (URL_Statistics.Count < 1)
                ViewBag.Message = "No data";
            return _context.URL_Statistics != null ?
                        View(await _context.URL_Statistics.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.URL_Statistics' is null.");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(URL_Statistics urlStatistics)
        {
            if (ModelState.IsValid && Uri.IsWellFormedUriString(urlStatistics.LongURL, UriKind.Absolute))
            {
                var longUrlDB = await _context.URL_Statistics.FirstOrDefaultAsync(u => u.LongURL == urlStatistics.LongURL);
                if (longUrlDB != null)
                {
                    return RedirectToAction(nameof(Edit), new { Id = longUrlDB.Id });
                }
                else
                {
                    urlStatistics.ShortURL = await _urlShortenerService.ShortenUrlAsync(urlStatistics.LongURL);
                    urlStatistics.DateCreation = DateTime.Now;
                    urlStatistics.NumberTransitions = 0;
                    _context.Add(urlStatistics);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Edit), new { Id = urlStatistics.Id });
                }
            }
            ModelState.AddModelError("LongURL", "Enter the valid URL");
            return View(urlStatistics);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var urlStatistics = await _context.URL_Statistics.FindAsync(id);

            if (urlStatistics != null)
            {
                _context.URL_Statistics.Remove(urlStatistics);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(long? id)
        {
            if (id != null || _context.URL_Statistics != null)
            {
                var urlStatistics = await _context.URL_Statistics.FindAsync(id);
                if (urlStatistics != null)
                    return View(urlStatistics);
            }
            return RedirectToAction(nameof(Error));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(long id, URL_Statistics urlStatistics)
        {
            if (id != urlStatistics.Id)
            {
                return RedirectToAction(nameof(Error));
            }

            if (ModelState.IsValid && Uri.IsWellFormedUriString(urlStatistics.LongURL, UriKind.Absolute))
            {
                try
                {
                    _context.Update(urlStatistics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrlStatisticsExists(urlStatistics.Id))
                    {
                        return RedirectToAction(nameof(Error));
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("LongURL", "Enter the valid URL");
            return View(urlStatistics);
        }
        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> RedirectToLongUrl(string shortUrl)
        {
            string longUrl = await _urlShortenerService.ExpandUrlAsync(shortUrl);

            if (longUrl != null)
            {
                return Redirect(longUrl);
            }
            return NotFound();
        }
        private bool UrlStatisticsExists(long id)
        {
            return (_context.URL_Statistics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}