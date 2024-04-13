using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using art_gallery.Models;
using art_gallery.Persistence;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace art_gallery.Controllers
{
    [Route("api/artfacts")]
    [ApiController]
    public class ArtfactController : ControllerBase
    {
        private readonly IArtfactDataAccess _dataAccess;

        public ArtfactController(IArtfactDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Artfact>> GetArtfacts()
        {
            var artfacts = _dataAccess.GetArtfacts();
            return Ok(artfacts);
        }
        [HttpGet("{id}")]
        [Authorize(Policy = "Artist")]
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

        [HttpPost]
        [Authorize(Policy = "Curator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Artfact> AddArtfact([FromBody] Artfact artfact)
        {
            _dataAccess.InsertArtfact(artfact);
            return CreatedAtAction(nameof(GetArtfact), new { id = artfact.Id }, artfact);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Artist")]
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

     
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
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

