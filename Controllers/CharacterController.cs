using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarrativusAPI.Data;
using NarrativusAPI.Models;

namespace NarrativusAPI.Controllers
{
    [ApiController]
    [Route("v1/characters")]
    public class CharacterController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var characters = await context
                .Characters
                .AsNoTracking()
                .ToListAsync();

            return Ok(characters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var character = await context
                .Characters
                .AsNoTracking()
                .Include(c => c.Relationships)
                .Include(c => c.BelongsTo)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(character);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await context.Characters.AddAsync(character);
                await context.SaveChangesAsync();
                return Created("v1/characters/{location.Id}", character);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] Character character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dbCharacter = await context
                .Characters
                .FirstOrDefaultAsync(x => x.Id == character.Id);

            if (dbCharacter == null)
            {
                return NotFound();
            }

            dbCharacter.Name = character.Name;
            dbCharacter.Ancestry = character.Ancestry;
            dbCharacter.Family = character.Family;
            dbCharacter.Sex = character.Sex;
            dbCharacter.PhysicalCharacteristics = character.PhysicalCharacteristics;
            dbCharacter.Background = character.Background;
            dbCharacter.Roleplay = character.Roleplay;
            dbCharacter.DateOfBirth = character.DateOfBirth;
            dbCharacter.DateOfDeath = character.DateOfDeath;
            dbCharacter.Relationships = character.Relationships;
            dbCharacter.BelongsToId = character.BelongsToId;
            dbCharacter.BelongsTo = character.BelongsTo;
            dbCharacter.Appearances = character.Appearances;

            try
            {
                context.Characters.Update(dbCharacter);
                await context.SaveChangesAsync();

                return Ok(dbCharacter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(
                [FromServices] AppDbContext context,
                [FromRoute] int id)
        {
            var dbCharacter = await context
                .Characters
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Characters.Remove(dbCharacter);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
