using System.Collections.Generic;
using art_gallery.Models;
using MongoDB.Driver;
using BCrypt.Net;

namespace art_gallery.Persistence
{
    public class UserEF : IUserDataAccess
    {
        private readonly ArtContext _context = new ArtContext();

        public List<User> GetUsers()
        {
            return _context.Users.Find(_ => true).ToList();
        }

        public User GetUserById(int id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return _context.Users.Find(filter).FirstOrDefault();
        }

        public void InsertUser(User user)
        {
            _context.Users.InsertOne(user);
        }

        public void UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            _context.Users.ReplaceOne(filter, user);
        }

        public void UpdateCredentials(int id, string email, string password)
        {

            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var update = Builders<User>.Update
                .Set(u => u.Email, email)
                .Set(u => u.PasswordHash, password);

            _context.Users.UpdateOne(filter, update);
        }

        public void DeleteUser(int id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            _context.Users.DeleteOne(filter);
        }
    }
}
