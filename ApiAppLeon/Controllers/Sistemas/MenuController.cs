
// Developer    : josara  
// DateCreate   : 11/03/2025
// Description  : Controlador para mantenedor de menus(web y windows)
using Microsoft.AspNetCore.Mvc;
using ApiAppLeon.Models;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ApiAppLeon.Controllers.Sistemas
{
    [ApiExplorerSettings(GroupName = "Sistemas")]
    [Route("api/Sistemas/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public  MenuController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        readonly List<MenuClass>  Menu_ = new();
        readonly List<SubMenuClass> SubMenu_ = new();
        readonly List<DesgloseClass> Desglose_ = new();
        // POST: api/Menu
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MenuRoot>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        [HttpPost]
        public async Task<ActionResult<Respuesta<requestMenuModel>>> PostMenu([FromBody] requestMenuModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "Invalid Model State", Data = new ErrorTxA { codigo = "01", Mensaje = "Model state validation failed" } });
            }

            try
            {

                Menu_.Clear();
                
                
                MenuClass Menu = new();
                SubMenuClass SubMenu= new();
                DesgloseClass Desglose = new();

                var parameterNames = new[]
{
                    "Id", "Perfil", "TipoWin", "TipoWeb", "IdUser"
                };

                // 2. Build the parameter array using reflection
                var parameters = parameterNames.Select(name => new SqlParameter("@" + name, model.GetType().GetProperty(name)?.GetValue(model) ?? DBNull.Value)).ToArray();

                // 3. Build the stored procedure call string
                string storeMenu = $"[Seguridad].[sp_MantPerfil] {string.Join(", ", parameterNames.Select(n => "@" + n))}";

                //};
                List<MenuDBModel> MenuDB = await _dbContext.MenuDB.FromSqlRaw(storeMenu, parameters).ToListAsync();

                if (MenuDB != null && MenuDB.Count > 0)
                {
                    List<MenuClass> MenuRes = new();
                    List<MenuRoot> menuRoots = new();
                    foreach (var item in MenuDB)
                    {
                        // Search if the Menu already exists
                        //var existingMenu = MenuRes.FirstOrDefault(m => m.Menu == item.Menu);
                        var existingMenuRoot = menuRoots.FirstOrDefault(m => m.label == item.Menu);
                        // If Menu does not exist, create it and add it to MenuRes
                        //if (existingMenu == null)
                        //{
                        //    existingMenu = new MenuClass
                        //    {
                        //        Menu = item.Menu,
                        //        SubMenu = new List<SubMenuClass>()
                        //    };
                            
                        //    MenuRes.Add(existingMenu);
                        //}                         
                        if (existingMenuRoot == null)
                        {
                            existingMenuRoot = new MenuRoot
                            {
                                icon = item.MenuIcono,
                                label = item.Menu,
                                route = item.FuncionMenu,
                                subItems = new List<SubItem>()
                            };
                            menuRoots.Add(existingMenuRoot);
                        }

                        // Now, inside this Menu, check if SubMenu exists
                        //var existingSubMenu = existingMenu.SubMenu.FirstOrDefault(s => s.NombreSubMenu == item.NombreSubMenu);
                        var existingSubMenuRoot = existingMenuRoot.subItems.FirstOrDefault(s => s.label == item.NombreSubMenu);
                        // If SubMenu does not exist, create it
                        //if (existingSubMenu == null)
                        //{
                        //    existingSubMenu = new SubMenuClass
                        //    {
                        //        NombreSubMenu = item.NombreSubMenu,
                        //        Tipo = item.Tipo,
                        //        Desglose = new List<DesgloseClass>(),
                        //        Funcion = item.Tipo == "SubMenu" ? item.FuncionMenu : "" // Funcion only if SubMenu type
                        //    };
                        //    existingMenu.SubMenu.Add(existingSubMenu);
                        //}
                        //else 
                        if (existingSubMenuRoot == null)
                        {
                            existingSubMenuRoot = new SubItem
                            {
                                icon = item.SubMenuIcono,
                                label = item.NombreSubMenu,
                                route = item.FuncionMenu,                                
                                subItems = new List<SubItem>()
                            };
                            existingMenuRoot.subItems.Add(existingSubMenuRoot);
                        }


                        // If it's a Menu type (it will have desglose), add Desglose items if not already present
                        //if (item.Tipo == "Menu" && !string.IsNullOrEmpty(item.Name) && !string.IsNullOrEmpty(item.FuncionSubMenu))
                        //{
                        //    // Avoid duplicates in Desglose
                        //    bool desgloseExists = existingSubMenu.Desglose.Any(d => d.Name == item.Name && d.Funcion == item.FuncionSubMenu);
                        //    if (!desgloseExists)
                        //    {
                        //        existingSubMenu.Desglose.Add(new DesgloseClass
                        //        {
                        //            Name = item.Name,
                        //            Funcion = item.FuncionSubMenu
                        //        });
                        //    }
                        //}
                         
                        if (!string.IsNullOrEmpty(item.FuncionSubMenu))
                            {
                                // Avoid duplicates in SubItem
                                bool subItemExists = existingSubMenuRoot.subItems.Any(d => d.label == item.Name && d.route == item.FuncionSubMenu);
                                if (!subItemExists)
                                {
                                    existingSubMenuRoot.subItems.Add(new SubItem
                                    {
                                        icon = item.NameIcono,
                                        label = item.Name,
                                        route = item.FuncionSubMenu,
                                        subItems = new List<SubItem>()
                                    });
                                }
                        }
                    }

                    return Ok(menuRoots);

                }
                else
                {
                    return NotFound(new Respuesta<ErrorTxA> { Exito = 0, Mensaje = "No products found", Data = new ErrorTxA { codigo = "02", Mensaje = "No data returned from DB" } });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // PUT: api/Menu/{id}        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestMenuModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> PutMenu(int id, [FromBody] requestMenuModel model)
        {
            try
            {
                string storeMenu = "sp_Menu";
                var updateResult = await _dbContext.MenuDB.FromSqlRaw(storeMenu, model).ToListAsync();
                if (updateResult.Count == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // GET: api/Menu
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestMenuModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<ActionResult<IEnumerable<requestMenuModel>>> GetMenuModel()
        {
            try
            {
                string storeMenu = "sp_Menu";
                var result = await _dbContext.MenuDB.FromSqlRaw(storeMenu).ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

        // DELETE: api/Menu/{id}
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<requestMenuModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Respuesta<ErrorTxA>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<ErrorTxA>))]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            try
            {
                string storeMenu = "sp_Menu";
                var deleteResult = await _dbContext.MenuDB.FromSqlRaw(storeMenu, id).ToListAsync();
                if (deleteResult.Count == 0)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Respuesta<ErrorTxA> { Exito = 0, Mensaje = ex.Message, Data = new ErrorTxA { codigo = "03", Mensaje = "Internal Server Error" } });
            }
        }

    }
}