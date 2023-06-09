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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
