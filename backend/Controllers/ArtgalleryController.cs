using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using art_gallery.Models;
using art_gallery.Persistence;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace art_gallery.Controllers
{
    /// <summary>
    /// Controller for managing art galleries.
    /// </summary>
    [Route("api/artgallery")]
    [ApiController]
    public class ArtgalleryController : ControllerBase
    {
        private readonly IArtgalleryDataAccess _dataAccess;

        public ArtgalleryController(IArtgalleryDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// Retrieves all art galleries.
        /// </summary>
        /// <returns>A list of all art galleries</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Artgallery>> GetArtgalleries()
        {
            var artgalleries = _dataAccess.GetArtgalleries();
            return Ok(artgalleries);
        }

        /// <summary>
        /// Retrieves an art gallery by its ID.
        /// </summary>
        /// <param name="id">The ID of the art gallery to retrieve</param>
        /// <returns>The art gallery with the specified ID</returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "Artist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Artgallery> GetArtgallery(int id)
        {
            var artgallery = _dataAccess.GetArtgalleryById(id);

            if (artgallery == null)
            {
                return NotFound();
            }

            return Ok(artgallery);
        }

        /// <summary>
        /// Adds a new art gallery.
        /// </summary>
        /// <param name="artgallery">The art gallery to add</param>
        /// <returns>The newly added art gallery</returns>
        [HttpPost]
        [Authorize(Policy = "Curator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Artgallery> AddArtgallery([FromBody] Artgallery artgallery)
        {
            _dataAccess.InsertArtgallery(artgallery);
            return CreatedAtAction(nameof(GetArtgallery), new { id = artgallery.Id }, artgallery);
        }

        /// <summary>
        /// Updates an existing art gallery.
        /// </summary>
        /// <param name="id">The ID of the art gallery to update</param>
        /// <param name="artgallery">Updated information for the art gallery</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [Authorize(Policy = "Artist")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateArtgallery(int id, [FromBody] Artgallery artgallery)
        {
            if (id != artgallery.Id)
            {
                return BadRequest();
            }

            _dataAccess.UpdateArtgallery(artgallery);
            return NoContent();
        }

        /// <summary>
        /// Deletes an art gallery.
        /// </summary>
        /// <param name="id">The ID of the art gallery to delete</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteArtgallery(int id)
        {
            var artgallery = _dataAccess.GetArtgalleryById(id);

            if (artgallery == null)
            {
                return NotFound();
            }

            _dataAccess.DeleteArtgallery(id);
            return NoContent();
        }
    }
}
