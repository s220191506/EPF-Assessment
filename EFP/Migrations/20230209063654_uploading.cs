using Microsoft.EntityFrameworkCore.Migrations;

namespace EFP.Migrations
{
    public partial class uploading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Uploads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Uploads_UserId",
                table: "Uploads",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uploads_Customers_UserId",
                table: "Uploads",
                column: "UserId",
                principalTable: "Customers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uploads_Customers_UserId",
                table: "Uploads");

            migrationBuilder.DropIndex(
                name: "IX_Uploads_UserId",
                table: "Uploads");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Uploads");
        }
    }
}
