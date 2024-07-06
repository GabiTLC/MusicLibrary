using MongoDB.Bson;

namespace MusicLibrary.Models
{
    public class Band
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Album> Albums { get; } = new List<Album>(); 

        
    }
    public class Album 
    {
        public int Id { get; set; }

        public int BandId { get; set; }

        public string Title { get; set; }

        public ICollection<Song> Songs { get; } = new List<Song>();

        public Band Band { get; set; } = null!;

        public string Description { get; set; }

    }
    public class Song
    {
        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string Title { get; set; }

        public string Length { get; set; }

        public Album Album { get; set; } = null!;
    }
}
