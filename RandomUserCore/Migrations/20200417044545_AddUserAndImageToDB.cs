using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomUserCore.Migrations
{
    public partial class AddUserAndImageToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 512, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Original = table.Column<string>(maxLength: 50, nullable: false),
                    Thumbnail = table.Column<string>(maxLength: 50, nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageDetail_UserId",
                table: "ImageDetail",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__EmailAddress",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageDetail");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
