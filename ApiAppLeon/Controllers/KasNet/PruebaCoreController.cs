using ApiAppLeon.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ApiAppLeon.Models.Sistemas;

namespace ApiAppLeon.Controllers.KasNet
{
    [ApiExplorerSettings(GroupName = "Kasnet")]
    //[Authorize]
    [Route("/api/kasnet/[controller]")]
    [ApiController]
    public class PruebaCoreController : ControllerBase
    {
        private readonly PruebaCoreContext _dbContext;
        public PruebaCoreController(PruebaCoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        private async Task<ActionResult<IEnumerable<PruebaCore>>> GetPruebaCore()
        {
            if (_dbContext.PruebaCore == null)
            {
                return NotFound();
            }
            return await _dbContext.PruebaCore.ToListAsync();

        }
        [HttpGet("{dni}")]
        //public async Task<ActionResult<PruebaCore>> GetPruebaCore(int id)
        public async Task<ActionResult<IEnumerable<PruebaCore>>> GetPruebaCore(string dni)
        {
            if (_dbContext.PruebaCore == null)
            {
                return NotFound();
            }
            string StoreProc = "exec sp_GetPagosxDNI " + "@DNI=" + dni;

            //var pruebaCore = 
            //var pruebaCore = await _dbContext.PruebaCore.FromSqlRaw(StoreProc).ToArrayAsync();
            List<PruebaCore> PruebaS_;
            PruebaS_ = await _dbContext.PruebaCore.FromSqlRaw(StoreProc).ToListAsync();
            //PruebaS_.GetEnumerator();
            return await _dbContext.PruebaCore.FromSqlRaw(StoreProc).ToArrayAsync();
            //if (pruebaCore == null)
            //{
            //    return NotFound();
            //}
            //return pruebaCore;
        }
        [HttpPost]
        private async Task<ActionResult<PruebaCore>> PostPruebaCore(PruebaCore pruebaCore)
        {
            _dbContext.PruebaCore.Add(pruebaCore);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPruebaCore), new { id = pruebaCore.Id }, pruebaCore);
        }
        [HttpPut("{id}")]
        private async Task<IActionResult> PutPruebaCore(int id, PruebaCore pruebaCore)
        {
            if (id != pruebaCore.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(pruebaCore).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PruebaCoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        private bool PruebaCoreExists(long id)
        {
            return (_dbContext.PruebaCore?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
