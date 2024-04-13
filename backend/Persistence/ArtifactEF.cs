using System.Collections.Generic;
using art_gallery.Models;
using MongoDB.Driver;

namespace art_gallery.Persistence
{
    public class ArtifactEF : IArtifactDataAccess
    {
        private readonly ArtContext _context = new ArtContext();

        public List<Artifact> GetArtifacts()
        {
            return _context.Artifacts.Find(_ => true).ToList();
        }

        public Artifact GetArtifactById(int id)
        {
            var filter = Builders<Artifact>.Filter.Eq(a => a.Id, id);
            return _context.Artifacts.Find(filter).FirstOrDefault();
        }

        public void InsertArtifact(Artifact artifact)
        {
            _context.Artifacts.InsertOne(artifact);
        }

        public void UpdateArtifact(Artifact artifact)
        {
            var filter = Builders<Artifact>.Filter.Eq(a => a.Id, artifact.Id);
            _context.Artifacts.ReplaceOne(filter, artifact);
        }

        public void DeleteArtifact(int id)
        {
            var filter = Builders<Artifact>.Filter.Eq(a => a.Id, id);
            _context.Artifacts.DeleteOne(filter);
        }
    }
}
