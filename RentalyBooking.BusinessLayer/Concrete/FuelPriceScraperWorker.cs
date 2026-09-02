using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RentalyBooking.BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.BusinessLayer.Concrete
{
    public class FuelPriceScraperWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public FuelPriceScraperWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private readonly TimeSpan _period = TimeSpan.FromHours(24);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var fuelPriceService = scope.ServiceProvider.GetRequiredService<IFuelPriceService>();

                        // 1. Siteden güncel fiyatları DTO olarak çekiyoruz
                        var webPrices = await fuelPriceService.GetCurrentPricesAsync("", "ISTANBUL (ANADOLU)");

                        if (webPrices != null && webPrices.Any())
                        {
                            var currentWebPrice = webPrices.First();

                            // 2. DTO'yu Entity'ye çeviriyoruz (Mapping)
                            var newFuelPrice = new RentalyBooking.EntityLayer.Entities.FuelPrice // Kendi Entity namespace'ine göre düzelt
                            {
                                DistrictName = currentWebPrice.DistrictName,
                                GasolinePrice = currentWebPrice.GasolinePrice,
                                DieselPrice = currentWebPrice.DieselPrice,
                                LpgPrice = currentWebPrice.LpgPrice,
                                UpdatedDate = DateTime.Now
                            };

                            // 3. Veritabanına INSERT atıyoruz!
                            await fuelPriceService.TInsertAsync(newFuelPrice);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Worker Hatası: " + ex.Message);
                }

                // Kodu test etmek için süreyi şimdilik 24 saat yerine 1 dakika yapabilirsin.
                // Testin bitince tekrar TimeSpan.FromHours(24) yaparsın.
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
