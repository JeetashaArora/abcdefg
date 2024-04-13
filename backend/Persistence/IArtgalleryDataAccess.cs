using System.Collections.Generic;
using art_gallery.Models;

namespace art_gallery.Persistence
{
    public interface IArtgalleryDataAccess
    {
        List<Artgallery> GetArtgalleries();
        Artgallery GetArtgalleryById(int id);
        void InsertArtgallery(Artgallery artifact);
        void UpdateArtgallery(Artgallery artifact);
        void DeleteArtgallery(int id);
    }
}
