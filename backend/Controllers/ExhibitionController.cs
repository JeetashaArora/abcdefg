using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using art_gallery.Models;
using art_gallery.Persistence;
using Microsoft.AspNetCore.Http;
using System;

namespace art_gallery.Controllers
{
    /// <summary>
    /// Controller for managing exhibitions.
    /// </summary>
    [Route("api/exhibitions")]
    [ApiController]
    public class ExhibitionController : ControllerBase
    {
        private readonly IExhibitionDataAccess _dataAccess;

        public ExhibitionController(IExhibitionDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// Retrieves all exhibitions.
        /// </summary>
        /// <returns>All exhibitions</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/exhibitions
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Exhibition>> GetExhibitions()
        {
            var exhibitions = _dataAccess.GetExhibitions();
            return Ok(exhibitions);
        }

        /// <summary>
        /// Retrieves a specific exhibition by its ID.
        /// </summary>
        /// <param name="id">The ID of the exhibition to retrieve</param>
        /// <returns>The requested exhibition</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/exhibitions/1
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Exhibition> GetExhibition(int id)
        {
            var exhibition = _dataAccess.GetExhibitionById(id);

            if (exhibition == null)
            {
                return NotFound();
            }

            return Ok(exhibition);
        }

        /// <summary>
        /// Adds a new exhibition.
        /// </summary>
        /// <param name="exhibition">The exhibition to add</param>
        /// <returns>The newly added exhibition</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// POST /api/exhibitions
        /// {
        ///     "name": "Impressionist Masterpieces",
        ///     "description": "A collection of Impressionist artworks from various artists.",
        ///     "place": "Art Gallery",
        ///     "date": "2024-05-01T00:00:00"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Exhibition> AddExhibition([FromBody] Exhibition exhibition)
        {
            _dataAccess.InsertExhibition(exhibition);
            return CreatedAtAction(nameof(GetExhibition), new { id = exhibition.Id }, exhibition);
        }

        /// <summary>
        /// Updates an existing exhibition.
        /// </summary>
        /// <param name="id">The ID of the exhibition to update</param>
        /// <param name="exhibition">Updated information for the exhibition</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// PUT /api/exhibitions/1
        /// {
        ///     "id": 1,
        ///     "name": "Updated Impressionist Masterpieces",
        ///     "description": "An updated description of the Impressionist Masterpieces exhibition.",
        ///     "place": "Art Gallery",
        ///     "date": "2024-05-01T00:00:00"
        /// }
        /// </remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateExhibition(int id, [FromBody] Exhibition exhibition)
        {
            if (id != exhibition.Id)
            {
                return BadRequest();
            }

            _dataAccess.UpdateExhibition(exhibition);
            return NoContent();
        }

        /// <summary>
        /// Deletes an exhibition.
        /// </summary>
        /// <param name="id">The ID of the exhibition to delete</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        /// DELETE /api/exhibitions/1
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteExhibition(int id)
        {
            var exhibition = _dataAccess.GetExhibitionById(id);

            if (exhibition == null)
            {
                return NotFound();
            }

            _dataAccess.DeleteExhibition(id);
            return NoContent();
        }
    }
}


