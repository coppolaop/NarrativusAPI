using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NarrativusAPI.Data;
using NarrativusAPI.Models;

namespace NarrativusAPI.Controllers
{
    [ApiController]
    [Route("v1/campaigns")]
    public class CampaignController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var campaign = await context
                .Campaigns
                .AsNoTracking()
                .ToListAsync();

            return Ok(campaign);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Campaign>> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var campaign = await context
                .Campaigns
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return Ok(campaign);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] Campaign campaign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await context.Campaigns.AddAsync(campaign);
                await context.SaveChangesAsync();
                return Created("v1/campaigns/{campaign.Id}", campaign);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] Campaign campaign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dbCampaign = await context
                .Campaigns
                .FirstOrDefaultAsync(x => x.Id == campaign.Id);

            if (dbCampaign == null)
            {
                return NotFound();
            }

            dbCampaign.Name = campaign.Name;
            dbCampaign.Description = campaign.Description;
            dbCampaign.Sessions = campaign.Sessions;
            dbCampaign.Stars = campaign.Stars;

            try
            {
                context.Campaigns.Update(dbCampaign);
                await context.SaveChangesAsync();

                return Ok(dbCampaign);
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
            var dbCampaign = await context
                .Campaigns
                .FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Campaigns.Remove(dbCampaign);
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
