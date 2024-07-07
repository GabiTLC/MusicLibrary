using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MusicLibrary.Models
{
    public class Band
    {
        public string Name { get; set; }
        public List<Album> Albums { get; set; }
    }

    public class Album
    {
        public string Title { get; set; }
        public List<Song> Songs { get; set; }
        public string Description { get; set; }
    }

    public class Song
    {
        public string Title { get; set; }
        public string Length { get; set; }
    }
}
