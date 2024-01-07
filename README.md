# ASP.NETcore: Implementação de segurança baseada em funções (Role Based Security) em uma aplicação ASP.NET Core Minimal API.  

## Implementação de Segurança Baseada em Funções com ASP.NET Core Identity

O ASP.NET Core Identity fornece uma estrutura robusta para gerenciamento de identidade do usuário em aplicativos ASP.NET Core,
incluindo suporte para Segurança Baseada em Funções (Role Based Security - RBS). 
Ao utilizar a interface IdentityDbContext, é possível integrar facilmente a autenticação e autorização baseada em funções em sua aplicação.

Para utilizar o Projeto: 
* Registre um Usuário no endpoint
* Crie uma regra
* Atribue a regra ao usuário
* Autentique o usuário
* Acesse o endpoint GetWeatherForecast


###  Configuração do DbContext:
```
public class SecurityDbContext : IdentityDbContext
{
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
    {
    }
}

```

### Adição de Funções:

```
public static void ConfigureServices(this WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<SecurityDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<SecurityDbContext>();

    builder.Services.AddScoped<SecurityServices>();

    builder.Services.AddAuthentication();
    builder.Services.AddAuthorizationBuilder()
        .AddPolicy("read", policy => policy.RequireRole("user"));
}
```

### Adição de Gerenciamento no EndPoint



```
app.MapGet("/GetWeatherForecast", () =>
{
  ...........
})
.WithName("GetWeatherForecast")
.WithOpenApi()
.RequireAuthorization("ler"); // A Autorização com Políticas
```


> [!NOTE]
> Esse projeto foi desenvolvido apenas para fins de aprendizado
