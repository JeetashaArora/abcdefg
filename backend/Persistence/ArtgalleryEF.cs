using System.Collections.Generic;
using art_gallery.Models;
using MongoDB.Driver;

namespace art_gallery.Persistence
{
    public class ArtgalleryEF : IArtgalleryDataAccess
    {
        private readonly ArtContext _context = new ArtContext();

        public List<Artgallery> GetArtgalleries()
        {
            return _context.Artgalleries.Find(_ => true).ToList();
        }

        public Artgallery GetArtgalleryById(int id)
        {
            var filter = Builders<Artgallery>.Filter.Eq(a => a.Id, id);
            return _context.Artgalleries.Find(filter).FirstOrDefault();
        }

        public void InsertArtgallery(Artgallery artgallery)
        {
            _context.Artgalleries.InsertOne(artgallery);
        }

        public void UpdateArtgallery(Artgallery artgallery)
        {
            var filter = Builders<Artgallery>.Filter.Eq(a => a.Id, artgallery.Id);
            _context.Artgalleries.ReplaceOne(filter, artgallery);
        }

        public void DeleteArtgallery(int id)
        {
            var filter = Builders<Artgallery>.Filter.Eq(a => a.Id, id);
            _context.Artgalleries.DeleteOne(filter);
        }
    }
}
