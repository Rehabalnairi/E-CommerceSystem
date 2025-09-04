using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixRefreshTokensTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "UID",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "UId",
                table: "RefreshTokens",
                newName: "UID");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UID");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UID",
                table: "RefreshTokens",
                column: "UID",
                principalTable: "Users",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UID",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "UID",
                table: "RefreshTokens",
                newName: "UId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UID",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UId");

            migrationBuilder.AddColumn<int>(
                name: "UID",
                table: "RefreshTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UId",
                table: "RefreshTokens",
                column: "UId",
                principalTable: "Users",
                principalColumn: "UID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
