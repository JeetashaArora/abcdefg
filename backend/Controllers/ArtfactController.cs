using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using art_gallery.Models;
using art_gallery.Persistence;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace art_gallery.Controllers
{
    /// <summary>
    /// Controller for managing art facts.
    /// </summary>
    [Route("api/artfacts")]
    [ApiController]
    public class ArtfactController : ControllerBase
    {
        private readonly IArtfactDataAccess _dataAccess;

        public ArtfactController(IArtfactDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// Retrieves all art facts.
        /// </summary>
        /// <returns>All art facts</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/artfacts
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Artfact>> GetArtfacts()
        {
            var artfacts = _dataAccess.GetArtfacts();
            return Ok(artfacts);
        }

        /// <summary>
        /// Retrieves a specific art fact by its ID.
        /// </summary>
        /// <param name="id">The ID of the art fact to retrieve</param>
        /// <returns>The requested art fact</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/artfacts/1
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Artfact> GetArtfact(int id)
        {
            var artfact = _dataAccess.GetArtfactById(id);

            if (artfact == null)
            {
                return NotFound();
            }

            return Ok(artfact);
        }

        /// <summary>
        /// Adds a new art fact.
        /// </summary>
        /// <param name="artfact">The art fact to add</param>
        /// <returns>The newly added art fact</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// POST /api/artfacts
        /// {
        ///     "year": "2003-09-17T00:00:00",
        ///     "image": "https://example.com/image5.jpg",
        ///     "description": "Claude Monet's 'Water Lilies' series captures the beauty of his flower garden at his home in Giverny, France, with its serene ponds and floating lilies."
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Artfact> AddArtfact([FromBody] Artfact artfact)
        {
            _dataAccess.InsertArtfact(artfact);
            return CreatedAtAction(nameof(GetArtfact), new { id = artfact.Id }, artfact);
        }

        /// <summary>
        /// Updates an existing art fact.
        /// </summary>
        /// <param name="id">The ID of the art fact to update</param>
        /// <param name="artfact">Updated information for the art fact</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// PUT /api/artfacts/1
        /// {
        ///     "id": 1,
        ///     "year": "2003-09-17T00:00:00",
        ///     "image": "https://example.com/updated-image.jpg",
        ///     "description": "An updated description of the art fact"
        /// }
        /// </remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateArtfact(int id, [FromBody] Artfact artfact)
        {
            if (id != artfact.Id)
            {
                return BadRequest();
            }

            _dataAccess.UpdateArtfact(artfact);
            return NoContent();
        }

        /// <summary>
        /// Deletes an art fact.
        /// </summary>
        /// <param name="id">The ID of the art fact to delete</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        /// DELETE /api/artfacts/1
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteArtfact(int id)
        {
            var artfact = _dataAccess.GetArtfactById(id);

            if (artfact == null)
            {
                return NotFound();
            }

            _dataAccess.DeleteArtfact(id);
            return NoContent();
        }
    }
}
