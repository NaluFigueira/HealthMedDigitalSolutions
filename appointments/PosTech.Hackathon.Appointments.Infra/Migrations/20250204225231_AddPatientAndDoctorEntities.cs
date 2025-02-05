using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosTech.Hackathon.Appointments.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientAndDoctorEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Patients",
                type: "NVARCHAR(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Patients",
                type: "NVARCHAR(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Patients",
                type: "NVARCHAR(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Doctors",
                type: "NVARCHAR(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CRM",
                table: "Doctors",
                type: "NVARCHAR(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Doctors",
                type: "NVARCHAR(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Doctors",
                type: "NVARCHAR(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "CRM",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Doctors");
        }
    }
}
