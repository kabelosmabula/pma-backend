using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMA.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class corrections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doctorid",
                table: "Consultations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Doctorid",
                table: "Consultations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
