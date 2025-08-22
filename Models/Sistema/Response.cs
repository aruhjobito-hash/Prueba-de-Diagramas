namespace LeonXIIICore.Models.Sistema
{
    public class Respuesta<T>
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public T Data { get; set; }
    }
    

    /// <summary>
    /// Esta clase contiene el código de error y Mensaje de error originado.
    /// </summary>
    public class ErrorTxA
    {
        public string? codigo { get; set; }
        public string? Mensaje { get; set; }
    }
}
