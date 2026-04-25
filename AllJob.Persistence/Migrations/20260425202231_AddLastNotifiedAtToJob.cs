using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllJob.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLastNotifiedAtToJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastNotifiedAt",
                table: "Jobs",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastNotifiedAt",
                table: "Jobs");
        }
    }
}
