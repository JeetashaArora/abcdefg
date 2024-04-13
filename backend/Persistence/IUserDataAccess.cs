using System.Collections.Generic;
using art_gallery.Models;


namespace art_gallery.Persistence
{
    public interface IUserDataAccess
    {
        List<User> GetUsers();
        User GetUserById(int id);
        void InsertUser(User user);
        void UpdateUser(User user);
        void UpdateCredentials(int id, string email, string passwordHash);
        void DeleteUser(int id);
    }
}