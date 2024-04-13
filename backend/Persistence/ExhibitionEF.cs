using System.Collections.Generic;
using art_gallery.Models;
using MongoDB.Driver;

namespace art_gallery.Persistence
{
    public class ExhibitionEF : IExhibitionDataAccess
    {
        private readonly ArtContext _context = new ArtContext();

        public List<Exhibition> GetExhibitions()
        {
            return _context.Exhibitions.Find(_ => true).ToList();
        }

        public Exhibition GetExhibitionById(int id)
        {
            var filter = Builders<Exhibition>.Filter.Eq(e => e.Id, id);
            return _context.Exhibitions.Find(filter).FirstOrDefault();
        }

        public void InsertExhibition(Exhibition exhibition)
        {
            _context.Exhibitions.InsertOne(exhibition);
        }

        public void UpdateExhibition(Exhibition exhibition)
        {
            var filter = Builders<Exhibition>.Filter.Eq(e => e.Id, exhibition.Id);
            _context.Exhibitions.ReplaceOne(filter, exhibition);
        }

        public void DeleteExhibition(int id)
        {
            var filter = Builders<Exhibition>.Filter.Eq(e => e.Id, id);
            _context.Exhibitions.DeleteOne(filter);
        }
    }
}
