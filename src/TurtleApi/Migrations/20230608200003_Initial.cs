using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurtleApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Turtles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turtles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    TurtleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programs_Turtles_TurtleId",
                        column: x => x.TurtleId,
                        principalTable: "Turtles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    TurtleProgramId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Steps_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Steps_Programs_TurtleProgramId",
                        column: x => x.TurtleProgramId,
                        principalTable: "Programs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programs_TurtleId",
                table: "Programs",
                column: "TurtleId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_ProgramId",
                table: "Steps",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_State",
                table: "Steps",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_TurtleProgramId",
                table: "Steps",
                column: "TurtleProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Turtles");
        }
    }
}
