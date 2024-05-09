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
        private readonly ApplicationDbContext _context;
        public VillaApiController(ILogging logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Getting All Villas", "");
            return Ok(_context.Villas.ToList());
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

            var villa = _context.Villas.FirstOrDefault(u => u.Id == id);
            _logger.Log("Get Villa with ID : " + id, "");
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
            if (_context.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Villa Already Exists");
                _logger.Log("Villa Already Exists with ID  : " + villaDto.Id, "error");
                return BadRequest(ModelState);
            }

            Villa model = new ()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
            };

            _context.Villas.Add(model);
            _context.SaveChanges();

            _logger.Log("Villa was Created with", "");

            return CreatedAtRoute("GetVilla", new {id =  villaDto.Id}, villaDto);
        }

        [HttpDelete("id", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            var villa = _context.Villas.FirstOrDefault(u => u.Id == id);
            if (villa is null)
            {
                _logger.Log("Villa was not found with ID : " + id, "error");
                return NotFound();
            }
            _context.Villas.Remove(villa);
            _context.SaveChanges();
            _logger.Log("Villa Was Deleted with ID : " + id, "");
            return NoContent();
        }


        [HttpPut("id", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDto)
        {
            if (villaDto == null)
            {
                _logger.Log("Villa was not found", "error");
                return BadRequest();
            }
            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
            };
            _context.Update(model);
            _context.SaveChanges();
            _logger.Log("Villa was Updated with ID : " + id, "");
            return NoContent();
        }

        [HttpPatch("id", Name = "UpdateVilla")]
        public IActionResult UpdatePartial(int id, JsonPatchDocument<VillaDTO> PatchDto)
        {
            if (id == 0 || PatchDto == null)
            {
                return BadRequest();
            }

            var villa = _context.Villas.FirstOrDefault(u => u.Id == id);

            if (villa is null)
            {
                return BadRequest();
            }
            VillaDTO villaDto = new VillaDTO()
            {
                Id = villa.Id,
                Name = villa.Name,
                Details = villa.Details,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
            };



            PatchDto.ApplyTo(villaDto, ModelState);

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Details = villaDto.Details,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
            };

            _context.Villas.Update(model);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
