using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnalyticsService.Migrations
{
    /// <inheritdoc />
    public partial class modelsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppointmentSummaries",
                columns: table => new
                {
                    AppointmentSummaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalAppointments = table.Column<int>(type: "int", nullable: false),
                    CompletedCount = table.Column<int>(type: "int", nullable: false),
                    CancelledCount = table.Column<int>(type: "int", nullable: false),
                    NoShowCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentSummaries", x => x.AppointmentSummaryId);
                });

            migrationBuilder.CreateTable(
                name: "DoctorUtilizations",
                columns: table => new
                {
                    DoctorUtilizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    UtilizationPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorUtilizations", x => x.DoctorUtilizationId);
                });

            migrationBuilder.CreateTable(
                name: "NoShowStatistics",
                columns: table => new
                {
                    NoShowStatisticId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    NoShowRate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoShowStatistics", x => x.NoShowStatisticId);
                });

            migrationBuilder.CreateTable(
                name: "PeakHours",
                columns: table => new
                {
                    PeakHourId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    HourOfDay = table.Column<int>(type: "int", nullable: false),
                    AppointmentCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeakHours", x => x.PeakHourId);
                });

            migrationBuilder.CreateTable(
                name: "WaitTimeMetrics",
                columns: table => new
                {
                    WaitTimeMetricId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    AverageWaitTimeMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaitTimeMetrics", x => x.WaitTimeMetricId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSummaries_Date",
                table: "AppointmentSummaries",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorUtilizations_DoctorId_Date",
                table: "DoctorUtilizations",
                columns: new[] { "DoctorId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoShowStatistics_Date",
                table: "NoShowStatistics",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeakHours_Date_HourOfDay",
                table: "PeakHours",
                columns: new[] { "Date", "HourOfDay" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaitTimeMetrics_Date",
                table: "WaitTimeMetrics",
                column: "Date",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentSummaries");

            migrationBuilder.DropTable(
                name: "DoctorUtilizations");

            migrationBuilder.DropTable(
                name: "NoShowStatistics");

            migrationBuilder.DropTable(
                name: "PeakHours");

            migrationBuilder.DropTable(
                name: "WaitTimeMetrics");
        }
    }
}
