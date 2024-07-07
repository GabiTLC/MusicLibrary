using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MusicLibrary.Models;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Connection string for MongoDB server
var connectionString = "mongodb://root:example@mongodb:27017/"; //to take from appsettings

// Create a MongoDB client
var client = new MongoClient(connectionString);

// Get a reference to the database
var database = client.GetDatabase("mldb");

// Read the JSON file
string text = File.ReadAllText(@"./Data/data2.json");
Console.WriteLine(text);

//string cleanedJsonData = Regex.Replace(text, @"[^\u0020-\u007E\u00A0-\u00FF]", string.Empty);
//cleanedJsonData = Regex.Replace(cleanedJsonData, @"\\n|\\t", string.Empty);
//cleanedJsonData = Regex.Replace(cleanedJsonData, @"\s+", " ");
//cleanedJsonData = cleanedJsonData.Replace("\"", "\\\"");
// Deserialize the JSON into the Record class

var record = JsonConvert.DeserializeObject<List<Band>>(text);


Console.WriteLine("Warning! Invalid Record!");


// Get a reference to the collection
var collection = database.GetCollection<Band>("Band");

// Insert the record into the collection
collection.InsertMany(record);

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

app.Run();
