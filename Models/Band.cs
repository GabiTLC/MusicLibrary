using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MusicLibrary.Models
{
    public class Band
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("albums")]
        public List<Album> Albums { get; set; }
    }

    public class Album
    {
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("songs")]
        public List<Song> Songs { get; set; }
        [BsonElement("description")]
        public string Description { get; set; }
    }

    public class Song
    {
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("length")]
        public string Length { get; set; }
    }
}