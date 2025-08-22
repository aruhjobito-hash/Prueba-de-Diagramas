
// Developer: JosAra 08/07/2025 - Endpoints para registro de trabajador
// DateCreate   : 08/07/2025

using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using ApiAppLeon.Models.Utilitarios;
using ApiAppLeon.Controllers.Utilitarios;
namespace ApiAppLeon.Models.Recursos_Humanos
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: RegistroTrabajador 
    /// </summary>
    public class requestRegistroTrabajadorModel
    {
        public string? IdPersona { get; set; }

        [Required(ErrorMessage = "El campo Direccion es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo Direccion no puede tener más de 50 caracteres")]
        public string Direccion { get; set; }            
        [Required(ErrorMessage = "El campo cReferencia es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo cReferencia no puede tener más de 50 caracteres")]
        public string cReferencia { get; set; }
            
        [Required(ErrorMessage = "El campo IdUbigeoDir es obligatorio")]
        [MaxLength(8, ErrorMessage = "El campo IdUbigeoDir no puede tener más de 8 caracteres")]
        public string IdUbigeoDir { get; set; }
            
        [MaxLength(12, ErrorMessage = "El campo Telefono2 no puede tener más de 12 caracteres")]
        public string Telefono2 { get; set; }
            
        [Required(ErrorMessage = "El campo Telefono es obligatorio")]
        [MaxLength(12, ErrorMessage = "El campo Telefono no puede tener más de 12 caracteres")]
        public string Telefono { get; set; }
            
        public decimal IngMensual { get; set; }
            
        [Required(ErrorMessage = "El campo TipPersona es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo TipPersona no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo TipPersona debe tener al menos 1 caracteres")]
        public string TipPersona { get; set; }
            
        [Required(ErrorMessage = "El campo IdTipViv es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo IdTipViv no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo IdTipViv debe tener al menos 4 caracteres")]
        public string IdTipViv { get; set; }
            
        [Required(ErrorMessage = "El campo Email es obligatorio")]
        [MaxLength(50, ErrorMessage = "El campo Email no puede tener más de 50 caracteres")]
        public string Email { get; set; }
            
        [Required(ErrorMessage = "El campo IdMedioDif es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo IdMedioDif no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo IdMedioDif debe tener al menos 4 caracteres")]
        public string IdMedioDif { get; set; }

            
        [Required(ErrorMessage = "El campo Requisitoriado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Requisitoriado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Requisitoriado debe tener al menos 1 caracteres")]
        public string Requisitoriado { get; set; }

            
        [Required(ErrorMessage = "El campo FechaNac es obligatorio")]
        [MaxLength(10, ErrorMessage = "El campo FechaNac no puede tener más de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo FechaNac debe tener al menos 10 caracteres")]
        public string FechaNac { get; set; }

            
        [Required(ErrorMessage = "El campo IdUbigeoNac es obligatorio")]
        [MaxLength(8, ErrorMessage = "El campo IdUbigeoNac no puede tener más de 8 caracteres")]
        [MinLength(8, ErrorMessage = "El campo IdUbigeoNac debe tener al menos 8 caracteres")]
        public string IdUbigeoNac { get; set; }

            
        [Required(ErrorMessage = "El campo IdUrbe es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo IdUrbe no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo IdUrbe debe tener al menos 4 caracteres")]
        public string IdUrbe { get; set; }

            
        [Required(ErrorMessage = "El campo idUser es obligatorio")]
        [MaxLength(6, ErrorMessage = "El campo idUser no puede tener más de 6 caracteres")]
        [MinLength(6, ErrorMessage = "El campo idUser debe tener al menos 6 caracteres")]
        public string idUser { get; set; }

            
        [Required(ErrorMessage = "El campo Fecpro es obligatorio")]
        [MaxLength(10, ErrorMessage = "El campo Fecpro no puede tener más de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo Fecpro debe tener al menos 10 caracteres")]
        public string Fecpro { get; set; }

            
        [Required(ErrorMessage = "El campo Hora es obligatorio")]
        [MaxLength(8, ErrorMessage = "El campo Hora no puede tener más de 8 caracteres")]
        [MinLength(8, ErrorMessage = "El campo Hora debe tener al menos 8 caracteres")]
        public string Hora { get; set; }

            
        [Required(ErrorMessage = "El campo FechaReg es obligatorio")]
        [MaxLength(10, ErrorMessage = "El campo FechaReg no puede tener más de 10 caracteres")]
        [MinLength(10, ErrorMessage = "El campo FechaReg debe tener al menos 10 caracteres")]
        public string FechaReg { get; set; }

            
        [Required(ErrorMessage = "El campo ApePat es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo ApePat no puede tener más de 20 caracteres")]
        public string ApePat { get; set; }

            
        [Required(ErrorMessage = "El campo ApeMat es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo ApeMat no puede tener más de 20 caracteres")]
        public string ApeMat { get; set; }

            
        [Required(ErrorMessage = "El campo Nombres es obligatorio")]
        [MaxLength(30, ErrorMessage = "El campo Nombres no puede tener más de 30 caracteres")]
        public string Nombres { get; set; }

            
        [Required(ErrorMessage = "El campo Sexo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo Sexo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo Sexo debe tener al menos 1 caracteres")]
        public string Sexo { get; set; }

            
        [Required(ErrorMessage = "El campo IdTipDocId es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo IdTipDocId no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo IdTipDocId debe tener al menos 4 caracteres")]
        public string IdTipDocId { get; set; }

            
        public string NroDocId { get; set; }

            
        public string IdOcupacion { get; set; }

            
        public string IdEstCivil { get; set; }

            
        public string IdGradInst { get; set; }

            
        public string IdProfesion { get; set; }

            
        public string ApeCas { get; set; }

        [Required(ErrorMessage = "El campo Nacionalidad es obligatorio")]
        [MaxLength(25, ErrorMessage = "El campo Nacionalidad no puede tener más de 25 caracteres")]
        public string Nacionalidad { get; set; }

        [Required(ErrorMessage = "El campo CarnetSeguro es obligatorio")]
        [MaxLength(20, ErrorMessage = "El campo CarnetSeguro no puede tener más de 20 caracteres")]
        public string CarnetSeguro { get; set; }

            
        [Required(ErrorMessage = "El campo IdPension es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdPension no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdPension debe tener al menos 2 caracteres")]
        public string IdPension { get; set; }

            
        [Required(ErrorMessage = "El campo IdCtaPension es obligatorio")]
        [MaxLength(25, ErrorMessage = "El campo IdCtaPension no puede tener más de 25 caracteres")]
        public string IdCtaPension { get; set; }

        [Required(ErrorMessage = "El campo FechaInscripcion es obligatorio")]
        [MaxLength(10, ErrorMessage = "El campo FechaInscripcion no puede tener más de 10 caracteres")]
        public string FechaInscripcion { get; set; }

            
        [Required(ErrorMessage = "El campo iCalculo es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo iCalculo no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo iCalculo debe tener al menos 1 caracteres")]
        public string iCalculo { get; set; }

            
        [Required(ErrorMessage = "El campo iDeclararPDT es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo iDeclararPDT no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo iDeclararPDT debe tener al menos 1 caracteres")]
        public string iDeclararPDT { get; set; }

            
        [Required(ErrorMessage = "El campo iAfectoQuinta es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo iAfectoQuinta no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo iAfectoQuinta debe tener al menos 1 caracteres")]
        public string iAfectoQuinta { get; set; }

            
        [Required(ErrorMessage = "El campo cInformacionDomicilio es obligatorio")]
        [MaxLength(250, ErrorMessage = "El campo cInformacionDomicilio no puede tener más de 250 caracteres")]
        public string cInformacionDomicilio { get; set; }

        public string cImagen { get; set; }
        public string fechaIngreso { get; set; }

        [Required(ErrorMessage = "El campo cIdTipTrabajador es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo cIdTipTrabajador no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo cIdTipTrabajador debe tener al menos 2 caracteres")]
        public string cIdTipTrabajador { get; set; }

        [Required(ErrorMessage = "El campo CentroCosto es obligatorio")]
        [MaxLength(25, ErrorMessage = "El campo CentroCosto no puede tener más de 25 caracteres")]
        public string CentroCosto { get; set; }

        [Required(ErrorMessage = "El campo IdAgencia es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdAgencia no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdAgencia debe tener al menos 2 caracteres")]
        public string IdAgencia { get; set; }

        [Required(ErrorMessage = "El campo cIdTipTrabajador es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdArea no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdArea debe tener al menos 2 caracteres")]
        public string IdArea { get; set; }

        [Required(ErrorMessage = "El campo cIdTipTrabajador es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo IdCargo no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo IdCargo debe tener al menos 2 caracteres")]
        public string IdCargo { get; set; }

        [Required(ErrorMessage = "El campo cIdRegLaboral es obligatorio")]
        [MaxLength(3, ErrorMessage = "El campo cIdRegLaboral no puede tener más de 3 caracteres")]
        [MinLength(3, ErrorMessage = "El campo cIdRegLaboral debe tener al menos 3 caracteres")]
        public string cIdRegLaboral { get; set; }

            
        [Required(ErrorMessage = "El campo cIdTipContrato es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo cIdTipContrato no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo cIdTipContrato debe tener al menos 2 caracteres")]
        public string cIdTipContrato { get; set; }

        [Required(ErrorMessage = "El campo dFechaTerminoContrato es obligatorio")]
        [MaxLength(10, ErrorMessage = "El campo dFechaTerminoContrato no puede tener más de 10 caracteres")]
        public string dFechaTerminoContrato { get; set; }

            
        [Required(ErrorMessage = "El campo cSItuacion es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo cSItuacion no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo cSItuacion debe tener al menos 4 caracteres")]
        public string cSItuacion { get; set; }

            
        [Required(ErrorMessage = "El campo RUC_EPS es obligatorio")]
        [MaxLength(14, ErrorMessage = "El campo RUC_EPS no puede tener más de 14 caracteres")]
        public string RUC_EPS { get; set; }
        [Required(ErrorMessage = "El campo dFechaCese es obligatorio")]
        public string dFechaCese { get; set; }
        [Required(ErrorMessage = "El campo mIngresoBasico es obligatorio")]
        public float mIngresoBasico { get; set; }
        public float mAsigFamiliar { get; set; }
            
        [Required(ErrorMessage = "El campo tipMoneda es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo tipMoneda no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo tipMoneda debe tener al menos 1 caracteres")]
        public string tipMoneda { get; set; }

            
        [Required(ErrorMessage = "El campo ctaBancariaHaberes es obligatorio")]
        [MaxLength(25, ErrorMessage = "El campo ctaBancariaHaberes no puede tener más de 25 caracteres")]
        public string ctaBancariaHaberes { get; set; }

            
        [Required(ErrorMessage = "El campo cIdBancoHaberes es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo cIdBancoHaberes no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo cIdBancoHaberes debe tener al menos 2 caracteres")]
        public string cIdBancoHaberes { get; set; }

            
        [Required(ErrorMessage = "El campo ctaBancariaCTS es obligatorio")]
        [MaxLength(25, ErrorMessage = "El campo ctaBancariaCTS no puede tener más de 25 caracteres")]
        public string ctaBancariaCTS { get; set; }

            
        [Required(ErrorMessage = "El campo cIdBancoCTS es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo cIdBancoCTS no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo cIdBancoCTS debe tener al menos 2 caracteres")]
        public string cIdBancoCTS { get; set; }

            
        [Required(ErrorMessage = "El campo cidSitEsp es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo cidSitEsp no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo cidSitEsp debe tener al menos 2 caracteres")]
        public string cidSitEsp { get; set; }

            
        [Required(ErrorMessage = "El campo cPeriodo es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo cPeriodo no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo cPeriodo debe tener al menos 2 caracteres")]
        public string cPeriodo { get; set; }

            
        [Required(ErrorMessage = "El campo cTipoPago es obligatorio")]
        [MaxLength(2, ErrorMessage = "El campo cTipoPago no puede tener más de 2 caracteres")]
        [MinLength(2, ErrorMessage = "El campo cTipoPago debe tener al menos 2 caracteres")]
        public string cTipoPago { get; set; }

            
        [Required(ErrorMessage = "El campo bTrabRegAlt es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bTrabRegAlt no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bTrabRegAlt debe tener al menos 1 caracteres")]
        public string bTrabRegAlt { get; set; }

            
        [Required(ErrorMessage = "El campo bTrabJorMax es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bTrabJorMax no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bTrabJorMax debe tener al menos 1 caracteres")]
        public string bTrabJorMax { get; set; }

            
        [Required(ErrorMessage = "El campo bTrabHorNoc es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bTrabHorNoc no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bTrabHorNoc debe tener al menos 1 caracteres")]
        public string bTrabHorNoc { get; set; }

            
        [Required(ErrorMessage = "El campo cCentroRiesgo es obligatorio")]
        [MaxLength(25, ErrorMessage = "El campo cCentroRiesgo no puede tener más de 25 caracteres")]
        public string cCentroRiesgo { get; set; }

            
        [Required(ErrorMessage = "El campo SCTRSalud es obligatorio")]
        [MaxLength(25, ErrorMessage = "El campo SCTRSalud no puede tener más de 25 caracteres")]
        public string SCTRSalud { get; set; }

            
        [Required(ErrorMessage = "El campo SCTRPension es obligatorio")]
        [MaxLength(25, ErrorMessage = "El campo SCTRPension no puede tener más de 25 caracteres")]
        public string SCTRPension { get; set; }

            
        public decimal mOtrosIngresos { get; set; }

            
        [Required(ErrorMessage = "El campo bSindicalizado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bSindicalizado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bSindicalizado debe tener al menos 1 caracteres")]
        public string bSindicalizado { get; set; }

            
        [Required(ErrorMessage = "El campo bDiscapacitado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bDiscapacitado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bDiscapacitado debe tener al menos 1 caracteres")]
        public string bDiscapacitado { get; set; }

            
        [Required(ErrorMessage = "El campo bDomiciliado es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bDomiciliado no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bDomiciliado debe tener al menos 1 caracteres")]
        public string bDomiciliado { get; set; }

            
        [Required(ErrorMessage = "El campo bEsSaludVida es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bEsSaludVida no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bEsSaludVida debe tener al menos 1 caracteres")]
        public string bEsSaludVida { get; set; }

            
        [Required(ErrorMessage = "El campo bAfiliaSeguroPension es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bAfiliaSeguroPension no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bAfiliaSeguroPension debe tener al menos 1 caracteres")]
        public string bAfiliaSeguroPension { get; set; }

            
        [Required(ErrorMessage = "El campo bRentaExonerada es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo bRentaExonerada no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo bRentaExonerada debe tener al menos 1 caracteres")]
        public string bRentaExonerada { get; set; }

            
        [Required(ErrorMessage = "El campo BackOffice es obligatorio")]
        [MaxLength(1, ErrorMessage = "El campo BackOffice no puede tener más de 1 caracteres")]
        [MinLength(1, ErrorMessage = "El campo BackOffice debe tener al menos 1 caracteres")]
        public string BackOffice { get; set; }
        [Required(ErrorMessage = "El campo FormaMarcado es obligatorio")]
        [MaxLength(4, ErrorMessage = "El campo FormaMarcado no puede tener más de 4 caracteres")]
        [MinLength(4, ErrorMessage = "El campo FormaMarcado debe tener al menos 4 caracteres")]
        public string FormaMarcado { get; set; }
        public Int32 Opt { get; set; } // Bandera para indicar si es un registro nuevo o actualización
        public List<PersonaFamModel>? ListaFamiliares { get; set; } // Lista de personas a registrar
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_RegistroTrabajador 
    /// </summary>
    [Keyless]
    public class RegistroTrabajadorDBModel
    {      
        public string? Estado { get; set; }
        public string? Direccion { get; set; }
        public string? cReferencia { get; set; }
        public string? IdUbigeoDir { get; set; }
        public string? Telefono2 { get; set; }
        public string? Telefono { get; set; }
        public decimal? IngMensual { get; set; }
        public string? TipPersona { get; set; }
        public string? IdTipViv { get; set; }
        public string? Email { get; set; }
        public string? IdMedioDif { get; set; }
        public string? Requisitoriado { get; set; }
        public string? FechaNac { get; set; }
        public string? IdUbigeoNac { get; set; }
        public string? IdUrbe { get; set; }
        public string? idUser { get; set; }
        public string? Fecpro { get; set; }
        public string? Hora { get; set; }
        public string? FechaReg { get; set; }
        public string? ApePat { get; set; }
        public string? ApeMat { get; set; }
        public string? Nombres { get; set; }
        public string? Sexo { get; set; }
        public string? IdTipDocId { get; set; }     
        public string? NroDocId { get; set; }
        public string? IdOcupacion { get; set; }
        public string? IdEstCivil { get; set; }
        public string? IdGradInst { get; set; }
        public string? IdProfesion { get; set; }
        public string? ApeCas { get; set; }
        public string? IdPersona { get; set; }
        public string? Nacionalidad { get; set; }
        public string? CarnetSeguro { get; set; }
        public string? IdPension { get; set; }
        public string? IdCtaPension { get; set; }
        public DateTime? FechaInscripcion { get; set; }
        public string? iCalculo { get; set; }
        public string? iDeclararPDT { get; set; }
        public string? iAfectoQuinta { get; set; }
        public string? cInformacionDomicilio { get; set; }
        public string? cImagen { get; set; }
        public string? cIdTipTrabajador { get; set; }
        public string? cIdRegLaboral { get; set; }
        public string? cIdTipContrato { get; set; }
        public DateTime? dFechaTerminoContrato { get; set; }
        public string? cSItuacion { get; set; }
        public string? RUC_EPS { get; set; }
        public decimal? mIngresoBasico { get; set; }
        public decimal? mAsigFamiliar { get; set; }
        public string? tipMoneda { get; set; }
        public string? ctaBancariaHaberes { get; set; }
        public string? cIdBancoHaberes { get; set; }
        public string? ctaBancariaCTS { get; set; }
        public string? cIdBancoCTS { get; set; }
        public string? cidSitEsp { get; set; }
        public string? cPeriodo { get; set; }
        public string? cTipoPago { get; set; }
        public string? bTrabRegAlt { get; set; }
        public string? bTrabJorMax { get; set; }
        public string? bTrabHorNoc { get; set; }
        public string? cCentroRiesgo { get; set; }
        public string? SCTRSalud { get; set; }
        public string? SCTRPension { get; set; }
        public decimal? mOtrosIngresos { get; set; }
        public string? bSindicalizado { get; set; }
        public string? bDiscapacitado { get; set; }
        public string? bDomiciliado { get; set; }
        public string? bEsSaludVida { get; set; }
        public string? bAfiliaSeguroPension { get; set; }
        public string? bRentaExonerada { get; set; }
        public string? BackOffice { get; set; }
        public string? FormaMarcado { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? IdAgencia { get; set; }
        public string? IdCargo { get; set; }
        public string? IdArea { get; set; }
        public DateTime? dFechaCese { get; set; }

        public List<PersonaFamModel>? ListaFamiliares { get; set; } // Lista de personas a registrar
    }
    [Keyless]
    public class ListarCombosRegistraPersonaBD
    {
        public Int32? IdLista { get; set; }
        public string? NombreLista { get; set; }
        public string? Id { get; set; }
        public string? Des { get; set; }
    }
    public class ListaCombosRegistraPersona
    {
        public Int32? IdLista { get; set; }
        public string? NombreLista { get; set; }
        public List<ListaCombos> List { get;set; }
    }
    public class ListaCombos
    {
        public string Id { get; set; }
        public string Des { get; set; }
    }
    public class PersonaFamModel
    {
        public string? IdParentesco { get; set; }
        public string? Direccion { get; set; }
        public string? cReferencia { get; set; }
        public string? IdUbigeoDir { get; set; }
        public string? Telefono2 { get; set; }
        public string? Telefono { get; set; }
        public decimal? IngMensual { get; set; }
        public string? TipPersona { get; set; }
        public string? IdTipViv { get; set; }
        public string? Email { get; set; }
        public string? IdMedioDif { get; set; }
        public string? Requisitoriado { get; set; }
        public string? FechaNac { get; set; }
        public string? IdUbigeoNac { get; set; }
        public string? IdUrbe { get; set; }
        public string? IdUser { get; set; }
        public string? Fecpro { get; set; }
        public string? Hora { get; set; }
        public string? FechaReg { get; set; }
        public string? ApePat { get; set; }
        public string? ApeMat { get; set; }
        public string? Nombres { get; set; }
        public string? Sexo { get; set; }
        public string? IdTipDocId { get; set; }
        public string? NroDocId { get; set; }
        public string? IdOcupacion { get; set; }
        public string? IdEstCivil { get; set; }
        public string? IdGrandInst { get; set; }
        public string? IdProfesion { get; set; }
        public string? ApeCas { get; set; }
    }
    public class PersonaFamTrab
    {
        public string IdPersonaFam { get; set; }
        public string IdPersonaTrab { get; set; }
    }
    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}