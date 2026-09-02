using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalyBooking.DtoLayer.Dtos
{
    public class FuelPriceDto
    {
        /// <summary>
        /// İl veya İlçe Adı (Örn: "ISTANBUL (AVRUPA)", "KADIKOY")
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// V/Max Kurşunsuz 95 Litre Fiyatı
        /// </summary>
        public decimal GasolinePrice { get; set; }

        /// <summary>
        /// V/Max Diesel Litre Fiyatı
        /// </summary>
        public decimal DieselPrice { get; set; }

        /// <summary>
        /// PO/gaz Otogaz Litre Fiyatı
        /// </summary>
        public decimal LpgPrice { get; set; }

        /// <summary>
        /// Verinin siteden okunduğu tarih ve saat (Veritabanına kaydederken loglamak için faydalıdır)
        /// </summary>
        public DateTime ScrapedAt { get; set; }
    }
}
