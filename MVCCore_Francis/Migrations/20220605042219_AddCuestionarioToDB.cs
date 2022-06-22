using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCore_Francis.Migrations
{
    public partial class AddCuestionarioToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuestionarios",
                columns: table => new
                {
                    CuestionarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pregunta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opcion1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opcion2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opcion3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opcion4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Puntaje1 = table.Column<int>(type: "int", nullable: false),
                    Puntaje2 = table.Column<int>(type: "int", nullable: false),
                    Puntaje3 = table.Column<int>(type: "int", nullable: false),
                    Puntaje4 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuestionarios", x => x.CuestionarioID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuestionarios");
        }
    }
}
