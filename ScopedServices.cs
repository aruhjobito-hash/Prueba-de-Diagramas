using LeonXIIICore.Controllers.Configuracion;
using LeonXIIICore.Controllers.Contabilidad;
using LeonXIIICore.Controllers.Sistema;

namespace LeonXIIICore
{
    public static class ScopedServiceRegistry
    {
        public static List<Type> Services => new()
        {
            typeof(PlanContableClass),
            typeof(OperacionClass),
            typeof(JSInteropHelper),
            typeof(MenuService),
            typeof(UserClaimsService),
            typeof(AgenciaController),
            typeof(CentroCostoController),
            typeof(AreasController),
            typeof(CargosController),
            typeof(PensionesController)
            // Agrega aquí todos los nuevos servicios creados para que el scoped pueda añadirlos al builder
        };
    }

}
