using System;
namespace Application.DTOs
{
	public record LoginResponse (bool Flag , string Message , string Token= null);

}

//public record RegisterResponse: C# 9.0 ve sonrasında gelen record türü, özellikle veri taşımak için kullanılan ve değiştirilemez (immutable) olan sınıflardır.
//Bu, daha kısa ve temiz bir veri yapısı oluşturmanızı sağlar.

//bool Flag: İşlem başarılı mı, başarısız mı olduğunu belirtmek için kullanılır. Bu, genellikle başarılı bir işlemde true,
//başarısız bir işlemde false olarak dönebilir.

//string Message = null!: İstemciye bir mesaj döndürmek için kullanılır. Bu parametre null! şeklinde işaretlenmiş,
//yani bu değeri null olarak vermek zorunda değilsiniz ve nullable (boş olabilen) bir string olarak işaret edilmiştir.
//Bu, null değerinin atanmaması gerektiği ve mesajın her zaman geçerli bir değere sahip olması gerektiği anlamına gelir.

//string Token = null!: Bu alan, kullanıcı kaydı tamamlandığında bir Token döndürebilir.
//null! işaretlemesi, başlangıçta null değeri atanabileceğini, ancak daha sonra mutlaka bir değere sahip olması gerektiğini ifade eder.