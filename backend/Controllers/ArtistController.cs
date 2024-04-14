using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using art_gallery.Models;
using art_gallery.Persistence;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace art_gallery.Controllers
{
    /// <summary>
    /// Controller for managing artists.
    /// </summary>
    [Route("api/artists")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistDataAccess _dataAccess;

        public ArtistController(IArtistDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// Retrieves all artists.
        /// </summary>
        /// <returns>A list of all artists</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Artist>> GetArtists()
        {
            var artists = _dataAccess.GetArtists();
            return Ok(artists);
        }

        /// <summary>
        /// Retrieves an artist by its ID.
        /// </summary>
        /// <param name="id">The ID of the artist to retrieve</param>
        /// <returns>The artist with the specified ID</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Artist> GetArtist(int id)
        {
            var artist = _dataAccess.GetArtistById(id);

            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        /// <summary>
        /// Adds a new artist.
        /// </summary>
        /// <param name="artist">The artist to add</param>
        /// <returns>The newly added artist</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Artist> AddArtist([FromBody] Artist artist)
        {
            _dataAccess.InsertArtist(artist);
            return CreatedAtAction(nameof(GetArtist), new { id = artist.Id }, artist);
        }

        /// <summary>
        /// Updates an existing artist.
        /// </summary>
        /// <param name="id">The ID of the artist to update</param>
        /// <param name="artist">Updated information for the artist</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateArtist(int id, [FromBody] Artist artist)
        {
            if (id != artist.Id)
            {
                return BadRequest();
            }

            _dataAccess.UpdateArtist(artist);
            return NoContent();
        }

        /// <summary>
        /// Deletes an artist.
        /// </summary>
        /// <param name="id">The ID of the artist to delete</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteArtist(int id)
        {
            var artist = _dataAccess.GetArtistById(id);

            if (artist == null)
            {
                return NotFound();
            }

            _dataAccess.DeleteArtist(id);
            return NoContent();
        }
    }
}
