using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using RentalyBooking.DataAccessLayer.Abstract;
using RentalyBooking.DataAccessLayer.Concrete;
using RentalyBooking.DtoLayer.Dtos;
using RentalyBooking.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.DataAccessLayer.EntityFramework
{
    public class ScraperFuelPriceServiceDal : IFuelPriceServiceDal
    {
        private readonly HttpClient _httpClient;
        private readonly RentalyContext _context;

        public ScraperFuelPriceServiceDal(HttpClient httpClient, RentalyContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<List<FuelPriceDto>> GetCurrentPricesAsync(string city, string district)
        {
            var prices = new List<FuelPriceDto>();
            string url = "https://www.petrolofisi.com.tr/akaryakit-fiyatlari";

            // Engellenmemek için standart bir tarayıcı User-Agent'ı ekliyoruz
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            try
            {
                var htmlContent = await _httpClient.GetStringAsync(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                var priceRows = doc.DocumentNode.SelectNodes("//tr[contains(@class, 'price-row')]");

                if (priceRows != null)
                {
                    foreach (var row in priceRows)
                    {
                        string rowDistrictName = row.GetAttributeValue("data-disctrict-name", "").Trim();

                        // Eğer parametre olarak ilçe gönderilmişse ve bu satır o ilçe değilse atla
                        // (Kullanıcı "KADIKOY" gönderdiyse sadece o satırı alır)
                        if (!string.IsNullOrEmpty(district) &&
                            !rowDistrictName.Equals(district, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        string benzinText = row.SelectSingleNode("./td[2]//span[contains(@class, 'with-tax')]")?.InnerText.Trim();
                        string motorinText = row.SelectSingleNode("./td[3]//span[contains(@class, 'with-tax')]")?.InnerText.Trim();
                        string lpgText = row.SelectSingleNode("./td[7]//span[contains(@class, 'with-tax')]")?.InnerText.Trim();

                        // String değerleri decimal'e çevirirken InvariantCulture kullanıyoruz (Nokta ayracı için)
                        decimal.TryParse(benzinText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal benzinPrice);
                        decimal.TryParse(motorinText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal motorinPrice);
                        decimal.TryParse(lpgText, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal lpgPrice);

                        prices.Add(new FuelPriceDto
                        {
                            DistrictName = rowDistrictName,
                            GasolinePrice = benzinPrice,
                            DieselPrice = motorinPrice,
                            LpgPrice = lpgPrice,
                            ScrapedAt = DateTime.Now
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // İleride buraya Serilog vb. ile loglama ekleyebilirsin
                Console.WriteLine($"Veri çekilirken hata oluştu: {ex.Message}");
            }

            return prices;
        }

        public async Task<FuelPrice> GetLastPriceAsync()
        {
            // Tarihe göre ters sıralayıp en üsttekini (en son kaydedileni) alıyoruz
            return await _context.FuelPrices
                                .OrderByDescending(x => x.UpdatedDate)
                                .FirstOrDefaultAsync();
        }

        public async Task InsertAsync(FuelPrice fuelPrice)
        {
            
            await _context.FuelPrices.AddAsync(fuelPrice);
            await _context.SaveChangesAsync();
        }
    }
}
