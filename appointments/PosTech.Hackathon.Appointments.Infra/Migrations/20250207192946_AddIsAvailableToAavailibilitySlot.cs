using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosTech.Hackathon.Appointments.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAvailableToAavailibilitySlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "AvailabilitySlots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "AvailabilitySlots");
        }
    }
}
