using System.Collections.Generic;
using art_gallery.Models;
using MongoDB.Driver;

namespace art_gallery.Persistence
{
    public class ArtfactEF : IArtfactDataAccess
    {
        private readonly ArtContext _context = new ArtContext();

        public List<Artfact> GetArtfacts()
        {
            return _context.Artfacts.Find(_ => true).ToList();
        }

        public Artfact GetArtfactById(int id)
        {
            var filter = Builders<Artfact>.Filter.Eq(a => a.Id, id);
            return _context.Artfacts.Find(filter).FirstOrDefault();
        }

        public void InsertArtfact(Artfact artfact)
        {
            _context.Artfacts.InsertOne(artfact);
        }

        public void UpdateArtfact(Artfact artfact)
        {
            var filter = Builders<Artfact>.Filter.Eq(a => a.Id, artfact.Id);
            _context.Artfacts.ReplaceOne(filter, artfact);
        }

        public void DeleteArtfact(int id)
        {
            var filter = Builders<Artfact>.Filter.Eq(a => a.Id, id);
            _context.Artfacts.DeleteOne(filter);
        }
    }
}
