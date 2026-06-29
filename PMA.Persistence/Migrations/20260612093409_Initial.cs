using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMA.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organisations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    MedicalAidName = table.Column<string>(type: "text", nullable: false),
                    MedicalAidNumber = table.Column<string>(type: "text", nullable: false),
                    MedicalAidId = table.Column<Guid>(type: "uuid", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "practices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PracticeNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_practices", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Firstname = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    Displayname = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Isemailverified = table.Column<bool>(type: "boolean", nullable: false),
                    Istwofactorenabled = table.Column<bool>(type: "boolean", nullable: false),
                    Phonenumber = table.Column<string>(type: "text", nullable: true),
                    Isphonenumberverified = table.Column<bool>(type: "boolean", nullable: false),
                    Lastlogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Failedloginattempts = table.Column<int>(type: "integer", nullable: false),
                    Accountstatus = table.Column<string>(type: "text", nullable: false),
                    Passwordchangedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Lastotp = table.Column<string>(type: "text", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PatientHousehold",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicalAidId = table.Column<Guid>(type: "uuid", nullable: true),
                    HouseholdName = table.Column<string>(type: "text", nullable: false),
                    PrimaryContactName = table.Column<string>(type: "text", nullable: false),
                    PrimaryContactSurName = table.Column<string>(type: "text", nullable: false),
                    PrimaryContactPhone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientHousehold", x => x.id);
                    table.ForeignKey(
                        name: "FK_PatientHousehold_practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "practices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uuid", nullable: false),
                    HouseholdId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Identitynumber = table.Column<string>(type: "text", nullable: false),
                    Displayname = table.Column<string>(type: "text", nullable: false),
                    Phonenumber = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Dateofbirth = table.Column<DateOnly>(type: "date", nullable: true),
                    MedicalAidId = table.Column<Guid>(type: "uuid", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Organisationid = table.Column<Guid>(type: "uuid", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.id);
                    table.ForeignKey(
                        name: "FK_Patients_Organisations_Organisationid",
                        column: x => x.Organisationid,
                        principalTable: "Organisations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Patients_practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "practices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Practitioners",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    HPCSANumber = table.Column<string>(type: "text", nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    Yearsofexperience = table.Column<int>(type: "integer", nullable: false),
                    Licenseexpirydate = table.Column<DateOnly>(type: "date", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Practitioners", x => x.id);
                    table.ForeignKey(
                        name: "FK_Practitioners_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPractices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uuid", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPractices", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserPractices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPractices_practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "practices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Isactive = table.Column<bool>(type: "boolean", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uuid", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "practices",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    AllergyName = table.Column<string>(type: "text", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.id);
                    table.ForeignKey(
                        name: "FK_Allergies_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalRecords",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalRecords", x => x.id);
                    table.ForeignKey(
                        name: "FK_ClinicalRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicalRecords_practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "practices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalAids",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    SchemeCode = table.Column<string>(type: "text", nullable: false),
                    SchemeName = table.Column<string>(type: "text", nullable: false),
                    PlanCode = table.Column<string>(type: "text", nullable: false),
                    PlanName = table.Column<string>(type: "text", nullable: false),
                    MembershipNumber = table.Column<string>(type: "text", nullable: false),
                    DependentCode = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    EffectiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalAids", x => x.id);
                    table.ForeignKey(
                        name: "FK_MedicalAids_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientPatientHousehold",
                columns: table => new
                {
                    Householdsid = table.Column<Guid>(type: "uuid", nullable: false),
                    Patientsid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPatientHousehold", x => new { x.Householdsid, x.Patientsid });
                    table.ForeignKey(
                        name: "FK_PatientPatientHousehold_PatientHousehold_Householdsid",
                        column: x => x.Householdsid,
                        principalTable: "PatientHousehold",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientPatientHousehold_Patients_Patientsid",
                        column: x => x.Patientsid,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Appointmentreference = table.Column<string>(type: "text", nullable: false),
                    Appointmenttype = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    PractitionerId = table.Column<Guid>(type: "uuid", nullable: false),
                    HouseholdId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsFollowUp = table.Column<bool>(type: "boolean", nullable: false),
                    AppointmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    startappointment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    endappointment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Appointments_PatientHousehold_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "PatientHousehold",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Practitioners_PractitionerId",
                        column: x => x.PractitionerId,
                        principalTable: "Practitioners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "practices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PracticePractitioners",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PractitionerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Specialty = table.Column<string>(type: "text", nullable: false),
                    PracticeEmail = table.Column<string>(type: "text", nullable: false),
                    Isactive = table.Column<bool>(type: "boolean", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticePractitioners", x => x.id);
                    table.ForeignKey(
                        name: "FK_PracticePractitioners_Practitioners_PractitionerId",
                        column: x => x.PractitionerId,
                        principalTable: "Practitioners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracticePractitioners_practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "practices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consultations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClinicalRecordId = table.Column<Guid>(type: "uuid", nullable: false),
                    Doctorid = table.Column<Guid>(type: "uuid", nullable: false),
                    PracticePractitionerId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppointmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrganisationId = table.Column<Guid>(type: "uuid", nullable: true),
                    PatientId = table.Column<string>(type: "text", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConsultationNotes = table.Column<string>(type: "text", nullable: false),
                    SpecialtyTypes = table.Column<int>(type: "integer", nullable: false),
                    reasonforvisit = table.Column<string>(type: "text", nullable: false),
                    BillingType = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    RequiresFollowUp = table.Column<bool>(type: "boolean", nullable: false),
                    ParentConsultationId = table.Column<Guid>(type: "uuid", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Consultations_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Consultations_ClinicalRecords_ClinicalRecordId",
                        column: x => x.ClinicalRecordId,
                        principalTable: "ClinicalRecords",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultations_PracticePractitioners_PracticePractitionerId",
                        column: x => x.PracticePractitionerId,
                        principalTable: "PracticePractitioners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PractitionerAvailabilities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PracticePractitionerId = table.Column<Guid>(type: "uuid", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PractitionerAvailabilities", x => x.id);
                    table.ForeignKey(
                        name: "FK_PractitionerAvailabilities_PracticePractitioners_PracticePr~",
                        column: x => x.PracticePractitionerId,
                        principalTable: "PracticePractitioners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardiologyConsultations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ECGPerformed = table.Column<bool>(type: "boolean", nullable: false),
                    ECGFindings = table.Column<string>(type: "text", nullable: false),
                    EchoPerformed = table.Column<bool>(type: "boolean", nullable: false),
                    EjectionFraction = table.Column<int>(type: "integer", nullable: true),
                    SmokingRisk = table.Column<bool>(type: "boolean", nullable: false),
                    HypertensionRisk = table.Column<bool>(type: "boolean", nullable: false),
                    ConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardiologyConsultations", x => x.id);
                    table.ForeignKey(
                        name: "FK_CardiologyConsultations_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalDocuments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    documentName = table.Column<string>(type: "text", nullable: false),
                    documentType = table.Column<string>(type: "text", nullable: false),
                    fileUrl = table.Column<string>(type: "text", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalDocuments", x => x.id);
                    table.ForeignKey(
                        name: "FK_ClinicalDocuments_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DentistryConsultations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    XRayCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    GumCondition = table.Column<string>(type: "text", nullable: false),
                    ConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentistryConsultations", x => x.id);
                    table.ForeignKey(
                        name: "FK_DentistryConsultations_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DermatologyConsultations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    SkinCondition = table.Column<string>(type: "text", nullable: false),
                    Severity = table.Column<string>(type: "text", nullable: false),
                    AffectedAreas = table.Column<string>(type: "text", nullable: false),
                    ConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DermatologyConsultations", x => x.id);
                    table.ForeignKey(
                        name: "FK_DermatologyConsultations_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.id);
                    table.ForeignKey(
                        name: "FK_Diagnoses_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientMedicalAidId = table.Column<Guid>(type: "uuid", nullable: true),
                    ConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    OutStandingAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    HouseholdId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.id);
                    table.ForeignKey(
                        name: "FK_Invoices_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OncologyConsultations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    CancerType = table.Column<string>(type: "text", nullable: false),
                    CancerStage = table.Column<string>(type: "text", nullable: false),
                    ChemotherapyCycle = table.Column<int>(type: "integer", nullable: false),
                    TumorResponse = table.Column<string>(type: "text", nullable: false),
                    ConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OncologyConsultations", x => x.id);
                    table.ForeignKey(
                        name: "FK_OncologyConsultations_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientReferral",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    specialty = table.Column<string>(type: "text", nullable: false),
                    practice = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    reason = table.Column<string>(type: "text", nullable: false),
                    Consultationid = table.Column<Guid>(type: "uuid", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientReferral", x => x.id);
                    table.ForeignKey(
                        name: "FK_PatientReferral_Consultations_Consultationid",
                        column: x => x.Consultationid,
                        principalTable: "Consultations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PediatricsConsultations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    WeightKg = table.Column<decimal>(type: "numeric", nullable: false),
                    HeightCm = table.Column<decimal>(type: "numeric", nullable: false),
                    VaccinationsUpToDate = table.Column<bool>(type: "boolean", nullable: false),
                    DevelopmentalMilestones = table.Column<string>(type: "text", nullable: false),
                    ConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PediatricsConsultations", x => x.id);
                    table.ForeignKey(
                        name: "FK_PediatricsConsultations_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    consultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    medicationName = table.Column<string>(type: "text", nullable: false),
                    dosage = table.Column<string>(type: "text", nullable: false),
                    frequency = table.Column<string>(type: "text", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: false),
                    duration = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Consultations_consultationId",
                        column: x => x.consultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedures",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    tariffAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedures", x => x.id);
                    table.ForeignKey(
                        name: "FK_Procedures_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsychiatryConsultations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Mood = table.Column<string>(type: "text", nullable: false),
                    Affect = table.Column<string>(type: "text", nullable: false),
                    Speech = table.Column<string>(type: "text", nullable: false),
                    SuicidalIdeation = table.Column<bool>(type: "boolean", nullable: false),
                    SelfHarmRiskLevel = table.Column<string>(type: "text", nullable: false),
                    TherapyNotes = table.Column<string>(type: "text", nullable: false),
                    ConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychiatryConsultations", x => x.id);
                    table.ForeignKey(
                        name: "FK_PsychiatryConsultations_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vitals",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    consultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    oxygenSaturation = table.Column<int>(type: "integer", nullable: true),
                    respiratoryRate = table.Column<int>(type: "integer", nullable: true),
                    Temperature = table.Column<decimal>(type: "numeric", nullable: true),
                    Pulse = table.Column<int>(type: "integer", nullable: true),
                    SystolicBP = table.Column<int>(type: "integer", nullable: true),
                    DiastolicBP = table.Column<int>(type: "integer", nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: true),
                    height = table.Column<decimal>(type: "numeric", nullable: true),
                    BMI = table.Column<decimal>(type: "numeric", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitals", x => x.id);
                    table.ForeignKey(
                        name: "FK_Vitals_Consultations_consultationId",
                        column: x => x.consultationId,
                        principalTable: "Consultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToothCharts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ToothNumber = table.Column<int>(type: "integer", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    DentistryConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToothCharts", x => x.id);
                    table.ForeignKey(
                        name: "FK_ToothCharts_DentistryConsultations_DentistryConsultationId",
                        column: x => x.DentistryConsultationId,
                        principalTable: "DentistryConsultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalImages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CapturedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DermatologyConsultationId = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalImages", x => x.id);
                    table.ForeignKey(
                        name: "FK_ClinicalImages_DermatologyConsultations_DermatologyConsulta~",
                        column: x => x.DermatologyConsultationId,
                        principalTable: "DermatologyConsultations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseHoldInvoiceCollection",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    invoiceId = table.Column<int>(type: "integer", nullable: false),
                    Invoiceid = table.Column<Guid>(type: "uuid", nullable: false),
                    patientid = table.Column<int>(type: "integer", nullable: false),
                    Patientid = table.Column<Guid>(type: "uuid", nullable: false),
                    practiceid = table.Column<int>(type: "integer", nullable: false),
                    Practiceid = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientHouseholdid = table.Column<Guid>(type: "uuid", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseHoldInvoiceCollection", x => x.id);
                    table.ForeignKey(
                        name: "FK_HouseHoldInvoiceCollection_Invoices_Invoiceid",
                        column: x => x.Invoiceid,
                        principalTable: "Invoices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseHoldInvoiceCollection_PatientHousehold_PatientHousehol~",
                        column: x => x.PatientHouseholdid,
                        principalTable: "PatientHousehold",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_HouseHoldInvoiceCollection_Patients_Patientid",
                        column: x => x.Patientid,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseHoldInvoiceCollection_practices_Practiceid",
                        column: x => x.Practiceid,
                        principalTable: "practices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineItems",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferenceCode = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentMethod = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifieddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleteddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_Payment_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_PatientId",
                table: "Allergies",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_HouseholdId",
                table: "Appointments",
                column: "HouseholdId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PracticeId",
                table: "Appointments",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PractitionerId",
                table: "Appointments",
                column: "PractitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_CardiologyConsultations_ConsultationId",
                table: "CardiologyConsultations",
                column: "ConsultationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalDocuments_ConsultationId",
                table: "ClinicalDocuments",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalImages_DermatologyConsultationId",
                table: "ClinicalImages",
                column: "DermatologyConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalRecords_PatientId",
                table: "ClinicalRecords",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalRecords_PracticeId",
                table: "ClinicalRecords",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_AppointmentId",
                table: "Consultations",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_ClinicalRecordId",
                table: "Consultations",
                column: "ClinicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_PracticePractitionerId",
                table: "Consultations",
                column: "PracticePractitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_DentistryConsultations_ConsultationId",
                table: "DentistryConsultations",
                column: "ConsultationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DermatologyConsultations_ConsultationId",
                table: "DermatologyConsultations",
                column: "ConsultationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_ConsultationId",
                table: "Diagnoses",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseHoldInvoiceCollection_Invoiceid",
                table: "HouseHoldInvoiceCollection",
                column: "Invoiceid");

            migrationBuilder.CreateIndex(
                name: "IX_HouseHoldInvoiceCollection_PatientHouseholdid",
                table: "HouseHoldInvoiceCollection",
                column: "PatientHouseholdid");

            migrationBuilder.CreateIndex(
                name: "IX_HouseHoldInvoiceCollection_Patientid",
                table: "HouseHoldInvoiceCollection",
                column: "Patientid");

            migrationBuilder.CreateIndex(
                name: "IX_HouseHoldInvoiceCollection_Practiceid",
                table: "HouseHoldInvoiceCollection",
                column: "Practiceid");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItems_InvoiceId",
                table: "InvoiceLineItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ConsultationId",
                table: "Invoices",
                column: "ConsultationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalAids_PatientId",
                table: "MedicalAids",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_OncologyConsultations_ConsultationId",
                table: "OncologyConsultations",
                column: "ConsultationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientHousehold_PracticeId",
                table: "PatientHousehold",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPatientHousehold_Patientsid",
                table: "PatientPatientHousehold",
                column: "Patientsid");

            migrationBuilder.CreateIndex(
                name: "IX_PatientReferral_Consultationid",
                table: "PatientReferral",
                column: "Consultationid");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Organisationid",
                table: "Patients",
                column: "Organisationid");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PracticeId",
                table: "Patients",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_InvoiceId",
                table: "Payment",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PediatricsConsultations_ConsultationId",
                table: "PediatricsConsultations",
                column: "ConsultationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PracticePractitioners_PracticeId",
                table: "PracticePractitioners",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticePractitioners_PractitionerId",
                table: "PracticePractitioners",
                column: "PractitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_practices_PracticeNumber",
                table: "practices",
                column: "PracticeNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PractitionerAvailabilities_PracticePractitionerId",
                table: "PractitionerAvailabilities",
                column: "PracticePractitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_Practitioners_UserId",
                table: "Practitioners",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_consultationId",
                table: "Prescriptions",
                column: "consultationId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_ConsultationId",
                table: "Procedures",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychiatryConsultations_ConsultationId",
                table: "PsychiatryConsultations",
                column: "ConsultationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToothCharts_DentistryConsultationId",
                table: "ToothCharts",
                column: "DentistryConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPractices_PracticeId",
                table: "UserPractices",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPractices_UserId",
                table: "UserPractices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_PracticeId",
                table: "UserRoles",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Vitals_consultationId",
                table: "Vitals",
                column: "consultationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "CardiologyConsultations");

            migrationBuilder.DropTable(
                name: "ClinicalDocuments");

            migrationBuilder.DropTable(
                name: "ClinicalImages");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "HouseHoldInvoiceCollection");

            migrationBuilder.DropTable(
                name: "InvoiceLineItems");

            migrationBuilder.DropTable(
                name: "MedicalAids");

            migrationBuilder.DropTable(
                name: "OncologyConsultations");

            migrationBuilder.DropTable(
                name: "PatientPatientHousehold");

            migrationBuilder.DropTable(
                name: "PatientReferral");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PediatricsConsultations");

            migrationBuilder.DropTable(
                name: "PractitionerAvailabilities");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Procedures");

            migrationBuilder.DropTable(
                name: "PsychiatryConsultations");

            migrationBuilder.DropTable(
                name: "ToothCharts");

            migrationBuilder.DropTable(
                name: "UserPractices");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Vitals");

            migrationBuilder.DropTable(
                name: "DermatologyConsultations");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "DentistryConsultations");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Consultations");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "ClinicalRecords");

            migrationBuilder.DropTable(
                name: "PracticePractitioners");

            migrationBuilder.DropTable(
                name: "PatientHousehold");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Practitioners");

            migrationBuilder.DropTable(
                name: "Organisations");

            migrationBuilder.DropTable(
                name: "practices");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
