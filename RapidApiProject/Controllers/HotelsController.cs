using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using RapidApiProject.Models;

namespace RapidApiProject.Controllers
{
    public class HotelsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult>  Index()
        {
            //var locationId = GetLocationId("Antalya");
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/hotels/search?checkout_date=2024-09-15&order_by=distance&filter_by_currency=AED&room_number=1&dest_id=-553173&dest_type=city&adults_number=2&checkin_date=2024-09-14&locale=en-gb&units=metric&include_adjacency=true&children_number=2&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1&page_number=0&children_ages=5%2C0"),
                Headers =
    {
        { "X-RapidAPI-Key", "3c3900831cmsh65a7d35387f9eb4p1e90dbjsnf2dc1d3ab5fd" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<HotelSearchViewModel>(body);
                return View(values.result.ToList());
            }


        }

		[HttpPost]
		public async Task<IActionResult> Index(HotelsSearchLocationViewModel hotelsSearchLocationViewModel)
		{
			var locationId =await GetLocationId(hotelsSearchLocationViewModel.city_name);
          
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/search?checkout_date=2024-09-15&order_by=distance&filter_by_currency=AED&room_number=1" +
                $"&dest_id={locationId}&dest_type=city&adults_number=2&checkin_date=2024-09-14&locale=en-gb&units=metric&include_adjacency=true&children_number=2&categories_filter_ids=class%3A%3A2%2Cclass%3A%3A4%2Cfree_cancellation%3A%3A1&page_number=0&children_ages=5%2C0"),
				Headers =
	{
		{ "X-RapidAPI-Key", "3c3900831cmsh65a7d35387f9eb4p1e90dbjsnf2dc1d3ab5fd" },
		{ "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
	},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<HotelSearchViewModel>(body);
				return View(values.result.ToList());
			}


		}

		private async Task<string> GetLocationId(string locationName)
        {
          
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/hotels/locations?name={locationName}&locale=tr"),
                    Headers =
    {
        { "X-RapidAPI-Key", "3c3900831cmsh65a7d35387f9eb4p1e90dbjsnf2dc1d3ab5fd" },
        { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<HotelsSearchLocationViewModel>>(body);
                    return values[0]?.dest_id ?? throw new Exception("Destinasyon ID'si bulunamadı");
			}


            }
        
        }
    
    
       
}
