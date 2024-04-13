using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using art_gallery.Models;

namespace art_gallery.Persistence
{
    public partial class ArtContext : DbContext
    {
        private readonly IMongoDatabase _database;

        public ArtContext()
        {
            var connectionString = "mongodb+srv://tanay4847be22:2WNYy2Q9L6fFohsf@cluster0.uhez5xq.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
            var databaseName = "sit331";

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Artifact> Artifacts => _database.GetCollection<Artifact>("Artifacts");
        public IMongoCollection<Exhibition> Exhibitions => _database.GetCollection<Exhibition>("Exhibitions");
        public IMongoCollection<ArtStyle> ArtStyles => _database.GetCollection<ArtStyle>("ArtStyles");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Artist> Artists => _database.GetCollection<Artist>("Artists");
        public IMongoCollection<Artfact> Artfacts => _database.GetCollection<Artfact>("Artfacts");
        public IMongoCollection<Artgallery> Artgalleries => _database.GetCollection<Artgallery>("Artgalleries");
    }
}
