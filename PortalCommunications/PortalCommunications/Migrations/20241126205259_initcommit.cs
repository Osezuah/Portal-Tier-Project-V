using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalCommunications.Migrations
{
    /// <inheritdoc />
    public partial class initcommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "devicegroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devicegroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "deviceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deviceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "homes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_homes_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    HomeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rooms_homes_HomeId",
                        column: x => x.HomeId,
                        principalTable: "homes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    TypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_devices_deviceTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "deviceTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_devices_devicegroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "devicegroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_devices_rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "devicelogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoggedState = table.Column<string>(type: "TEXT", nullable: false),
                    LoggedTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_devicelogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_devicelogs_devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_devicelogs_DeviceId",
                table: "devicelogs",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_devices_GroupId",
                table: "devices",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_devices_RoomId",
                table: "devices",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_devices_TypeId",
                table: "devices",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_homes_UserId",
                table: "homes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_HomeId",
                table: "rooms",
                column: "HomeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "devicelogs");

            migrationBuilder.DropTable(
                name: "devices");

            migrationBuilder.DropTable(
                name: "deviceTypes");

            migrationBuilder.DropTable(
                name: "devicegroups");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "homes");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
