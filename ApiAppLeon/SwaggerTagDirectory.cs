namespace ApiAppLeon
{
    public class SwaggerGroupConfig
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class VersionSwagger
    {
        public string version { get; set; } = "ALPHA 1.0.09";
    }
    public class SwaggerConfig
    {
        public List<SwaggerGroupConfig> Groups { get; set; }
        // Constructor para generar los espacios de trabajo por cada endpoint definido
        public SwaggerConfig()
        {
            Groups = new List<SwaggerGroupConfig>
        {
            new SwaggerGroupConfig { Name = "Operaciones", Description = "Carpeta para endpoints de Operaciones" },
            new SwaggerGroupConfig { Name = "Sistemas", Description = "Carpeta para endpoints de Sistemas" },
            new SwaggerGroupConfig { Name = "Negocios", Description = "Carpeta para endpoints de Negocios" },
            new SwaggerGroupConfig { Name = "Contabilidad", Description = "Carpeta para endpoints de Contabilidad" },
            new SwaggerGroupConfig { Name = "Kasnet", Description = "Carpeta para endpoints de Kasnet" },
            new SwaggerGroupConfig { Name = "Desarrollo", Description = "Carpeta para endpoints de Desarrollo" },
            new SwaggerGroupConfig { Name = "Utilitarios", Description = "Carpeta para endpoints de Utilitarios" },
            new SwaggerGroupConfig { Name = "Recursos_Humanos", Description = "Carpeta para endpoints de Recursos Humanos" },
            new SwaggerGroupConfig { Name = "Parametros", Description = "Carpeta para endpoints de Parametros" },
            new SwaggerGroupConfig { Name = "Logistica", Description = "Carpeta para endpoints de Logistica" },
            new SwaggerGroupConfig { Name = "Planeamiento", Description = "Carpeta para endpoints de Planeamiento" },
            new SwaggerGroupConfig { Name = "Finanzas", Description = "Carpeta para endpoints de Finanzas" },
            new SwaggerGroupConfig { Name = "Configuracion", Description = "Carpeta para endpoints de Configuracion" },
            new SwaggerGroupConfig { Name = "Reportes_Anexos_SBS", Description = "Carpeta para endpoints de Reportes y Anexos de la SBS" }
            // new SwaggerGroupConfig { Name = "F", Description = "Carpeta para endpoints de fucniones " },
        };
        }
    }


}
