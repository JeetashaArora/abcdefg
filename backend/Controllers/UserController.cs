using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using art_gallery.Models;
using art_gallery.Persistence;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace art_gallery.Controllers
{
    /// <summary>
    /// Controller for managing user accounts.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserDataAccess _userDataAccess;

        public UserController(IUserDataAccess userDataAccess)
        {
            _userDataAccess = userDataAccess;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>All users</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/users
        /// </remarks>
        [HttpGet]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _userDataAccess.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a specific user by its ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve</param>
        /// <returns>The requested user</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/users/1
        /// </remarks>
        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userDataAccess.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>The newly added user</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// POST /api/users
        /// {
        ///     "firstName": "John",
        ///     "lastName": "Doe",
        ///     "description": "User description",
        ///     "role": "User",
        /// }
        /// </remarks>
        [HttpPost]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<User> InsertUser(User user)
        {
            _userDataAccess.InsertUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updatedUser">The updated user information.</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        /// PUT /api/users/1
        /// {
        ///     "id": 1,
        ///     "firstName": "John",
        ///     "lastName": "Doe",
        ///     "description": "Updated description",
        ///     "role": "User"
        /// }
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var existingUser = _userDataAccess.GetUserById(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Description = updatedUser.Description;
            existingUser.Role = updatedUser.Role;
            existingUser.ModifiedDate = DateTime.Now;

            _userDataAccess.UpdateUser(existingUser);

            return NoContent();
        }

        /// <summary>
        /// Updates the credentials of a user.
        /// </summary>
        /// <param name="id">The ID of the user to update</param>
        /// <param name="login">Updated login credentials</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// PATCH /api/users/1/credentials
        /// {
        ///     "email": "user@example.com",
        ///     "password": "new_password"
        /// }
        /// </remarks>
        [HttpPatch("{id}/credentials")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateCredentials(int id, Login login)
        {
            var user = _userDataAccess.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(login.Password);

            _userDataAccess.UpdateCredentials(id, login.Email, hashedPassword);

            return NoContent();
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        /// DELETE /api/users/1
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int id)
        {
            var user = _userDataAccess.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            _userDataAccess.DeleteUser(id);
            return NoContent();
        }
    }
}
