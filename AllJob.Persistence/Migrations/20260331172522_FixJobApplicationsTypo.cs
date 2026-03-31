using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllJob.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixJobApplicationsTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoApplications_Jobs_JobId",
                table: "JoApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JoApplications_Users_UserId",
                table: "JoApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoApplications",
                table: "JoApplications");

            migrationBuilder.RenameTable(
                name: "JoApplications",
                newName: "JobApplications");

            migrationBuilder.RenameIndex(
                name: "IX_JoApplications_UserId",
                table: "JobApplications",
                newName: "IX_JobApplications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_JoApplications_JobId",
                table: "JobApplications",
                newName: "IX_JobApplications_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobApplications",
                table: "JobApplications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Jobs_JobId",
                table: "JobApplications",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Users_UserId",
                table: "JobApplications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Jobs_JobId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Users_UserId",
                table: "JobApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobApplications",
                table: "JobApplications");

            migrationBuilder.RenameTable(
                name: "JobApplications",
                newName: "JoApplications");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_UserId",
                table: "JoApplications",
                newName: "IX_JoApplications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_JobApplications_JobId",
                table: "JoApplications",
                newName: "IX_JoApplications_JobId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoApplications",
                table: "JoApplications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JoApplications_Jobs_JobId",
                table: "JoApplications",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JoApplications_Users_UserId",
                table: "JoApplications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
