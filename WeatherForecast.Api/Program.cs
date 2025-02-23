using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var weatherDataQueue = "weather-data";
var localStackUrl = "http://localhost:4566";
var localStcakRegion = "us-east-1";

if (builder.Environment.IsDevelopment())
{
    var awsOptions = builder.Configuration.GetAWSOptions();
    awsOptions.DefaultClientConfig.ServiceURL = localStackUrl;
    awsOptions.DefaultClientConfig.AuthenticationRegion = localStcakRegion;
    builder.Services.AddDefaultAWSOptions(awsOptions);
}

builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddAWSService<IAmazonSQS>();
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/weatherforecast", async (WeatherForecastData data, IDynamoDBContext dynamoDbContext, IAmazonSQS publisher) =>
{
    Console.WriteLine($"Received WeatherForecast data for city {data.City}");
    await dynamoDbContext.SaveAsync(data);
    await publisher.SendMessageAsync(
        new SendMessageRequest(weatherDataQueue,
       JsonSerializer.Serialize(new WeatherForecastAddedEvent()
       {
           City = data.City,
           DateTime = data.Date,
           TemperatureC = data.TemperatureC,
           Summary = data.Summary
       })));
})
    .WithName("PostWeatherForecast")
    .DisableAntiforgery()
    .WithOpenApi();

app.Run();


public class WeatherForecastData
{
    public string City { get; set; }
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string Summary { get; set; }
}

public class WeatherForecastAddedEvent
{
    public string City { get; set; }
    public DateTime DateTime { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; }
}
