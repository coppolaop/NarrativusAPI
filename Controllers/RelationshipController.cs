using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarrativusAPI.Data;
using NarrativusAPI.DTOs;
using NarrativusAPI.Models;

namespace NarrativusAPI.Controllers
{
    [ApiController]
    [Route("v1/relationships")]
    public class RelationshipController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var relationships = await context
                .Relationships
                .AsNoTracking()
                .ToListAsync();

            return Ok(relationships);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Relationship>> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var relationship = await context
                .Relationships
                .AsNoTracking()
                .Include(r => r.Owner)
                .Include(r => r.RelatedTo)
                .FirstOrDefaultAsync(r => r.Id == id);

            return Ok(relationship);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] RelationshipDTO relationshipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var owner = await context.Characters.FirstOrDefaultAsync(c => c.Id == relationshipDto.OwnerId);
                var relatedTo = await context.Characters.FirstOrDefaultAsync(c => c.Id == relationshipDto.RelatedToId);

                Relationship relationship = new Relationship
                {
                    OwnerId = relationshipDto.OwnerId,
                    Owner = owner,
                    RelatedToId = relationshipDto.RelatedToId,
                    RelatedTo = relatedTo,
                    Type = relationshipDto.Relation

                };
                await context.Relationships.AddAsync(relationship);
                await context.SaveChangesAsync();
                return Created("v1/relationships/{relationship.Id}", relationship);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] RelationshipDTO relationshipDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dbRelationship = await context
                .Relationships
                .FirstOrDefaultAsync(x => x.Id == relationshipDto.Id);

            if (dbRelationship == null)
            {
                return NotFound();
            }

            dbRelationship.Type = relationshipDto.Relation;
            dbRelationship.RelatedToId = relationshipDto.RelatedToId;
            dbRelationship.OwnerId = relationshipDto.OwnerId;

            try
            {
                var owner = await context.Characters.FirstOrDefaultAsync(c => c.Id == relationshipDto.OwnerId);
                var relatedTo = await context.Characters.FirstOrDefaultAsync(c => c.Id == relationshipDto.RelatedToId);

                dbRelationship.Owner = owner;
                dbRelationship.RelatedTo = relatedTo;

                context.Relationships.Update(dbRelationship);
                await context.SaveChangesAsync();

                return Ok(dbRelationship);
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
            var dbRelationship = await context
                .Relationships
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Relationships.Remove(dbRelationship);
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
