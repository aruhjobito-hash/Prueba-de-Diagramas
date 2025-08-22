using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models.Utilitarios
{
#pragma warning disable IDE1006 // JosAra 18/07/2024 Deshabilita las sugerencias de Estilos de nombres para Modelos

    public class PersonaModel
    {
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
    public class PersonaResponse
    {
        public string? Estado { get; set; }
        public string? IdPersona { get; set; }
    }
    public class ConsultaPersona
    {
        public string? DNI { get; set; }
    }
    public partial class ConsultaPersonaResponse
    {
        public string? IdPersona { get; set; }
        public string? Nombre { get; set; }
        public string? Celular { get; set; }
    }
    [Keyless]
    public partial class ConsultaPersonaRes
    {
        public string? Estado { get; set; }
        public string? IdPersona { get; set; }
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
        public string? IdGradInst { get; set; }
        public string? IdProfesion { get; set; }
        public string? ApeCas { get; set; }
    }
}
