using rwwo.min_web_api.rbs.Models;
using rwwo.min_web_api.rbs.Services;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi()
.RequireAuthorization("read"); 


app.MapPost("/registeruser", async (RegisterUser user, SecurityServices serv) => {
    var response = await serv.RegisterUserAsync(user);
    return Results.Ok(response);
});

app.MapPost("/loginuser", async (LoginUser user, SecurityServices serv) => {
    var response = await serv.AuthUser(user);
    return Results.Ok(response);
});

app.MapPost("/createrole", async (RoleData role, SecurityServices serv) => {
    var response = await serv.CreateRoleAsync(role);
    return Results.Ok(response);
});


app.MapPost("/assigrole", async (UserRole userrole, SecurityServices serv) => {
    var response = await serv.AddRoleToUserAsync(userrole);
    return Results.Ok(response);
});


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
