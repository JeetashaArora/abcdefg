using System.Collections.Generic;
using art_gallery.Models;
using MongoDB.Driver;

namespace art_gallery.Persistence
{
    public class ArtistEF : IArtistDataAccess
    {
        private readonly ArtContext _context = new ArtContext();

        public List<Artist> GetArtists()
        {
            return _context.Artists.Find(_ => true).ToList();
        }

        public Artist GetArtistById(int id)
        {
            var filter = Builders<Artist>.Filter.Eq(a => a.Id, id);
            return _context.Artists.Find(filter).FirstOrDefault();
        }

        public void InsertArtist(Artist artist)
        {
            _context.Artists.InsertOne(artist);
        }

        public void UpdateArtist(Artist artist)
        {
            var filter = Builders<Artist>.Filter.Eq(a => a.Id, artist.Id);
            _context.Artists.ReplaceOne(filter, artist);
        }

        public void DeleteArtist(int id)
        {
            var filter = Builders<Artist>.Filter.Eq(a => a.Id, id);
            _context.Artists.DeleteOne(filter);
        }
    }
}
