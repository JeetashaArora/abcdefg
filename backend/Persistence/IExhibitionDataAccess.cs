using System.Collections.Generic;
using art_gallery.Models;

namespace art_gallery.Persistence
{
    public interface IExhibitionDataAccess
    {
        List<Exhibition> GetExhibitions();
        Exhibition GetExhibitionById(int id);
        void InsertExhibition(Exhibition exhibition);
        void UpdateExhibition(Exhibition exhibition);
        void DeleteExhibition(int id);
    }
}
