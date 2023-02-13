using App.Metrics;
using App.Metrics.Apdex;
using App.Metrics.Counter;
using App.Metrics.Gauge;
using App.Metrics.Histogram;
using App.Metrics.ReservoirSampling.ExponentialDecay;
using App.Metrics.ReservoirSampling.Uniform;
using App.Metrics.Timer;

namespace ExampleApiMetric;

public static class MetricsGetWeather
{
    public static CounterOptions GetWeatherCount => new CounterOptions
    {
        Name = "WeatherCount",
        Context = "WeatherApi",
        MeasurementUnit = Unit.Calls
    };
    public static GaugeOptions Errors => new GaugeOptions
    {
        Name = "Errors",
        Context = "WeatherApi",
        MeasurementUnit = Unit.Bytes
    };
    public static HistogramOptions SampleHistogram => new HistogramOptions
    {
        Name = "Sample Histogram",
        Context = "WeatherApi",
        Reservoir = () => new DefaultAlgorithmRReservoir(),
        MeasurementUnit = Unit.MegaBytes
    };
    public static TimerOptions SampleTimer => new TimerOptions
    {
        Name = "Sample Timer",
        Context = "WeatherApi",
        MeasurementUnit = Unit.Items,
        DurationUnit = TimeUnit.Milliseconds,
        RateUnit = TimeUnit.Milliseconds,
        Reservoir = () => new DefaultForwardDecayingReservoir(sampleSize: 1028, alpha: 0.015)
    };

    public static ApdexOptions SampleApdex => new ApdexOptions
    {
        Name = "Sample Apdex",
        Context = "WeatherApi"
    };
}