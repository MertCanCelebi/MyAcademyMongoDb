using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace JadooTravel.Controllers
{
    [AllowAnonymous]
    public class AIController : Controller
    {
        private readonly string apiKey = "APİKEY";

        [HttpPost]
        public async Task<IActionResult> GetPlaces(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                TempData["Error"] = "Lütfen bir şehir adı giriniz!";
                return RedirectToAction("Index", "Default");
            }

            string prompt = $"Sen bir seyahat rehberisin. '{cityName}' şehrinde gezilecek en popüler 10 yeri listele. " +
                            $"Her yer için 'Yer Adı - Kısa açıklama' formatında yaz.";

            try
            {
                using var client = new HttpClient();

                client.BaseAddress = new Uri("https://openrouter.ai/api/v1/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.DefaultRequestHeaders.Add("HTTP-Referer", "https://seninsiten.com");
                client.DefaultRequestHeaders.Add("X-Title", "JadooTravel AI");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "Sen bir seyahat rehberisin. Cevaplarını net, kısa ve bilgilendirici tut." },
                        new { role = "user", content = prompt }
                    },
                    temperature = 0.7,
                    max_tokens = 800
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync("chat/completions", jsonContent);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = $"API hatası: {responseText}";
                    return RedirectToAction("Index", "Default");
                }

                using var doc = JsonDocument.Parse(responseText);
                var messageContent = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString() ?? "";

                var results = messageContent
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                    .Select(line => line.TrimStart(' ', '-', '.', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'))
                    .Select(line => line.Trim())
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Take(10)
                    .ToList();

                TempData["City"] = cityName;
                TempData["Results"] = JsonSerializer.Serialize(results);

                return RedirectToAction("Index", "Default");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Bir hata oluştu: {ex.Message}";
                return RedirectToAction("Index", "Default");
            }
        }
    }
}
