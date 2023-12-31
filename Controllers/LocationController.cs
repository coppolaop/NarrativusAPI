﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarrativusAPI.Data;
using NarrativusAPI.Models;

namespace NarrativusAPI.Controllers
{
    [ApiController]
    [Route("v1/locations")]
    public class LocationController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var location = await context
                .Locations
                .AsNoTracking()
                .ToListAsync();

            return Ok(location);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var location = await context
                .Locations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (location.Id.Equals(location.LocatedInId))
            {
                return BadRequest("Location could not be located inside itself");
            }

            try
            {
                await context.Locations.AddAsync(location);
                await context.SaveChangesAsync();
                return Created("v1/locations/{location.Id}", location);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (location.Id.Equals(location.LocatedInId))
            {
                return BadRequest("Location could not be located inside itself");
            }

            var dbLocation = await context
                .Locations
                .FirstOrDefaultAsync(x => x.Id == location.Id);

            if (dbLocation == null)
            {
                return NotFound();
            }

            dbLocation.Name = location.Name;
            dbLocation.Type = location.Type;
            dbLocation.Description = location.Description;
            dbLocation.FoundationDate = location.FoundationDate;
            dbLocation.LocatedInId = location.LocatedInId;
            dbLocation.Appearances = location.Appearances;

            try
            {
                context.Locations.Update(dbLocation);
                await context.SaveChangesAsync();

                return Ok(dbLocation);
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
            var dbLocation = await context
                .Locations
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Locations.Remove(dbLocation);
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
