using App.Metrics;
using App.Metrics.Meter;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApiMetric.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMetrics _metrics;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMetrics metrics)
    {
        _logger = logger;
        _metrics = metrics;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        
        _metrics.Measure.Counter.Increment(MetricsGetWeather.GetWeatherCount);

        _metrics.Measure.Gauge.SetValue(MetricsGetWeather.Errors, 1);

        _metrics.Measure.Histogram.Update(MetricsGetWeather.SampleHistogram, 1);

        using (_metrics.Measure.Timer.Time(MetricsGetWeather.SampleTimer))
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }

        // using (_metrics.Measure.Apdex.Track(MetricsGetWeather.SampleApdex))
        // {
        //     // Do something
        // }
        //
        // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateTime.Now.AddDays(index),
        //         TemperatureC = Random.Shared.Next(-20, 55),
        //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //     })
        //     .ToArray();
    }
}