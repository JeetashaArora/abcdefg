using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using art_gallery.Models;
using art_gallery.Persistence;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace art_gallery.Controllers
{
    /// <summary>
    /// Controller for managing artifacts.
    /// </summary>
    [Route("api/artifacts")]
    [ApiController]
    public class ArtifactController : ControllerBase
    {
        private readonly IArtifactDataAccess _dataAccess;

        public ArtifactController(IArtifactDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        /// <summary>
        /// Retrieves all artifacts.
        /// </summary>
        /// <returns>All artifacts</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/artifacts
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Artifact>> GetArtifacts()
        {
            var artifacts = _dataAccess.GetArtifacts();
            return Ok(artifacts);
        }

        /// <summary>
        /// Retrieves a specific artifact by its ID.
        /// </summary>
        /// <param name="id">The ID of the artifact to retrieve</param>
        /// <returns>The requested artifact</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// GET /api/artifacts/1
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Artifact> GetArtifact(int id)
        {
            var artifact = _dataAccess.GetArtifactById(id);

            if (artifact == null)
            {
                return NotFound();
            }

            return Ok(artifact);
        }

        /// <summary>
        /// Adds a new artifact.
        /// </summary>
        /// <param name="artifact">The artifact to add</param>
        /// <returns>The newly added artifact</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// POST /api/artifacts
        /// {
        ///     "id": 1,
        ///     "name": "Mona Lisa",
        ///     "image": "https://example.com/monalisa.jpg",
        ///     "artist": "Leonardo da Vinci",
        ///     "description": "A portrait painting by Leonardo da Vinci"
        /// }
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Artifact> AddArtifact([FromBody] Artifact artifact)
        {
            _dataAccess.InsertArtifact(artifact);
            return CreatedAtAction(nameof(GetArtifact), new { id = artifact.Id }, artifact);
        }

        /// <summary>
        /// Updates an existing artifact.
        /// </summary>
        /// <param name="id">The ID of the artifact to update</param>
        /// <param name="artifact">Updated information for the artifact</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        ///
        /// PUT /api/artifacts/1
        /// {
        ///     "id": 1,
        ///     "name": "Updated Mona Lisa",
        ///     "image": "https://example.com/updated-monalisa.jpg",
        ///     "artist": "Leonardo da Vinci",
        ///     "description": "An updated description of Mona Lisa"
        /// }
        /// </remarks>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateArtifact(int id, [FromBody] Artifact artifact)
        {
            if (id != artifact.Id)
            {
                return BadRequest();
            }

            _dataAccess.UpdateArtifact(artifact);
            return NoContent();
        }

        /// <summary>
        /// Deletes an artifact.
        /// </summary>
        /// <param name="id">The ID of the artifact to delete</param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        /// 
        /// DELETE /api/artifacts/1
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteArtifact(int id)
        {
            var artifact = _dataAccess.GetArtifactById(id);

            if (artifact == null)
            {
                return NotFound();
            }

            _dataAccess.DeleteArtifact(id);
            return NoContent();
        }
    }
}

