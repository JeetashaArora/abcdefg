using System.Collections.Generic;
using art_gallery.Models;

namespace art_gallery.Persistence
{
    public interface IArtfactDataAccess
    {
        List<Artfact> GetArtfacts();
        Artfact GetArtfactById(int id);
        void InsertArtfact(Artfact artifact);
        void UpdateArtfact(Artfact artifact);
        void DeleteArtfact(int id);
    }
}
