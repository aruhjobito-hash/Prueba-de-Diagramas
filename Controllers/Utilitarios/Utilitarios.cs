using Microsoft.AspNetCore.Mvc;
using System.Text;
using LeonXIIICore.Models.Reportes_Anexos_SBS;
using Clases.Sistema;
using LeonXIIICore.Models.Sistema;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System.IO;
using OfficeOpenXml;
using System.Drawing;

namespace LeonXIIICore.Controllers.Utilitarios
{
    public class Utilitarios
    {
        public static string MesEnLetras(string Mes)
        {
            string codigoMes;
            switch (Mes.ToLower())
            {
                case "01": codigoMes = "ENERO"; break;
                case "02": codigoMes = "FEBRERO"; break;
                case "03": codigoMes = "MARZO"; break;
                case "04": codigoMes = "ABRIL"; break;
                case "05": codigoMes = "MAYO"; break;
                case "06": codigoMes = "JUNIO"; break;
                case "07": codigoMes = "JULIO"; break;
                case "08": codigoMes = "AGOSTO"; break;
                case "09": codigoMes = "SEPTIEMBRE"; break;
                case "10": codigoMes = "OCTUBRE"; break;
                case "11": codigoMes = "NOVIEMBRE"; break;
                case "12": codigoMes = "DICIEMBRE"; break;
                default: codigoMes = "??"; break;
            }

            return codigoMes;
        }

        public static string MesEnNúmeros(string Mes)
        {
            string codigoMes;
            switch (Mes.ToLower())
            {
                case "enero": codigoMes = "01"; break;
                case "febrero": codigoMes = "02"; break;
                case "marzo": codigoMes = "03"; break;
                case "abril": codigoMes = "04"; break;
                case "mayo": codigoMes = "05"; break;
                case "junio": codigoMes = "06"; break;
                case "julio": codigoMes = "07"; break;
                case "agosto": codigoMes = "08"; break;
                case "septiembre": codigoMes = "09"; break;
                case "octubre": codigoMes = "10"; break;
                case "noviembre": codigoMes = "11"; break;
                case "diciembre": codigoMes = "12"; break;
                default: codigoMes = "??"; break;
            }

            return codigoMes;
        }

        public static int DaysInMonth(int year, int month)
        {
            if (month < 1 || month > 12)
            {
                throw new ArgumentOutOfRangeException("month", "El mes debe estar entre 1 y 12.");
            }

            int[] daysToMonth365 = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };
            int[] daysToMonth366 = { 0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366 };

            bool isLeapYear = DateTime.IsLeapYear(year);
            int[] daysArray = isLeapYear ? daysToMonth366 : daysToMonth365;

            return daysArray[month] - daysArray[month - 1];
        }

    }
}
