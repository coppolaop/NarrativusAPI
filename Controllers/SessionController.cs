using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarrativusAPI.Data;
using NarrativusAPI.Models;

namespace NarrativusAPI.Controllers
{
    [ApiController]
    [Route("v1/sessions")]
    public class SessionController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var session = await context
                .Sessions
                .AsNoTracking()
                .ToListAsync();

            return Ok(session);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var session = await context
                .Sessions
                .AsNoTracking()
                .Include(s => s.Campaign)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(session);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await context.Sessions.AddAsync(session);
                await context.SaveChangesAsync();
                return Created("v1/sessions/{session.Id}", session);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] Session session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dbSession = await context
                .Sessions
                .FirstOrDefaultAsync(x => x.Id == session.Id);

            if (dbSession == null)
            {
                return NotFound();
            }

            dbSession.Number = session.Number;
            dbSession.Season = session.Season;
            dbSession.Description = session.Description;
            dbSession.Date = session.Date;
            dbSession.CampaignId = session.CampaignId;
            dbSession.Campaign = session.Campaign;
            dbSession.Appearances = session.Appearances;

            try
            {
                context.Sessions.Update(dbSession);
                await context.SaveChangesAsync();

                return Ok(dbSession);
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
            var dbSession = await context
                .Sessions
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Sessions.Remove(dbSession);
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
