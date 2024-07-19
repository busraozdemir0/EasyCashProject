using Microsoft.AspNetCore.Mvc;

namespace EasyCashProject.PresentationLayer.Controllers
{
    public class ExchangeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // Dolar'dan Turk lirasina cevirme
            #region
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange.p.rapidapi.com/exchange?from=USD&to=TRY&q=1.0"),
                Headers =
                {
                    { "X-RapidAPI-Key", "3fbad29645msh80ffd96dbea7821p18ba73jsna1767cd6ef22" },
                    { "X-RapidAPI-Host", "currency-exchange.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                ViewBag.UsdToTry = Decimal.Parse(body.Replace(".", ",")).ToString("0.0000"); ;
            }
            #endregion

            // Euro'dan Turk lirasına cevirme
            #region
            var client2 = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange.p.rapidapi.com/exchange?from=EUR&to=TRY&q=1.0"),
                Headers =
                {
                    { "X-RapidAPI-Key", "630ce9cc86msh271c60cffe62d5ep1b514djsn0fe292593744" },
                    { "X-RapidAPI-Host", "currency-exchange.p.rapidapi.com" },
                },
            };
            using (var response2 = await client2.SendAsync(request2))
            {
                response2.EnsureSuccessStatusCode();
                var body2 = await response2.Content.ReadAsStringAsync();
                ViewBag.EurToTry = Decimal.Parse(body2.Replace(".", ",")).ToString("0.0000"); ;
            }
            #endregion

            // Gbp'den (Sterlin) Turk lirasina cevirme
            #region
            var client3 = new HttpClient();
            var request3 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange.p.rapidapi.com/exchange?from=GBP&to=TRY&q=1.0"),
                Headers =
                {
                    { "X-RapidAPI-Key", "630ce9cc86msh271c60cffe62d5ep1b514djsn0fe292593744" },
                    { "X-RapidAPI-Host", "currency-exchange.p.rapidapi.com" },
                },
            };
            using (var response3 = await client3.SendAsync(request3))
            {
                response3.EnsureSuccessStatusCode();
                var body3 = await response3.Content.ReadAsStringAsync();
                ViewBag.GbpToTry = Decimal.Parse(body3.Replace(".", ",")).ToString("0.0000"); ;
            }
            #endregion

            // Dolar'dan euro'ya cevirme
            #region
            var client4 = new HttpClient();
            var request4 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange.p.rapidapi.com/exchange?from=USD&to=EUR&q=1.0"),
                Headers =
                {
                    { "X-RapidAPI-Key", "630ce9cc86msh271c60cffe62d5ep1b514djsn0fe292593744" },
                    { "X-RapidAPI-Host", "currency-exchange.p.rapidapi.com" },
                },
            };
            using (var response4 = await client4.SendAsync(request4))
            {
                response4.EnsureSuccessStatusCode();
                var body4 = await response4.Content.ReadAsStringAsync();
                ViewBag.UsdToEur = Decimal.Parse(body4.Replace(".", ",")).ToString("0.0000"); ;
            }
            #endregion
            return View();
        }
    }
}
