# 🔨 Bidly - Online Mezat Uygulaması

**Bidly**, C# Windows Forms ve PostgreSQL kullanılarak geliştirilmiş, gerçek zamanlı teklif verme ve otomatik ihale sonuçlandırma özelliklerine sahip bir müzayede platformudur.

## 🚀 Öne Çıkan Özellikler

* **Dinamik Vitrin:** Aktif ihalelerin anlık olarak listelenmesi ve süre takibi.
* **Akıllı Teklif Sistemi:** Mevcut fiyattan düşük teklif verilmesini engelleyen kontrol mekanizması.
* **Otomatik İhale Sonlandırma:** Süresi dolan ürünlerin satış işlemlerinin veritabanı seviyesinde (Stored Procedure) otomatik tamamlanması.
* **Güvenli Şifre Yönetimi:** Kullanıcı şifrelerinin veritabanı üzerindeki bir saklı yordam (`sp_sifre_degistir`) aracılığıyla güncellenmesi.
* **Fiyat Loglama:** Her teklif sonrası eski ve yeni fiyatın tarih damgasıyla `fiyat_degisim_logla` mekanizması üzerinden yedeklenmesi.

## 🛠 Kullanılan Teknolojiler

* **Dil:** C# (.NET Framework)
* **Veritabanı:** PostgreSQL
* **Kütüphane:** Npgsql (PostgreSQL bağlantısı için)

---

## ⚙️ Kurulum ve Çalıştırma Rehberi 


### 1. Veritabanı Kurulumu

1. **pgAdmin 4**'ü açın ve `OnlineMezatDB` isminde yeni bir veritabanı oluşturun.
2. Proje ana dizininde bulunan **`veritabaniKurulum.sql`** dosyasını herhangi bir metin editörüyle açın ve içeriğini kopyalayın.
3. pgAdmin üzerinde `OnlineMezatDB`'ye sağ tıklayıp **Query Tool**'u açın, kodu yapıştırın ve **F5** ile çalıştırın.
* *Bu işlem; tüm tabloları, `ihale_sonlandir` ve `sp_sifre_degistir` prosedürlerini ve fiyat loglama tetikleyicilerini otomatik oluşturacaktır.*



### 2. Projenin Hazırlanması

1. **`OnlineMezatApp.sln`** dosyasını Visual Studio ile açın.
2. `OnlineMezatApp` projesi içindeki **`Veritabani.cs`** dosyasını bulun.
3. Bağlantı dizesindeki (ConnectionString) `User Id` ve `Password` kısımlarını kendi yerel PostgreSQL bilgilerinizle güncelleyin:
```csharp
"Host=localhost;Username=postgres;Password=YENI_SIFRENIZ;Database=OnlineMezatDB"

```



### 3. Çalıştırma

1. **Solution Explorer** üzerinden projeye sağ tıklayıp `Rebuild` yapın.
2. **F5** tuşuna basarak uygulamayı başlatabilirsiniz.

---

## 🧠 Veritabanı Mimarisi 

Proje, iş mantığının bir kısmını veritabanı seviyesinde yöneterek performans ve veri bütünlüğü sağlar:

* **Stored Procedure (`ihale_sonlandir`):** Süresi dolan ürünleri tespit eder, kazanan kullanıcının bakiyesini günceller ve satışı onaylayarak ürün durumunu değiştirir.
* **Stored Procedure (`sp_sifre_degistir`):** Kullanıcı şifre güncellemelerini parametre olarak aldığı ID ve yeni şifre bilgisiyle güvenli bir şekilde gerçekleştirir.
* **Trigger (`fiyat_degisim_logla`):** `urunler` tablosunda fiyat güncellendiği anda çalışır ve `fiyat_loglari` tablosuna eski/yeni fiyat bilgisini yazar.

---

**Geliştiriciler:** Eren Büyükaşık - Ertunç Yontuç
