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
    /// Controller for managing art styles.
    /// </summary>
    [Route("api/artstyles")]
    [ApiController]
    public class ArtStyleController : ControllerBase
    {
        private readonly IArtStyleDataAccess _dataAccess;

        public ArtStyleController(IArtStyleDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// Retrieves all art styles.
        /// </summary>
        /// <returns>All art styles</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/artstyles
        /// </remarks>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ArtStyle>> GetArtStyles()
        {
            var artStyles = _dataAccess.GetArtStyles();
            return Ok(artStyles);
        }

        /// <summary>
        /// Retrieves a specific art style by its ID.
        /// </summary>
        /// <param name="id">The ID of the art style to retrieve</param>
        /// <returns>The requested art style</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/artstyles/1
        /// </remarks>
        [HttpGet("{id}")]
        [Authorize(Policy = "Artist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ArtStyle> GetArtStyle(int id)
        {
            var artStyle = _dataAccess.GetArtStyleById(id);

            if (artStyle == null)
            {
                return NotFound();
            }

            return Ok(artStyle);
        }

        /// <summary>
        /// Adds a new art style.
        /// </summary>
        /// <param name="artStyle">The art style to add</param>
        /// <returns>The newly added art style</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// POST /api/artstyles
        /// {
        ///     "name": "Impressionism",
        ///     "description": "Art movement characterized by small, thin, yet visible brush strokes, open composition, emphasis on accurate depiction of light in its changing qualities, ordinary subject matter, inclusion of movement as a crucial element of human perception and experience, and unusual visual angles.",
        ///     "image": "https://example.com/impressionism.jpg",
        ///     "creator": "Claude Monet"
        /// }
        /// </remarks>
        [HttpPost]
        [Authorize(Policy = "Curator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ArtStyle> AddArtStyle([FromBody] ArtStyle artStyle)
        {
            _dataAccess.InsertArtStyle(artStyle);
            return CreatedAtAction(nameof(GetArtStyle), new { id = artStyle.Id }, artStyle);
        }

        /// <summary>
        /// Updates an existing art style.
        /// </summary>
        /// <param name="id">The ID of the art style to update</param>
        /// <param name="artStyle">Updated information for the art style</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// PUT /api/artstyles/1
        /// {
        ///     "id": 1,
        ///     "name": "Updated Impressionism",
        ///     "description": "An updated description of Impressionism",
        ///     "image": "https://example.com/updated-impressionism.jpg",
        ///     "creator": "Claude Monet"
        /// }
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize(Policy = "Artist")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateArtStyle(int id, [FromBody] ArtStyle artStyle)
        {
            if (id != artStyle.Id)
            {
                return BadRequest();
            }

            _dataAccess.UpdateArtStyle(artStyle);
            return NoContent();
        }

        /// <summary>
        /// Deletes an art style.
        /// </summary>
        /// <param name="id">The ID of the art style to delete</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        /// DELETE /api/artstyles/1
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteArtStyle(int id)
        {
            var artStyle = _dataAccess.GetArtStyleById(id);

            if (artStyle == null)
            {
                return NotFound();
            }

            _dataAccess.DeleteArtStyle(id);
            return NoContent();
        }
    }
}

