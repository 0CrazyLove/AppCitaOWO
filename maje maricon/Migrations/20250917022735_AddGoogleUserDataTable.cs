using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maje_maricon.Migrations
{
    /// <inheritdoc />
    public partial class AddGoogleUserDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleUserSub",
                table: "AppCitaDB",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GoogleAuthUsers",
                columns: table => new
                {
                    Sub = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Iss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Azp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aud = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GivenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exp = table.Column<long>(type: "bigint", nullable: false),
                    GoogleToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleAuthUsers", x => x.Sub);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCitaDB_GoogleUserSub",
                table: "AppCitaDB",
                column: "GoogleUserSub",
                unique: true,
                filter: "[GoogleUserSub] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCitaDB_GoogleAuthUsers_GoogleUserSub",
                table: "AppCitaDB",
                column: "GoogleUserSub",
                principalTable: "GoogleAuthUsers",
                principalColumn: "Sub");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCitaDB_GoogleAuthUsers_GoogleUserSub",
                table: "AppCitaDB");

            migrationBuilder.DropTable(
                name: "GoogleAuthUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppCitaDB_GoogleUserSub",
                table: "AppCitaDB");

            migrationBuilder.DropColumn(
                name: "GoogleUserSub",
                table: "AppCitaDB");
        }
    }
}
