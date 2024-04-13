using System.Collections.Generic;
using art_gallery.Models;

namespace art_gallery.Persistence
{
    public interface IArtStyleDataAccess
    {
        List<ArtStyle> GetArtStyles();
        ArtStyle GetArtStyleById(int id);
        void InsertArtStyle(ArtStyle artStyle);
        void UpdateArtStyle(ArtStyle artStyle);
        void DeleteArtStyle(int id);
    }
}
