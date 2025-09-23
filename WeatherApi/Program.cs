var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowReact");

app.MapGet("/weatherforecast/{city}", async (string city, HttpClient httpClient) =>
{
    var apiKey = "<YOUR_API_KEY_HERE>"; //replace with your OpenWeatherMap API key
    var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

    try
    {
        var response = await httpClient.GetFromJsonAsync<WeatherApiResponse>(url);
        if (response == null)
            return Results.BadRequest($"No weather data found for {city}");

        //clean view of the response data
        var cleanWeather = new CleanWeatherResponse
        {
            City = response.name,
            Country = response.sys.country,
            TemperatureCelsius = Math.Round(response.main.temp, 1),
            TemperatureFahrenheit = Math.Round(response.main.temp * 9 / 5 + 32, 1),
            FeelsLikeCelsius = Math.Round(response.main.feels_like, 1),
            FeelsLikeFahrenheit = Math.Round(response.main.feels_like * 9 / 5 + 32, 1),
            Description = response.weather[0].description,
            Humidity = response.main.humidity,
            WindSpeed = response.wind.speed,
            Timestamp = DateTime.Now
        };
        
        return Results.Ok(cleanWeather);
    }
    catch (HttpRequestException ex)
    {
        return Results.BadRequest($"Error fetching weather data for {city}: {ex.Message}");
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"Could not get the weather for {city}: {ex.Message}");
    }
})
.WithName("GetWeatherForecast");

app.Run();

public class CleanWeatherResponse
{
    public required string City { get; set; }
    public required string Country { get; set; }
    public double TemperatureCelsius { get; set; }
    public double TemperatureFahrenheit { get; set; }
    public double FeelsLikeCelsius { get; set; }
    public double FeelsLikeFahrenheit { get; set; }
    public required string Description { get; set; }
    public int Humidity { get; set; }
    public double WindSpeed { get; set; }
    public DateTime Timestamp { get; set; }
}
public class WeatherApiResponse
{
    public required string name { get; set; }
    public required Main main { get; set; }
    public required Weather[] weather { get; set; }
    public required Wind wind { get; set; }
    public required Sys sys { get; set; }
}

public class Main
{
    public double temp { get; set; }
    public double feels_like { get; set; }
    public int humidity { get; set; }
}

public class Weather
{
    public required string description { get; set; }
}

public class Wind
{
    public double speed { get; set; }
}

public class Sys
{
    public required string country { get; set; }
}