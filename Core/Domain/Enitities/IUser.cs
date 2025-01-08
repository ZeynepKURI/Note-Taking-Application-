using System;
namespace Domain.Interfaces
{
	public interface IUser
	{

		int Id { get; set; }

		string Username { get; set; }

        string Email{ get; set; }

        string Password { get; set; }

      
    }
   

}

//interface'ler (IService ve IRepository) genellikle domain veya core katmanında yer alır. Bu sayede bağımlılık tersine çevrilir (Dependency Inversion Principle), ve uygulamanın diğer katmanları (örneğin, Infrastructure veya Application) bu interface'leri implement ederek iş mantığını sağlar.

