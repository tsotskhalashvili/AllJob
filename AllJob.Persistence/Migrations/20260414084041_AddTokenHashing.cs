using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllJob.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenHashing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Token",
                table: "RefreshTokens",
                newName: "TokenHash");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_Token",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_TokenHash");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "PasswordResetTokens",
                newName: "TokenHash");

            migrationBuilder.RenameIndex(
                name: "IX_PasswordResetTokens_Token",
                table: "PasswordResetTokens",
                newName: "IX_PasswordResetTokens_TokenHash");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "AdminInvites",
                newName: "TokenHash");

            migrationBuilder.RenameIndex(
                name: "IX_AdminInvites_Token",
                table: "AdminInvites",
                newName: "IX_AdminInvites_TokenHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TokenHash",
                table: "RefreshTokens",
                newName: "Token");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_TokenHash",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_Token");

            migrationBuilder.RenameColumn(
                name: "TokenHash",
                table: "PasswordResetTokens",
                newName: "Token");

            migrationBuilder.RenameIndex(
                name: "IX_PasswordResetTokens_TokenHash",
                table: "PasswordResetTokens",
                newName: "IX_PasswordResetTokens_Token");

            migrationBuilder.RenameColumn(
                name: "TokenHash",
                table: "AdminInvites",
                newName: "Token");

            migrationBuilder.RenameIndex(
                name: "IX_AdminInvites_TokenHash",
                table: "AdminInvites",
                newName: "IX_AdminInvites_Token");
        }
    }
}
