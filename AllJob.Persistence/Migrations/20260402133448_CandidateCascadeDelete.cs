using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllJob.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CandidateCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducations_CandidateProfiles_CandidateProfileId",
                table: "CandidateEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExperiences_CandidateProfiles_CandidateProfileId",
                table: "CandidateExperiences");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateEducations_CandidateProfiles_CandidateProfileId",
                table: "CandidateEducations",
                column: "CandidateProfileId",
                principalTable: "CandidateProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExperiences_CandidateProfiles_CandidateProfileId",
                table: "CandidateExperiences",
                column: "CandidateProfileId",
                principalTable: "CandidateProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducations_CandidateProfiles_CandidateProfileId",
                table: "CandidateEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExperiences_CandidateProfiles_CandidateProfileId",
                table: "CandidateExperiences");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateEducations_CandidateProfiles_CandidateProfileId",
                table: "CandidateEducations",
                column: "CandidateProfileId",
                principalTable: "CandidateProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExperiences_CandidateProfiles_CandidateProfileId",
                table: "CandidateExperiences",
                column: "CandidateProfileId",
                principalTable: "CandidateProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
