using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleBancario.Migrations
{
    public partial class PasswordAlterColumnLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "TbUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 6, 13, 12, 16, 69, DateTimeKind.Utc).AddTicks(4458),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 9, 1, 0, 35, 42, 30, DateTimeKind.Utc).AddTicks(5394));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "TbUsers",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TbUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 6, 13, 12, 16, 68, DateTimeKind.Utc).AddTicks(5208),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 9, 1, 0, 35, 42, 29, DateTimeKind.Utc).AddTicks(7461));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "TbUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 1, 0, 35, 42, 30, DateTimeKind.Utc).AddTicks(5394),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 9, 6, 13, 12, 16, 69, DateTimeKind.Utc).AddTicks(4458));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "TbUsers",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TbUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 1, 0, 35, 42, 29, DateTimeKind.Utc).AddTicks(7461),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 9, 6, 13, 12, 16, 68, DateTimeKind.Utc).AddTicks(5208));
        }
    }
}
