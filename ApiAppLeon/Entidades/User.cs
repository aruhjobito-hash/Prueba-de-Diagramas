namespace ApiAppLeon.Entidades
{
    public partial class User
    {
        public int? Id { get; set; } 
        public string? Name { get; set; }    
#pragma warning disable IDE1006 // Estilos de nombres
        public string? username { get; set; }    
#pragma warning restore IDE1006 // Estilos de nombres
        public string? password { get; set; }    
        public DateTime? EnrollmentDate { get; set; }

    }
}
