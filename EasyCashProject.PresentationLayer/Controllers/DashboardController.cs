using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EasyCashProject.PresentationLayer.Controllers
{
    public class DashboardController : Controller
    {
        public async Task<IActionResult> Index()
        {
            // Dolar'dan Turk lirasina cevirme
            #region
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange22.p.rapidapi.com/rates?base=USD"),
                Headers =
                    {
                        { "x-rapidapi-key", "3fbad29645msh80ffd96dbea7821p18ba73jsna1767cd6ef22" },
                        { "x-rapidapi-host", "currency-exchange22.p.rapidapi.com" },
                    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                // JSON yanitini ayristir
                var json = JObject.Parse(body);

                // TRY degerini al
                var usdToTry = json["rates"]?["TRY"]?.ToString();

                ViewBag.UsdToTry = Decimal.Parse(usdToTry.Replace(".", ",")).ToString("0.0000");
            }
            #endregion

            // Euro'dan Turk lirasına cevirme
            #region
            var client2 = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange22.p.rapidapi.com/rates?base=EUR"),
                Headers =
                    {
                        { "x-rapidapi-key", "3fbad29645msh80ffd96dbea7821p18ba73jsna1767cd6ef22" },
                        { "x-rapidapi-host", "currency-exchange22.p.rapidapi.com" },
                    },
            };
            using (var response2 = await client2.SendAsync(request2))
            {
                response2.EnsureSuccessStatusCode();
                var body = await response2.Content.ReadAsStringAsync();

                // JSON yanitini ayristir
                var json = JObject.Parse(body);

                // TRY degerini al
                var eurToTry = json["rates"]?["TRY"]?.ToString();
                ViewBag.EurToTry = Decimal.Parse(eurToTry.Replace(".", ",")).ToString("0.0000"); ;
            }
            #endregion

            // Gbp'den (Sterlin) Turk lirasina cevirme
            #region
            var client3 = new HttpClient();
            var request3 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange22.p.rapidapi.com/rates?base=GBP"),
                Headers =
                    {
                        { "x-rapidapi-key", "3fbad29645msh80ffd96dbea7821p18ba73jsna1767cd6ef22" },
                        { "x-rapidapi-host", "currency-exchange22.p.rapidapi.com" },
                    },
            };
            using (var response3 = await client3.SendAsync(request3))
            {
                response3.EnsureSuccessStatusCode();
                var body = await response3.Content.ReadAsStringAsync();

                // JSON yanitini ayristir
                var json = JObject.Parse(body);

                // TRY degerini al
                var gbpToTry = json["rates"]?["TRY"]?.ToString();
                ViewBag.GbpToTry = Decimal.Parse(gbpToTry.Replace(".", ",")).ToString("0.0000"); ;
            }
            #endregion



            // Dolar'dan euro'ya cevirme
            #region
            var client4 = new HttpClient();
            var request4 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange22.p.rapidapi.com/rates?base=USD"),
                Headers =
                    {
                        { "x-rapidapi-key", "3fbad29645msh80ffd96dbea7821p18ba73jsna1767cd6ef22" },
                        { "x-rapidapi-host", "currency-exchange22.p.rapidapi.com" },
                    },
            };
            using (var response4 = await client4.SendAsync(request4))
            {
                response4.EnsureSuccessStatusCode();
                var body = await response4.Content.ReadAsStringAsync();

                // JSON yanitini ayristir
                var json = JObject.Parse(body);

                // EUR degerini al
                var usdToEur = json["rates"]?["EUR"]?.ToString();
                ViewBag.UsdToEur = Decimal.Parse(usdToEur.Replace(".", ",")).ToString("0.0000"); ;
            }
            #endregion
            return View();
        }
    }
}
