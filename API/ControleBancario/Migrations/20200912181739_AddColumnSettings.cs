using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ControleBancario.Migrations
{
    public partial class AddColumnSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "TbUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 12, 18, 17, 38, 598, DateTimeKind.Utc).AddTicks(2113),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 9, 6, 13, 22, 34, 354, DateTimeKind.Utc).AddTicks(1684));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TbUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 12, 18, 17, 38, 598, DateTimeKind.Utc).AddTicks(1718),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValue: new DateTime(2020, 9, 6, 13, 22, 34, 353, DateTimeKind.Utc).AddTicks(7600));

            migrationBuilder.AddColumn<int>(
                name: "SettingsForeignKey",
                table: "TbUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TbSettings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false, defaultValue: false),
                    IsManager = table.Column<bool>(nullable: false, defaultValue: false),
                    IsCreateUser = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSettings", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbUsers_SettingsForeignKey",
                table: "TbUsers",
                column: "SettingsForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_TbUsers_TbSettings_SettingsForeignKey",
                table: "TbUsers",
                column: "SettingsForeignKey",
                principalTable: "TbSettings",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbUsers_TbSettings_SettingsForeignKey",
                table: "TbUsers");

            migrationBuilder.DropTable(
                name: "TbSettings");

            migrationBuilder.DropIndex(
                name: "IX_TbUsers_SettingsForeignKey",
                table: "TbUsers");

            migrationBuilder.DropColumn(
                name: "SettingsForeignKey",
                table: "TbUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "TbUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 6, 13, 22, 34, 354, DateTimeKind.Utc).AddTicks(1684),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 9, 12, 18, 17, 38, 598, DateTimeKind.Utc).AddTicks(2113));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TbUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(2020, 9, 6, 13, 22, 34, 353, DateTimeKind.Utc).AddTicks(7600),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 9, 12, 18, 17, 38, 598, DateTimeKind.Utc).AddTicks(1718));
        }
    }
}
