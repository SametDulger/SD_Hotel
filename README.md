# SD Hotel Yönetim Sistemi

> ⚠️ **Geliştirme Aşamasında** ⚠️
> 
> Bu proje aktif geliştirme aşamasındadır. Bazı özellikler henüz tamamlanmamış olabilir ve beklenmedik hatalar ile karşılaşabilirsiniz. Üretim ortamında kullanmadan önce kapsamlı test yapılması önerilir.
> 
> **Bilinen Sınırlamalar:**
> - API authentication henüz implement edilmemiştir
> - Unit testler geliştirme aşamasındadır
> - Bazı UI bileşenleri optimize edilmemiştir
> - Performans optimizasyonları devam etmektedir

SD Hotel Yönetim Sistemi, otel işletmelerinin günlük operasyonlarını yönetmek için geliştirilmiş kapsamlı bir web uygulamasıdır.

## 🏗️ Proje Yapısı

Bu proje Clean Architecture prensiplerine uygun olarak geliştirilmiştir ve aşağıdaki katmanlardan oluşur:

- **SD_Hotel.Core**: Domain entities, interfaces ve business logic
- **SD_Hotel.Infrastructure**: Data access, external services ve infrastructure concerns
- **SD_Hotel.Application**: Business services, DTOs ve application logic
- **SD_Hotel.API**: REST API endpoints
- **SD_Hotel.Web**: MVC web uygulaması

## 🚀 Özellikler

### 📋 Ana Modüller
- **Oda Yönetimi**: Oda durumları, tipleri, fiyatları ve görsel yönetimi
- **Müşteri Yönetimi**: Müşteri kayıtları, sadakat programı ve kimlik bilgileri
- **Rezervasyon Sistemi**: Rezervasyon oluşturma, düzenleme, iptal ve erken rezervasyon indirimi
- **Personel Yönetimi**: Personel kayıtları, vardiya ve mesai takibi
- **Ek Gider Yönetimi**: Ek hizmetler ve gider takibi
- **Dashboard**: Gerçek zamanlı istatistikler ve aktivite takibi

### 🎯 Teknik Özellikler
- .NET 9 ile geliştirilmiş
- Entity Framework Core ile SQL Server veritabanı yönetimi
- RESTful API mimarisi
- Responsive web tasarımı (Bootstrap 5)
- SOLID prensiplerine uygun geliştirme
- Repository pattern implementasyonu
- DTO pattern ile veri transferi

## 🛠️ Kurulum

### Gereksinimler
- .NET 9 SDK
- SQL Server (SQL Express veya üzeri)
- Visual Studio 2022 veya VS Code

### Adımlar

1. **Repository'yi klonlayın**
   ```bash
   git clone https://github.com/SametDulger/SD_Hotel.git
   cd SD_Hotel
   ```

2. **Veritabanı bağlantısını yapılandırın**
   `SD_Hotel.API/appsettings.json` dosyasında connection string'i düzenleyin:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=SD_Hotel;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
     }
   }
   ```

3. **Migration oluşturun**
   ```bash
   cd SD_Hotel.Infrastructure
   dotnet ef migrations add InitialCreate
   ```

4. **Veritabanını oluşturun**
   ```bash
   cd SD_Hotel.Infrastructure
   dotnet ef database update
   ```

5. **API'yi çalıştırın**
   ```bash
   cd SD_Hotel.API
   dotnet run
   ```
   API varsayılan olarak `http://localhost:5158` adresinde çalışacaktır.

6. **Web uygulamasını çalıştırın**
   ```bash
   cd SD_Hotel.Web
   dotnet run
   ```
   Web uygulaması varsayılan olarak `http://localhost:5253` adresinde çalışacaktır.

## 📁 Proje Yapısı

```
SD_Hotel/
├── SD_Hotel.Core/                 # Domain katmanı
│   ├── Entities/                  # Domain entities 
│   │   ├── BaseEntity.cs         # Temel entity sınıfı
│   │   ├── Room.cs               # Oda entity
│   │   ├── RoomImage.cs          # Oda görsel entity
│   │   ├── Customer.cs           # Müşteri entity
│   │   ├── Employee.cs           # Personel entity
│   │   ├── Reservation.cs        # Rezervasyon entity
│   │   ├── Shift.cs              # Vardiya entity
│   │   ├── Overtime.cs           # Mesai entity
│   │   └── ExtraExpense.cs       # Ek gider entity
│   └── Repositories/              # Repository interfaces
├── SD_Hotel.Infrastructure/       # Infrastructure katmanı
│   ├── Data/                      # DbContext ve konfigürasyon
│   │   └── SD_HotelDbContext.cs  # EF Core DbContext
│   ├── Migrations/                # EF Core migrations
│   └── Repositories/              # Repository implementations
├── SD_Hotel.Application/          # Application katmanı
│   ├── DTOs/                      # Data Transfer Objects 
│   └── Services/                  # Business services 
├── SD_Hotel.API/                  # REST API
│   └── Controllers/               # API controllers 
└── SD_Hotel.Web/                  # MVC Web uygulaması
    ├── Controllers/               # MVC controllers 
    ├── Views/                     # Razor views 
    ├── Models/                    # ViewModels
    └── wwwroot/                   # Static files
```

