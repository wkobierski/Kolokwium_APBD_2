using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolokwium_APBD_2.Migrations
{
    /// <inheritdoc />
    public partial class Cleanup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2000, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "BirthDate",
                value: new DateTime(2000, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Delivery",
                keyColumn: "DeliveryId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Delivery",
                keyColumn: "DeliveryId",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Delivery",
                keyColumn: "DeliveryId",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2026, 6, 21, 19, 44, 1, 945, DateTimeKind.Local).AddTicks(8070));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2026, 6, 21, 19, 44, 1, 952, DateTimeKind.Local).AddTicks(6530));

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "BirthDate",
                value: new DateTime(2026, 6, 21, 19, 44, 1, 952, DateTimeKind.Local).AddTicks(6550));

            migrationBuilder.UpdateData(
                table: "Delivery",
                keyColumn: "DeliveryId",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2026, 6, 21, 19, 44, 1, 955, DateTimeKind.Local).AddTicks(4180));

            migrationBuilder.UpdateData(
                table: "Delivery",
                keyColumn: "DeliveryId",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2026, 6, 21, 19, 44, 1, 955, DateTimeKind.Local).AddTicks(4420));

            migrationBuilder.UpdateData(
                table: "Delivery",
                keyColumn: "DeliveryId",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2026, 6, 21, 19, 44, 1, 955, DateTimeKind.Local).AddTicks(4430));
        }
    }
}
