# ASP.NETcore: Implementa��o de seguran�a baseada em fun��es (Role Based Security) em uma aplica��o ASP.NET Core Minimal API.  

## Implementa��o de Seguran�a Baseada em Fun��es com ASP.NET Core Identity

O ASP.NET Core Identity fornece uma estrutura robusta para gerenciamento de identidade do usu�rio em aplicativos ASP.NET Core,
incluindo suporte para Seguran�a Baseada em Fun��es (Role Based Security - RBS). 
Ao utilizar a interface IdentityDbContext, � poss�vel integrar facilmente a autentica��o e autoriza��o baseada em fun��es em sua aplica��o.

Para utilizar o Projeto: 
* Registre um Usu�rio no endpoint
* Crie uma regra
* Atribue a regra ao usu�rio
* Autentique o usu�rio
* Acesse o endpoint GetWeatherForecast


###  Configura��o do DbContext:
```
public class SecurityDbContext : IdentityDbContext
{
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
    {
    }
}

```

### Adi��o de Fun��es:

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

### Adi��o de Gerenciamento no EndPoint



```
app.MapGet("/GetWeatherForecast", () =>
{
  ...........
})
.WithName("GetWeatherForecast")
.WithOpenApi()
.RequireAuthorization("ler"); // A Autoriza��o com Pol�ticas
```


> [!NOTE]
> Esse projeto foi desenvolvido apenas para fins de aprendizado