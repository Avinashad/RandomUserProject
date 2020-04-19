using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomUserCore.Migrations
{
    public partial class ChangeDeleteBehaviourToCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_User",
                table: "ImageDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_User",
                table: "ImageDetail",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_User",
                table: "ImageDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_User",
                table: "ImageDetail",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
