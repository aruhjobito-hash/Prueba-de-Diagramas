using ApiAppLeon.Models.Sistemas;
using ApiAppLeon.Models.Utilitarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiAppLeon.Controllers.Utilitarios
{
    [ApiExplorerSettings(GroupName = "Utilitarios")]
    [Authorize]
    [Route("api/Utilitarios/[controller]")]
    [ApiController]
    public class ListarProductoConvenioController : ControllerBase
    {

        public readonly DBContext _dbContext;
        public ListarProductoConvenioController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<ListarProductosBD>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        public async Task<ActionResult<paListarConveniosBD>> PostListarProtuctoConvenio()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Returns the validation errors
            }
            //Persona_.Clear();
            try
            {
                ErrorTxA errorTxA = new ErrorTxA();
                if (_dbContext.PersonaResponse == null)
                {
                    return NotFound();
                }
                //Store para consulta
                string StorePersona = "select*from sp_ListarProductosConvenio";
                List<paListarConveniosBD> producto = new List<paListarConveniosBD>();
                producto = await _dbContext.PaListarConveniosBDs.FromSqlRaw(StorePersona).ToListAsync();
                int band = 0;
                foreach (var item in producto)
                {
                    if (item.iddenocre != "")
                    {
                        band++;
                    }
                    else
                    {
                        band = 0;
                    }
                }
                if (band != 0)
                {
                    return CreatedAtAction(nameof(PostListarProtuctoConvenio), producto);
                }
                else
                {
                    return NotFound(producto);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Data + ex.Message);
            }
        }
    }    
}
