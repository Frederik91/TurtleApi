using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurtleApi.Migrations
{
    /// <inheritdoc />
    public partial class RelationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Programs_TurtleProgramId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_TurtleProgramId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "TurtleProgramId",
                table: "Steps");

            migrationBuilder.AddColumn<int>(
                name: "ProgramId1",
                table: "Steps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TurtleId1",
                table: "Programs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Steps_ProgramId1",
                table: "Steps",
                column: "ProgramId1");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_TurtleId1",
                table: "Programs",
                column: "TurtleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Turtles_TurtleId1",
                table: "Programs",
                column: "TurtleId1",
                principalTable: "Turtles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Programs_ProgramId1",
                table: "Steps",
                column: "ProgramId1",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Turtles_TurtleId1",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Programs_ProgramId1",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_ProgramId1",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Programs_TurtleId1",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ProgramId1",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "TurtleId1",
                table: "Programs");

            migrationBuilder.AddColumn<int>(
                name: "TurtleProgramId",
                table: "Steps",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Steps_TurtleProgramId",
                table: "Steps",
                column: "TurtleProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Programs_TurtleProgramId",
                table: "Steps",
                column: "TurtleProgramId",
                principalTable: "Programs",
                principalColumn: "Id");
        }
    }
}
