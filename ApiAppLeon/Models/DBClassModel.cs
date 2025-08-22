using Microsoft.EntityFrameworkCore;

namespace ApiAppLeon.Models
{
    [KeylessAttribute]
    public class DBClassModel
    {
     

    }
    [Keyless]
    public class Agencias
    {
        public string? idagencia { get; set; }
    }
}
