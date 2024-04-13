using System.Collections.Generic;
using art_gallery.Models;
using MongoDB.Driver;

namespace art_gallery.Persistence
{
    public class ArtStyleEF : IArtStyleDataAccess
    {
        private readonly ArtContext _context = new ArtContext();

        public List<ArtStyle> GetArtStyles()
        {
            return _context.ArtStyles.Find(_ => true).ToList();
        }

        public ArtStyle GetArtStyleById(int id)
        {
            var filter = Builders<ArtStyle>.Filter.Eq(a => a.Id, id);
            return _context.ArtStyles.Find(filter).FirstOrDefault();
        }

        public void InsertArtStyle(ArtStyle artStyle)
        {
            _context.ArtStyles.InsertOne(artStyle);
        }

        public void UpdateArtStyle(ArtStyle artStyle)
        {
            var filter = Builders<ArtStyle>.Filter.Eq(a => a.Id, artStyle.Id);
            _context.ArtStyles.ReplaceOne(filter, artStyle);
        }

        public void DeleteArtStyle(int id)
        {
            var filter = Builders<ArtStyle>.Filter.Eq(a => a.Id, id);
            _context.ArtStyles.DeleteOne(filter);
        }
    }
}
