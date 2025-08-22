
// Developer: JosAra 12/08/2025 - Endpoints para listar y obtener planillas
// DateCreate   : 12/08/2025

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Recursos_Humanos
{
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá el endpoint en el controlador: PLanillaBoleta 
    /// </summary>
    public class requestPlanillaBoletaModel
    {
        public Int32? Id { get; set; }       
          
    }
    /// <summary>
    /// Esta clase contiene el modelo (objeto) que recibirá los datos del store procedure: sp_PLanillaBoleta 
    /// </summary>
    [Keyless]
    public class PlanillaBoletaDBModel
    {      

        public int iplamenid { get; set; }                    
        public string tipo { get; set; }            
        public string cplamenperiodo { get; set; }            
        public string ANIO { get; set; }            
        public string MES { get; set; }
    }

    [Keyless]
    public class DetPlanillaBoletaDBModel
    {

        public int iplamenid { get; set; }
        public string tipo { get; set; }
        public string cplamenperiodo { get; set; }
        public string ANIO { get; set; }
        public string MES { get; set; }
        public string FirmaRH { get; set; } 
        public string FirmaGG {get;set;}
    }

    [Keyless]
    public class ImprimeBoletaDBModel
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Cargo {get; set;}
        public string Situacion {get; set;}
        public string cPensNombre {get; set;}
        public string DocumentoIdentidad {get; set;}
        public string Categoria {get; set;}
        public string ctrabPensCUSSP {get; set;}
        public decimal DiasLaborados {get; set; }
        public decimal TotalIngresos {get; set;}
        public decimal TotalDscto {get; set;}
        public decimal TotalAPortes {get; set;}
        public decimal NetoPagar {get; set; }
        public string cConNombre {get; set;}
        public decimal nplamenconmonto {get; set;}
        public string Correo { get; set; }
        public string fechaIngreso { get; set; }
        public string tipo { get; set; } // Puede ser 'INGRESO' o 'DESCUENTO'
    }

    public class ImprimeBolDBModel
    {
        public string Nombres { get; set; }
        public string Cargo { get; set; }
        public string Situacion { get; set; }
        public string cPensNombre { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Categoria { get; set; }
        public string ctrabPensCUSSP { get; set; }
        public decimal DiasLaborados { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal TotalDscto { get; set; }
        public decimal TotalAPortes { get; set; }
        public decimal NetoPagar { get; set; }
        public string Correo { get; set; }
        public string fechaIngreso { get; set; }
        public List<ListaConceptos> ListaConceptos { get; set; }
    }
    public class ListaConceptos
    {
        public string cConNombre { get; set; }
        public decimal nplamenconmonto { get; set; }
        public string tipo { get; set; }

    }

    // De requerir mas modelos o ser distintos agregar abajo de esta linea siguiendo la siguiente estructura
    // [Keyless]
    // public class NombreModel
    // {
    //    public int? Id { get; set; }
    //    /*Demás tipos de datos para el nuevo modelo*/
    // }
}