# 🚗 RentalyBooking - Kapsamlı Araç Kiralama Platformu

RentalyBooking, uçtan uca araç kiralama süreçlerini dijitalleştirmek amacıyla ASP.NET Core MVC mimarisi üzerinde inşa edilmiş, yüksek performanslı ve tam kapsamlı bir B2C platformudur. Sistem, müşterilere hızlı ve güvenli bir rezervasyon deneyimi sunarken, yöneticilere de araç filosu, şube ağları ve operasyonel süreçler üzerinde tek merkezden tam kontrol yetkisi sağlar.

Kullanıcı arayüzünde (UI), modern web standartlarına uygun ve mobil uyumlu Rentaly teması entegre edilerek kullanıcı deneyimi (UX) en üst düzeye çıkarılmıştır. Uygulamanın kalbi olan Admin Paneli ise hazır bir şablon kullanılmadan, tamamen projenin iş gereksinimlerine (business requirements) ve veri yönetimi ihtiyaçlarına göre sıfırdan (custom) tasarlanmış ve geliştirilmiştir.

---

## 📸 Ekran Görüntüleri
<div align="center">
  <img src="https://github.com/user-attachments/assets/d761eca9-1351-46bf-9f2f-a5f08b4d6113" width="48%">

  <br><br>
    <table>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/dd28ee5f-cf06-4fc6-a707-ad49df2cd628" width="100%"></td>
    <td rowspan="2"><img src="https://github.com/user-attachments/assets/625d4510-771f-402c-88ef-0de926a07bd1" width="100%"></td>
  </tr>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/fa8f824f-99ae-42b6-996e-12dbf472f202" width="100%"></td>
  </tr>
</table>

  <br><br>
  <img src="https://github.com/user-attachments/assets/9a47e84c-0cbf-451d-8464-bf74f2159d49" width="48%">
  <img src="https://github.com/user-attachments/assets/529322e7-09bd-483f-a4f9-0af75debea30" width="48%">
  
  <br><br>
  <img src="https://github.com/user-attachments/assets/849a834a-1c8b-4dbb-a8df-67b086af7742" width="48%">
  <img src="https://github.com/user-attachments/assets/5fe32e61-2270-48c6-b739-6fb25843edc8" width="48%">
  
  <br><br>
  <img src="https://github.com/user-attachments/assets/c62a55f0-72b8-410a-801d-55bd80c5d6c1" width="48%">
  <img src="https://github.com/user-attachments/assets/143a07c2-5376-4307-aaec-299317c8036c" width="48%">
  
  <br><br>
  <img src="https://github.com/user-attachments/assets/c20b0745-06a4-4dc0-b0f9-269346302959" width="48%">
  <img src="https://github.com/user-attachments/assets/5269a97e-3939-4a93-8b1a-fbaa64687dae" width="48%">
  
  <br><br>
  <img src="https://github.com/user-attachments/assets/00533fb4-c58c-4ba1-96c2-012a6f4a2ab0" width="48%">
</div>

---

## 🌟 Öne Çıkan Özellikler

### 🧑‍💻 Kullanıcı Arayüzü (UI)
* **Dinamik Ana Sayfa:** İşleyiş (Process), Hakkımızda, Top 10 Araç, Ödüller, Müşteri Yorumları (Testimonials), Sık Sorulan Sorular, Modeller ve Markalar bölümlerinin tamamı ViewComponent mimarisi ile dinamik olarak veritabanından beslenmektedir.
* **Akıllı Rezervasyon ve Filtreleme:** Kullanıcılar tarih ve lokasyon bazlı arama yapabilir. Seçilen tarihler arasında araç sistem tarafından kilitlenir ve çifte rezervasyon (double-booking) önlenir.
* **Gelişmiş Booking Akışı:** Araç listesinden bağımsız, UX odaklı özel bir rezervasyon sayfası mevcuttur. Rezervasyonlar onay gerektirecek şekilde doğrudan admin paneline düşer.
* **Mail Bildirim Sistemi:** Admin rezervasyonu onayladığında müşteriye özel HTML şablonlu, e-imzalı ve bir sonraki kiralama için görsel formatta **indirim kodu** içeren bir onay maili iletilir.
* **Gelişmiş Araç Listeleme:** Marka, model, fiyat ve lokasyon bazlı anlık filtreleme yapılabilen genişletilebilir araç listesi.

### 🛡️ Admin Paneli (Custom Tasarım)
* **Tam Kapsamlı Yönetim:** `Areas` mimarisi kullanılarak izole edilen admin panelinde; Şube, Marka, Araç, Model, Kategori, Müşteri ve Rezervasyon (Booking) entity'leri için tüm CRUD (Ekleme, Silme, Güncelleme, Listeleme) işlemleri mevcuttur.
* **Rezervasyon Yönetimi:** Bekleyen rezervasyonları inceleme ve tek tıkla onaylama mekanizması.
* **Akaryakıt Fiyatları Entegrasyonu (Web Scraping):** Admin Dashboard ekranında, **HtmlAgilityPack** kullanılarak Petrol Ofisi web sitesi üzerinden anlık ve lokasyon bazlı (İl/İlçe) benzin, motorin ve LPG fiyatları çekilmektedir.

> **⚠️ Yasal Uyarı / Bilgilendirme:** Bu proje kapsamında yapılan web scraping işlemi (akaryakıt fiyatlarının çekilmesi) tamamen **eğitim ve portfolyo geliştirme amacıyla** yapılmış olup, hiçbir ticari amaç gütmemektedir. Veriler ilgili kurumun halka açık sayfasından anlık olarak okunmaktadır.

## 🏗️ Proje Mimarisi ve Klasör Yapısı

Proje, temiz kod prensiplerine uygun olarak Controllers, Models ve ViewComponents olmak üzere ayrıştırılmıştır.
* **UI Tarafı:** `Booking`, `Car`, `Home` ve `Error` (Özel 404 Sayfası) Controller'ları ile yönetilir. Arayüz parçalanarak `Analytic`, `CarListTopTen`, `FAQ`, `Testimonial` gibi ViewComponent'ler ile modüler hale getirilmiştir.
* **Admin Tarafı:** Uygulamanın yönetim birimi `Areas/Admin` klasörü altında izole edilmiştir. `Dashboard`, `Branch`, `Brand`, `Car`, `Booking` vb. tüm Controller ve View dosyaları burada barınmaktadır.

## 🛠️ Kullanılan Teknolojiler
* **Backend:** C#, ASP.NET Core MVC
* **Veritabanı ve ORM:** SQL Server, Entity Framework Core (Code First)
* **Web Scraping:** HtmlAgilityPack (Anlık akaryakıt verisi için)
* **Frontend:** HTML5, CSS3, Bootstrap, JavaScript, jQuery
* **Tasarım Kalıpları:** Dependency Injection, Repository Pattern (DataAccessLayer), ViewComponent Yapısı

## 🚀 Kurulum ve Çalıştırma
1. Projeyi bilgisayarınıza klonlayın:
   ```bash
   git clone https://github.com/KullaniciAdiniz/RentalyBooking.git
   ```
2. SQL Server'ı başlatın ve appsettings.json dosyasındaki DefaultConnection connection string'ini kendi yerel veritabanınıza göre güncelleyin.
3. Package Manager Console (PMC) üzerinden veritabanını oluşturun:
   ```bash
   Update-Database
   ```
4. Projeyi derleyin ve çalıştırın.