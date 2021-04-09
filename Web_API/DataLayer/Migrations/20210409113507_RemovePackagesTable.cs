using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class RemovePackagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Speeds_Packages_PackageID",
                table: "Speeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_Packages_PackageID",
                table: "Times");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Times_PackageID",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Speeds_PackageID",
                table: "Speeds");

            migrationBuilder.AlterColumn<int>(
                name: "PackageID",
                table: "Times",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PackageID",
                table: "Speeds",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PackageID",
                table: "Times",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PackageID",
                table: "Speeds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Times_PackageID",
                table: "Times",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Speeds_PackageID",
                table: "Speeds",
                column: "PackageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Speeds_Packages_PackageID",
                table: "Speeds",
                column: "PackageID",
                principalTable: "Packages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_Packages_PackageID",
                table: "Times",
                column: "PackageID",
                principalTable: "Packages",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
