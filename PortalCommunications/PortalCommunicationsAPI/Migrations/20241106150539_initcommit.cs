using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalCommunicationsAPI.Migrations
{
    /// <inheritdoc />
    public partial class initcommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeviceType = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    humidityLevel = table.Column<double>(type: "REAL", nullable: true),
                    waterLevel = table.Column<double>(type: "REAL", nullable: true),
                    fridgeTemp = table.Column<double>(type: "REAL", nullable: true),
                    freezerTemp = table.Column<double>(type: "REAL", nullable: true),
                    SmartFridge_humidityLevel = table.Column<double>(type: "REAL", nullable: true),
                    currentTemp = table.Column<double>(type: "REAL", nullable: true),
                    targetTemp = table.Column<double>(type: "REAL", nullable: true),
                    batteryLife = table.Column<double>(type: "REAL", nullable: true),
                    location = table.Column<string>(type: "TEXT", nullable: true),
                    notification = table.Column<string>(type: "TEXT", nullable: true),
                    Thermostat_currentTemp = table.Column<double>(type: "REAL", nullable: true),
                    Thermostat_targetTemp = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceLog",
                columns: table => new
                {
                    deviceLogId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    deviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    loggedMessage = table.Column<string>(type: "TEXT", nullable: false),
                    loggedTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceLog", x => x.deviceLogId);
                    table.ForeignKey(
                        name: "FK_DeviceLog_Device_deviceId",
                        column: x => x.deviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceLog_deviceId",
                table: "DeviceLog",
                column: "deviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceLog");

            migrationBuilder.DropTable(
                name: "Device");
        }
    }
}
