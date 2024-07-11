using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MusicLibrary.Models;
using MusicLibrary.Services;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Add swagger service
//builder.Services.AddSwaggerGen();

// Register MongoDB client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("MongoDb");
    return new MongoClient(connectionString);
});

builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("mldb");
});


// Register the BandService
builder.Services.AddSingleton<BandService>();

//App builder
var app = builder.Build();


// Connection string for MongoDB server
var connectionString = "mongodb://root:example@mongodb:27017/"; //to take from appsettings

// Create a MongoDB client
var client = new MongoClient(connectionString);

// Get a reference to the database
var database = client.GetDatabase("mldb");  //to take from env

// Read the JSON file
string text = File.ReadAllText(@"./Data/data2.json");   //to take from env
Console.WriteLine(text);
    
// Parse JSON to a JArray
JArray jsonArray = JArray.Parse(text);

// Convert JArray to List<Band>
List<Band> bands = jsonArray.ToObject<List<Band>>();


//// Perform additional validation or sanitation if needed
//foreach (var band in bands)
//{
//    foreach (var album in band.Albums)
//    {
//        // Example: Removing newlines and extra spaces from descriptions
//        //album.Description = album.Description?.Trim().Replace("\"", "\\\"");
//        //Console.WriteLine($"Invalid JSON data for band {band.Name} {album.Description}");
//    }
//}


// Get a reference to the collection
var collection = database.GetCollection<Band>("Band");

// Insert the sanitized data into MongoDB
foreach (var band in bands)
{
    collection.InsertOne(band);
}

Console.WriteLine("Record inserted successfully.");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Band}/{action=Get}/{id?}");

app.Run();
