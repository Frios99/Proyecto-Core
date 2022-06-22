using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCore_Francis.Migrations
{
    public partial class PalabrasClaveToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "palabrasClaves",
                columns: table => new
                {
                    PalabrasClaveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PalabrasClaves = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_palabrasClaves", x => x.PalabrasClaveID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "palabrasClaves");
        }
    }
}
