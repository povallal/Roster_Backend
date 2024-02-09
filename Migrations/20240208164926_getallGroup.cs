using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rosterapi.Migrations
{
    /// <inheritdoc />
    public partial class getallGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Units_UnitId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Units_UnitId",
                table: "Groups",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Units_UnitId",
                table: "Groups");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Units_UnitId",
                table: "Groups",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
