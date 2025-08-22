using ApiAppLeon.Models.Sistemas;
using ApiAppLeon.Models.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Controllers.Utilitarios
{
    [ApiExplorerSettings(GroupName = "Utilitarios")]
    [Route("api/Utilitarios/[controller]")]
    [ApiController]
    public class ListarProductoSimpleController : ControllerBase
    {
        public readonly DBContext _dbContext;
        public ListarProductoSimpleController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public static List<ListarProductosBD> ListarProductoBD_ { get; set; } = new List<ListarProductosBD>();
        public static List<ListarProductoDet> ListarProductoDet_ { get; set; } = new List<ListarProductoDet>();
        public static List<ListarProductoDet> ListarProductoRes_ { get; set; } = new List<ListarProductoDet>();
        public static List<DestinoProducto> ListarDestino_ { get; set; } = new List<DestinoProducto>();
        public static List<TipoCreSBS> tipoCreSBs_ { get; set; } = new List<TipoCreSBS>();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<ListarProductosBD>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        public async Task<ActionResult<ListarProductosBD>> PostListarProtucto()
        {
            //Persona_.Clear();
            try
            {
                if (_dbContext.PersonaResponse == null)
                {
                    return NotFound();
                }
                //Store para consulta
                string StorePersona = "select*from sp_ListarProductosDestino";
                List<ListarProductosBD> producto = new List<ListarProductosBD>();
                producto = await _dbContext.ListarProductosBDs.FromSqlRaw(StorePersona).ToListAsync();
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
                    return CreatedAtAction(nameof(PostListarProtucto), producto);
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
