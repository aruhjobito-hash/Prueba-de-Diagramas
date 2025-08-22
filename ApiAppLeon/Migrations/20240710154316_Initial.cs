using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiAppLeon.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "PruebaCore",
            //    columns: table => new
            //    {
            //        //Id = table.Column<int>(type:"int", nullable: false),
            //        Tittle=table.Column<string>(type:"varchar(25)",nullable: true),
            //        Genre =table.Column<string>(type: "varchar(25)", nullable: true),   
            //        ReleaseDate = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PruebaCore", x => x.Id);
            //    }
            //    ) ;
        }
        // Comando para Inciar Migración con DBContext Reemplazado de PruebaCoreContext
        // Update-Database -Context DBContext   

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PruebaCore");
        }
    }
}
