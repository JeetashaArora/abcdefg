using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using art_gallery.Models;
using art_gallery.Persistence;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace art_gallery.Controllers
{
   
    [Route("api/artists")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistDataAccess _dataAccess;

        public ArtistController(IArtistDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Artist>> GetArtists()
        {
            var artists = _dataAccess.GetArtists();
            return Ok(artists);
        }

       
        [HttpGet("{id}")]
        [Authorize(Policy = "Artist")]
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

        [HttpPost]
        [Authorize(Policy = "Curator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Artist> AddArtist([FromBody] Artist artist)
        {
            _dataAccess.InsertArtist(artist);
            return CreatedAtAction(nameof(GetArtist), new { id = artist.Id }, artist);
        }

     
        [HttpPut("{id}")]
        [Authorize(Policy = "Artist")]
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

 
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
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