## 🔧 Konfigürasyon

### Veritabanı Bağlantısı
`SD_Hotel.API/appsettings.json` dosyasında connection string'i düzenleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=SD_Hotel;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

### API Bağlantısı
Web uygulamasının API'ye bağlanabilmesi için `SD_Hotel.Web/Program.cs` dosyasında API URL'ini kontrol edin:

```csharp
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("http://localhost:5158/");
});
```

## 📊 Veritabanı Şeması

Ana tablolar:
- **Rooms**: Oda bilgileri (numara, tip, fiyat, durum)
- **RoomImages**: Oda görselleri
- **Customers**: Müşteri kayıtları (kimlik, iletişim bilgileri)
- **Employees**: Personel bilgileri (kimlik, maaş, saatlik ücret)
- **Reservations**: Rezervasyon kayıtları (tarih, fiyat, indirim)
- **Shifts**: Vardiya bilgileri (çalışma saatleri)
- **Overtimes**: Mesai kayıtları (ekstra çalışma)
- **ExtraExpenses**: Ek giderler (hizmet ücretleri)

## 🎨 Kullanıcı Arayüzü

- **Ana Sayfa**: Dashboard, gerçek zamanlı istatistikler ve son aktiviteler
- **Odalar**: Oda listesi, durumları, filtreleme ve görsel yönetimi
- **Müşteriler**: Müşteri yönetimi, sadakat programı ve arama
- **Rezervasyonlar**: Rezervasyon oluşturma, düzenleme ve yönetimi
- **Personel**: Personel kayıtları, vardiya ve mesai yönetimi
- **Ek Giderler**: Ek hizmetler ve gider takibi


## 📝 API Dokümantasyonu

API endpoints:

### Odalar
- `GET /api/rooms` - Tüm odaları listele
- `GET /api/rooms/{id}` - Oda detaylarını getir
- `POST /api/rooms` - Yeni oda oluştur
- `PUT /api/rooms/{id}` - Oda güncelle
- `DELETE /api/rooms/{id}` - Oda sil

### Müşteriler
- `GET /api/customers` - Tüm müşterileri listele
- `GET /api/customers/{id}` - Müşteri detaylarını getir
- `POST /api/customers` - Yeni müşteri oluştur
- `PUT /api/customers/{id}` - Müşteri güncelle
- `DELETE /api/customers/{id}` - Müşteri sil

### Personel
- `GET /api/employees` - Tüm personeli listele
- `GET /api/employees/active` - Aktif personeli listele
- `GET /api/employees/{id}` - Personel detaylarını getir
- `POST /api/employees` - Yeni personel oluştur
- `PUT /api/employees/{id}` - Personel güncelle
- `DELETE /api/employees/{id}` - Personel sil

### Rezervasyonlar
- `GET /api/reservations` - Tüm rezervasyonları listele
- `GET /api/reservations/{id}` - Rezervasyon detaylarını getir
- `POST /api/reservations` - Yeni rezervasyon oluştur
- `PUT /api/reservations/{id}` - Rezervasyon güncelle
- `DELETE /api/reservations/{id}` - Rezervasyon sil

### Vardiyalar
- `GET /api/shifts` - Tüm vardiyaları listele
- `GET /api/shifts/{id}` - Vardiya detaylarını getir
- `POST /api/shifts` - Yeni vardiya oluştur
- `PUT /api/shifts/{id}` - Vardiya güncelle
- `DELETE /api/shifts/{id}` - Vardiya sil

### Mesailer
- `GET /api/overtimes` - Tüm mesaileri listele
- `GET /api/overtimes/{id}` - Mesai detaylarını getir
- `POST /api/overtimes` - Yeni mesai oluştur
- `PUT /api/overtimes/{id}` - Mesai güncelle
- `DELETE /api/overtimes/{id}` - Mesai sil

### Ek Giderler
- `GET /api/extraexpenses` - Tüm ek giderleri listele
- `GET /api/extraexpenses/{id}` - Ek gider detaylarını getir
- `POST /api/extraexpenses` - Yeni ek gider oluştur
- `PUT /api/extraexpenses/{id}` - Ek gider güncelle
- `DELETE /api/extraexpenses/{id}` - Ek gider sil


## 📄 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

---

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın! 