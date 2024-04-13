using System.Collections.Generic;
using art_gallery.Models;

namespace art_gallery.Persistence
{
    public interface IArtistDataAccess
    {
        List<Artist> GetArtists();
        Artist GetArtistById(int id);
        void InsertArtist(Artist artist);
        void UpdateArtist(Artist artist);
        void DeleteArtist(int id);
    }
}
