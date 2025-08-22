namespace ApiAppLeon.Models.Desarrollo
{
    public class FileGenerationRequest
    {
        public string Workfolder { get; set; }
        public string NameController { get; set; }
        public string NameModel { get; set; }
        public string NameProcedure { get; set; }
        public string Fields { get; set; }  // Campos Necesarios "fieldname type(size), ..."
        public string Params { get; set; }  // Parametros Necesarios "paramname type(size), ..."
        public int DbModel { get; set; }    // Indicates whether a DB model should be created (1: Yes, 0: No)
        public string NameDeveloper { get; set; }
        public string Comments { get; set; }
    }
    /// <summary>
    /// <c>Esta clase contiene la variable y declaración .Keyless necesarios en DBContext.cs para almacenar los objetos enviados a la base de datos y los obtenidos de la misma
    /// Variable: establece la virtualización del modelo que almacenará los datos provenientes de la base de datos 
    /// Funcion: De no tener un id (int) la consulta devuelta deberá incluirse en la función lineas abajo para indicar que la consulta será almacenada sin id en el modelo (objeto)</c>
    /// Navegador: Declaración necesaria para redirigir a la subcarpeta 
    /// </summary>
    public class DbContextFormat
    {
        public string? Variable { get; set; }
        public string? Funcion { get; set; }
        public string? Navegador { get; set; }
    }
    // The response model for the API responses
    public class Respuesta<T>
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public T Data { get; set; }
    }

    public class ErrorTxA
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
    }
}
