using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using art_gallery.Models;
using art_gallery.Persistence;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace art_gallery.Controllers
{
    [Route("api/artgallery")]
    [ApiController]
    public class ArtgalleryController : ControllerBase
    {
        private readonly IArtgalleryDataAccess _dataAccess;

        public ArtgalleryController(IArtgalleryDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Artgallery>> GetArtgalleries()
        {
            var artgalleries = _dataAccess.GetArtgalleries();
            return Ok(artgalleries);
        }
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

        [HttpPost]
        [Authorize(Policy = "Curator")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Artgallery> AddArtgallery([FromBody] Artgallery artgallery)
        {
            _dataAccess.InsertArtgallery(artgallery);
            return CreatedAtAction(nameof(GetArtgallery), new { id = artgallery.Id }, artgallery);
        }

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

