using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomUserCore.Migrations
{
    public partial class AutoPopulateCreatedAtDateInImageDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Thumbnail",
                table: "ImageDetail",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Original",
                table: "ImageDetail",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Thumbnail",
                table: "ImageDetail",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Original",
                table: "ImageDetail",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 512);
        }
    }
}
