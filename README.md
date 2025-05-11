# ProductManagement

.NET 6 ile gelistirilmiş bu mikroservis projesi, Domain Driven Design (DDD) prensipleri doğrultusunda inşa edilmiştir. Temel amacı ürün yönetimi yapmak, stok seviyelerini izlemek ve düşük stok durumlarını otomatik şekilde kaydetmektir. Projede CQRS, MediatR, Entity Framework Core, Hangfire, xUnit ve Docker gibi teknolojiler kullanılmaktadır.

---

## Proje Amacı

Bu proje, aşağıdaki ihtiyaçlara çözüm sunmak amacıyla geliştirilmiştir:

- Ürün CRUD işlemlerini gerçekleştirmek (Create, Read, Update, Delete)
- Her sabah saat 08:00’de çalışacak şekilde bir Hangfire job’u tanımlamak
- Stok seviyesi 10’un altındaki ürünleri loglamak
- Kritik seviyedeki stoklar için uyarı oluşturmak
- SQLite ile hafif ve taşınabilir veritabanı yönetimi sağlamak
- Docker ortamında kolay kurulum ve kullanım sunmak

---

## Kullanılan Teknolojiler

- ASP.NET Core 6
- Entity Framework Core (SQLite)
- MediatR (CQRS mimarisi için)
- Hangfire (Zamanlanmış görevler)
- xUnit (Birim testler)
- Docker & Docker Compose
- Swagger (API dökümantasyonu)

---

## Klasör Yapısı

```
ProductManagement.sln
├── ProductManagement.API               --> API girişi
├── ProductManagement.Application      --> CQRS işlemleri (Command/Query)
├── ProductManagement.Domain           --> Entity ve domain kuralları
├── ProductManagement.Infrastructure   --> EF Core context, repository
├── ProductManagement.Tests            --> xUnit testleri
```

---

## Başlatma Adımları

### Gerekli Araçlar

- .NET 6 SDK
- Docker Desktop

### Docker ile Başlatma

```bash
docker-compose down -v
docker-compose build --no-cache
docker-compose up
```

### Ulaşım URL’leri

- Swagger UI: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- Hangfire Dashboard: [http://localhost:5000/hangfire](http://localhost:5000/hangfire)

---

## Eklenen Özellikler

### 1. Low Stock Job

- Her gün saat 08:00’de tetiklenen `CheckLowStockJob` sınıfı
- Stok miktarı 10'dan az olan ürünler `LowStockLogs` tablosuna loglanır

### 2. LowStockLogs API

- `/api/logs` endpoint’i ile loglanan düşük stok kayıtları listelenebilir

### 3. Ürün Listelemede Sayfalama

- `/api/products?page=1&pageSize=10` ile sayfa bazlı ürün listelenebilir

---

## Testler

Aşağıdaki senaryolar test edilmiştir:

| Test                        | Açıklama                              |
| --------------------------- | ------------------------------------- |
| CreateProductCommandHandler | Ürün başarıyla ekleniyor mu?          |
| GetProductByIdQueryHandler  | ID’ye göre doğru ürün dönüyor mu?     |
| UpdateProductCommandHandler | Güncelleme işlemi doğru çalışıyor mu? |
| DeleteProductCommandHandler | Silme işlemi başarıyla yapılıyor mu?  |
| GetAllProductsQueryHandler  | Sayfalama ve ürün listesi doğru mu?   |

---

## Test Kapsama (Coverage)

Projedeki birim testlerin sadece varlığı değil, kodun ne kadarını test ettiğiniz de önemlidir. Bu amaçla, `coverlet` ve `ReportGenerator` araçları ile test coverage raporu üretimi eklenmiştir.

### Kurulum

```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
```

### Coverage Hesaplama Komutu

```bash
cd ProductManagement.Tests

dotnet test /p:CollectCoverage=true \
  /p:CoverletOutput=./TestResults/ \
  /p:CoverletOutputFormat=lcov

reportgenerator \
  -reports:./TestResults/coverage.info \
  -targetdir:./TestResults/CoverageReport \
  -reporttypes:Html
```

### Raporu Görüntüleme

```
### HTML Raporunu Görüntüleme

Aşağıdaki komut ile oluşturulan HTML dosyasını açarak coverage raporunu görsel olarak inceleyebilirsiniz:

```bash
start ProductManagement.Tests/TestResults/CoverageReport/index.html
```

> Not: Eğer Windows dışında bir sistem kullanıyorsanız, ilgili dosyayı tarayıcınızda manuel açabilirsiniz.

ProductManagement.Tests/TestResults/CoverageReport/index.html
```

---

## Test Kapsama (Coverage)

Kodun ne kadarının test edildiğini görmek için aşağıdaki adımları takip edebilirsiniz:

### 1. Paket Kurulumu

```bash
dotnet add ProductManagement.Tests package coverlet.collector
dotnet tool install --global dotnet-reportgenerator-globaltool
```

### 2. Coverage Raporu Oluşturma

```bash
dotnet test ProductManagement.Tests --collect:"XPlat Code Coverage"
```

Komuttan sonra `TestResults` klasöründe `coverage.cobertura.xml` oluşur.

### 3. HTML Raporu Üret ve Aç

```bash
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
start coveragereport/index.html
```

> Windows dışı sistemlerde `open` veya `xdg-open` komutları kullanılabilir.

## Veritabanı Bilgisi (SQLite)

- Veritabanı dosyası: `product.db`
- Docker volume ile `./data` klasörüne bağlanır
- EF Core migration’lar otomatik uygulanır (Database.Migrate)

---

## Hazırlayan

**Tuba Balkan**

Her türlü geri bildiriminiz ve katkılarınız için teşekkürler!