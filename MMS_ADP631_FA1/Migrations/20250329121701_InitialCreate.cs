using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MMS_ADP631_FA1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citizens",
                columns: table => new
                {
                    CitizenID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizens", x => x.CitizenID);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    HireDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffID);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CitizenID = table.Column<int>(type: "INTEGER", nullable: false),
                    ReportType = table.Column<string>(type: "TEXT", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: false),
                    SubmissionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_Reports_Citizens_CitizenID",
                        column: x => x.CitizenID,
                        principalTable: "Citizens",
                        principalColumn: "CitizenID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CitizenID = table.Column<int>(type: "INTEGER", nullable: false),
                    ServiceType = table.Column<string>(type: "TEXT", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Citizens_CitizenID",
                        column: x => x.CitizenID,
                        principalTable: "Citizens",
                        principalColumn: "CitizenID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Citizens",
                columns: new[] { "CitizenID", "Address", "DateOfBirth", "Email", "FullName", "PhoneNumber", "RegistrationDate" },
                values: new object[,]
                {
                    { 1, "84 Foggy Hollow Rd", new DateTime(1993, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "zara.windcroft@mail.com", "Zara Windcroft", "5554321987", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "22 Maple Creek", new DateTime(1987, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "elton.bravo@mail.com", "Elton Braverman", "5559872345", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "310 Sunset Boulevard", new DateTime(1995, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "lyra.monty@mail.com", "Lyra Montclair", "5551239087", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "15 Oakridge Lane", new DateTime(1990, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "dexter.flan@mail.com", "Dexter Flannigan", "5556781234", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffID", "Department", "Email", "FullName", "HireDate", "PhoneNumber", "Position" },
                values: new object[,]
                {
                    { 1, "Infrastructure", "marcel.thornby@municipality.com", "Marcel Thornby", new DateTime(2015, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "5556549821", "Chief Engineer" },
                    { 2, "Communications", "lila.hawthorne@municipality.com", "Lila Hawthorne", new DateTime(2018, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "5558912345", "Public Relations Officer" },
                    { 3, "Finance", "eugene.whitlock@municipality.com", "Eugene Whitlock", new DateTime(2012, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "5557612984", "Finance Manager" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "ReportID", "CitizenID", "Details", "ReportType", "Status", "SubmissionDate" },
                values: new object[,]
                {
                    { 1, 1, "Broken street light on Foggy Hollow Rd", "Complaint", "Under Review", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 3, "Water overflow at Sunset Boulevard", "Incident Report", "Resolved", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 4, "Quick response on pothole repair", "Feedback", "Closed", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ServiceRequests",
                columns: new[] { "RequestID", "CitizenID", "RequestDate", "ServiceType", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Street Light Repair", "Pending" },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Garbage Collection Delay", "Completed" },
                    { 3, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Water Pressure Issue", "In Progress" },
                    { 4, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Road Pothole Repair", "Pending" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CitizenID",
                table: "Reports",
                column: "CitizenID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CitizenID",
                table: "ServiceRequests",
                column: "CitizenID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Citizens");
        }
    }
}
