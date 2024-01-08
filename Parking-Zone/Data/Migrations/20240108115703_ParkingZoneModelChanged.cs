using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking_Zone.Data.Migrations
{
    /// <inheritdoc />
    public partial class ParkingZoneModelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingZones_ParkingZones_ParkingZoneId",
                table: "ParkingZones");

            migrationBuilder.DropIndex(
                name: "IX_ParkingZones_ParkingZoneId",
                table: "ParkingZones");

            migrationBuilder.DropColumn(
                name: "ParkingZoneId",
                table: "ParkingZones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParkingZoneId",
                table: "ParkingZones",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingZones_ParkingZoneId",
                table: "ParkingZones",
                column: "ParkingZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingZones_ParkingZones_ParkingZoneId",
                table: "ParkingZones",
                column: "ParkingZoneId",
                principalTable: "ParkingZones",
                principalColumn: "Id");
        }
    }
}
