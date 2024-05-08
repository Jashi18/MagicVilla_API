using MagicVillaAPI.Data;
using MagicVillaAPI.Logging;
using MagicVillaAPI.Models;
using MagicVillaAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly ILogging _logger;
        public VillaApiController(ILogging logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting All Villas", "");
            return Ok(VillaStore.VillaList);
        }

        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.Log("Get Villa Error with ID : " + id, "error");
                return BadRequest();
            }

            var villa = VillaStore.VillaList.Find(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDto)
        {
            if (VillaStore.VillaList.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa Already Exists");
                return BadRequest(ModelState);
            }
            villaDto.Id = VillaStore.VillaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            VillaStore.VillaList.Add(villaDto);

            return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
        }

        [HttpDelete("id", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if (villa is null)
            {
                return NotFound();
            }
            VillaStore.VillaList.Remove(villa);
            return NoContent();
        }


        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO VillaDto)
        {
            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);

            if (villa is null)
            {
                return NotFound();
            }
            villa.Name = VillaDto.Name;
            villa.Occupancy = VillaDto.Occupancy;
            villa.Sqft = VillaDto.Sqft;

            return NoContent();
        }

        [HttpPatch("id", Name = "UpdateVilla")]
        public IActionResult UpdatePartial(int id, JsonPatchDocument<VillaDTO> PatchDto)
        {
            if (id == 0 || PatchDto == null)
            {
                return BadRequest();
            }

            var villa = VillaStore.VillaList.FirstOrDefault(u => u.Id == id);
            if (villa is null)
            {
                return BadRequest();
            }

            PatchDto.ApplyTo(villa, ModelState);
            return NoContent();
        }
    }
}
