using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMA.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentRelationships_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the existing foreign key constraint
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Practitioners_PractitionerId",
                table: "Appointments");

            // Step 2: Add PracticePractitioner records for existing Practitioners
            // Note: Since Practitioner doesn't have PracticeId, we need to get it from other sources
            // For now, we'll set a default PracticeId or get it from PracticePractitioner if it exists
            migrationBuilder.Sql(@"
            INSERT INTO ""PracticePractitioners"" (""id"", ""PracticeId"", ""PractitionerId"", ""Specialty"", ""PracticeEmail"", ""Isactive"", ""StartDate"", ""EndDate"", ""createddate"", ""createdby"", ""isdeleted"")
            SELECT 
                gen_random_uuid() as ""id"",
                pp_existing.""PracticeId"" as ""PracticeId"",  -- Use existing PracticeId if available
                p.""id"" as ""PractitionerId"",
                '' as ""Specialty"",
                '' as ""PracticeEmail"",
                true as ""Isactive"",
                NOW() as ""StartDate"",
                NULL as ""EndDate"",
                NOW() as ""createddate"",
                'System' as ""createdby"",
                false as ""isdeleted""
            FROM ""Practitioners"" p
            LEFT JOIN ""PracticePractitioners"" pp_existing ON pp_existing.""PractitionerId"" = p.""id""
            WHERE NOT EXISTS (
                SELECT 1 FROM ""PracticePractitioners"" pp 
                WHERE pp.""PractitionerId"" = p.""id""
            )
            AND p.""isdeleted"" = false
        ");

            // Step 3: Update Appointments to link to PracticePractitioners
            // Find the PracticePractitioner that matches the PractitionerId
            migrationBuilder.Sql(@"
            UPDATE ""Appointments""
            SET ""PractitionerId"" = pp.""id""
            FROM ""PracticePractitioners"" pp
            WHERE ""Appointments"".""PractitionerId"" = pp.""PractitionerId""
        ");

            // Step 4: Add the new foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PracticePractitioners_PractitionerId",
                table: "Appointments",
                column: "PractitionerId",
                principalTable: "PracticePractitioners",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PracticePractitioners_PractitionerId",
                table: "Appointments");

            // Restore the old foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Practitioners_PractitionerId",
                table: "Appointments",
                column: "PractitionerId",
                principalTable: "Practitioners",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
