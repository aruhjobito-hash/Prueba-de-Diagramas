using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Common;
using System;
using Microsoft.Extensions.Hosting;
using static ApiAppLeon.Models.Utilitarios.JsonFacturacion;
using ApiAppLeon.Models.Sistemas;
using ApiAppLeon.Models.Utilitarios;

namespace ApiAppLeon.Controllers.Utilitarios
{
    [ApiExplorerSettings(GroupName = "Utilitarios")]
    [Route("api/Utilitarios/Registro/[controller]")]
    [ApiController]
    public class RegistraPersonaController : ControllerBase
    {
        public readonly DBContext _dbContext;
        public RegistraPersonaController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public static Conexion.BDSiaf _BDSIAF { get; set; } = new BDSiaf();
        private static List<PersonaModel> Persona_ { get; set; } = new List<PersonaModel>();
        private static List<PersonaResponse> PersonaResponse_ { get; set; } = new List<PersonaResponse>();
        private static List<ConsultaPersonaRes> Personas { get; set; } = new List<ConsultaPersonaRes>();
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Respuesta<ConsultaPersonaRes>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Respuesta<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Respuesta<object>))]
        public async Task<ActionResult<ConsultaPersonaRes>> Post(PersonaModel personaModel)
        {

            await Task.Delay(1000);
            Persona_.Clear();
            if (_dbContext.PersonaResponse == null)
            {
                return NotFound();
            }

            //Store para consulta
            string StorePersona = "exec registra_Persona " +
                                    " @Direccion='" + personaModel.Direccion + "'," +
                                    " @cReferencia='" + personaModel.cReferencia + "'," +
                                    " @IdUbigeoDir='" + personaModel.IdUbigeoDir + "'," +
                                    " @Telefono2='" + personaModel.Telefono2 + "'," +
                                    " @Telefono='" + personaModel.Telefono + "'," +
                                    " @IngMensual=" + personaModel.IngMensual + "," +
                                    " @TipPersona='" + personaModel.TipPersona + "'," +
                                    " @IdTipViv='" + personaModel.IdTipViv + "'," +
                                    " @Email='" + personaModel.Email + "'," +
                                    " @IdMedioDif='" + personaModel.IdMedioDif + "'," +
                                    " @Requisitoriado='" + personaModel.Requisitoriado + "'," +
                                    " @FechaNac='" + personaModel.FechaNac + "'," +
                                    " @IdUbigeoNac='" + personaModel.IdUbigeoDir + "'," +
                                    " @IdUrbe='" + personaModel.IdUrbe + "'," +
                                    " @idUser='" + personaModel.IdUser + "'," +
                                    " @Fecpro='" + personaModel.Fecpro + "'," +
                                    " @Hora='" + personaModel.Hora + "'," +
                                    " @FechaReg='" + personaModel.FechaReg + "'," +
                                    " @ApePat='" + personaModel.ApePat + "'," +
                                    " @ApeMat='" + personaModel.ApeMat + "'," +
                                    " @Nombres='" + personaModel.Nombres + "'," +
                                    " @Sexo='" + personaModel.Sexo + "'," +
                                    " @IdTipDocId='" + personaModel.IdTipDocId + "'," +
                                    " @NroDocId='" + personaModel.NroDocId + "'," +
                                    " @IdOcupacion='" + personaModel.IdOcupacion + "'," +
                                    " @IdEstCivil='" + personaModel.IdEstCivil + "'," +
                                    " @IdGradInst='" + personaModel.IdGrandInst + "'," +
                                    " @IdProfesion='" + personaModel.IdProfesion + "'," +
                                    " @ApeCas='" + personaModel.ApeCas + "'";

            //List<PersonaResponse> personaResponse_;
            List<ConsultaPersonaRes> personaModel1 = new List<ConsultaPersonaRes>();
            personaModel1 = await _dbContext.PersonaResponse.FromSqlRaw(StorePersona).ToListAsync();
            int band = 0;
            foreach (var item in personaModel1)
            {
                if (item.Estado == "REGISTRO EXITOSO")
                {
                    band = 1;
                }
                else if (item.Estado == "EXISTE REGISTRO")
                {
                    band = 2;
                }
                else
                {
                    band = 0;
                }
            }

            if (band == 1)
            {
                return CreatedAtAction(nameof(Post), personaModel1);
            }
            else if (band == 2)
            {
                return BadRequest();
            }
            else
            {
                return NotFound();
            }

        }
    }
}
