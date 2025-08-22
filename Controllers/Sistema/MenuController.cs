using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Clases.Sistema;
using LeonXIIICore.Models.Sistema;
using System.Data;
using System.Security.Claims;

namespace LeonXIIICore.Controllers.Sistema
{
    public class MenuController : Controller
    {
        [HttpPost("/Menu")]
        public async Task<IActionResult> CargaMenu()
        {
            var apiLeon = new APILEON();

            apiLeon = new APILEON();

            // Pasamos los datos y construimos el objeto del request()
            var menuRequest = new requestMenuModel
            {
                Id = 1,
                Perfil = "pTI_Jefatura",
                TipoWin = "0",
                TipoWeb = "1",
                IdUser = "JosAra"
            };

            // Utilizamos la función APILEONPOST (Url , object) y señalamos que lo esperado es una lista List<> del modelo MenuClass
            var menuResponse = await apiLeon.APILEONPOST<List<MenuClass>>("/api/Sistemas/Menu", menuRequest);

            if (menuResponse != null)
            {
                ViewBag.MenuData = menuResponse;
            }
            return View();
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
