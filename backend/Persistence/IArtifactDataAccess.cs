using System.Collections.Generic;
using art_gallery.Models;

namespace art_gallery.Persistence
{
    public interface IArtifactDataAccess
    {
        List<Artifact> GetArtifacts();
        Artifact GetArtifactById(int id);
        void InsertArtifact(Artifact artifact);
        void UpdateArtifact(Artifact artifact);
        void DeleteArtifact(int id);
    }
}
