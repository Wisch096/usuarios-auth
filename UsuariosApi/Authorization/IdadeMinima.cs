using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Authorization;

public class IdadeMinima : IAuthorizationRequirement 
{
    public IdadeMinima(int idade)
    {
        idade = idade;
    }
    
    public int Idade { get; set; }
}