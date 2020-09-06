using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleBancario.Migrations
{
    public partial class SetUsernameUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "TbUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 6, 13, 22, 34, 354, DateTimeKind.Utc).AddTicks(1684),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 9, 6, 13, 12, 16, 69, DateTimeKind.Utc).AddTicks(4458));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TbUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 6, 13, 22, 34, 353, DateTimeKind.Utc).AddTicks(7600),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 9, 6, 13, 12, 16, 68, DateTimeKind.Utc).AddTicks(5208));

            migrationBuilder.CreateIndex(
                name: "IX_TbUsers_UserName",
                table: "TbUsers",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbUsers_UserName",
                table: "TbUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "TbUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 6, 13, 12, 16, 69, DateTimeKind.Utc).AddTicks(4458),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 9, 6, 13, 22, 34, 354, DateTimeKind.Utc).AddTicks(1684));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TbUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 6, 13, 12, 16, 68, DateTimeKind.Utc).AddTicks(5208),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 9, 6, 13, 22, 34, 353, DateTimeKind.Utc).AddTicks(7600));
        }
    }
}
