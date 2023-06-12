using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EchangeProfesseursEtudiantsApp.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Idelement = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGroup = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Nameelement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriptionelement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coefficientelement = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Idelement);
                    table.ForeignKey(
                        name: "FK_Elements_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Elements_Modules_IdGroup",
                        column: x => x.IdGroup,
                        principalTable: "Modules",
                        principalColumn: "Idmodule",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Elements_IdGroup",
                table: "Elements",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_IdUser",
                table: "Elements",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Elements");
        }
    }
}
