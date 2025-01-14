# Note-Taking Application

Bu proje, **Onion Architecture** kullanarak .NET Core ile geliştirilmiş bir Not Alma uygulamasıdır. Uygulama, temel **CRUD** işlemleri sağlar ve **asenkron yöntemler**, **JWT kimlik doğrulama**, **AutoMapper**, **Dependency Injection** ve standart **yanıt yapıları** kullanır.

## Özellikler

- **Asenkron Yöntemler**: Veritabanı işlemleri için asenkron metodlar kullanılarak performans arttırılmıştır.
- **JWT Kimlik Doğrulama**: Kullanıcıların güvenli bir şekilde giriş yapabilmesi için JWT tabanlı kimlik doğrulama kullanılmıştır.
- **AutoMapper**: Veritabanı modelleri ile DTO'lar arasında dönüşüm yapmak için AutoMapper kullanılmıştır.
- **Dependency Injection (DI)**: Katmanlar arası bağımlılıkları çözmek için DI kullanılmıştır.
- **CRUD İşlemleri**: Not ekleme, listeleme, güncelleme ve silme işlemleri.
- **Standart Yanıtlar**: API'den dönen yanıtlar, belirli bir formatta düzenlenmiştir.

## Proje Yapısı

- **Core**: Entity'ler, Arayüzler, DTO'lar ve Servisler.
- **Application**: İş mantığı, AutoMapper yapılandırması.
- **Infrastructure**: Veritabanı bağlantısı, Repository'ler.
- **API**: Controller'lar, Modeller.

## Başlangıç

Projenizi başlatmadan önce gerekli NuGet paketlerini yükleyin:

1. JWT Kimlik Doğrulama:
   ```bash
   dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
2. AutoMapper:
   ```bash
      dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
3 .EF Core:

    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
4. Dependency Injection (DI) için:
DI, .NET Core'un yerleşik özelliklerinden biridir. Katmanlar arasında bağımlılıkları çözmek için Startup.cs veya Program.cs dosyasında servisleri kaydedebilirsiniz.
Örneğin, Application katmanındaki bir servisi DI ile eklemek için aşağıdaki gibi yapılandırabilirsiniz:
   ```bash
     public void ConfigureServices(IServiceCollection services)
     {
    // AutoMapper yapılandırması
    services.AddAutoMapper(typeof(Startup));

    // Veritabanı bağlantısı
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    // Servislerin DI ile eklenmesi
    services.AddScoped<INoteService, NoteService>();
}

## Kullanım
Projenizi çalıştırmak için şu adımları izleyin:
Veritabanı Bağlantısını Yapılandırın: appsettings.json dosyasındaki bağlantı dizesini kendi veritabanınıza göre ayarlayın.


- **POST **: /api/notes: Yeni not ekler.
- **GET **: /api/notes: Tüm notları listeler.
- **PUT**:  /api/notes/{id}: Mevcut bir notu günceller.
- **DELETE **: /api/notes/{id}: Bir notu siler.
Katkı Sağlamak
Bu projeye katkıda bulunmak isterseniz, lütfen bir pull request gönderin veya bir issue açın.

Lisans
Bu proje MIT lisansı ile lisanslanmıştır.





