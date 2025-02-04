using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosTech.Hackathon.Appointments.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCpfMaxValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Patients",
                type: "NVARCHAR(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Doctors",
                type: "NVARCHAR(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(11)",
                oldMaxLength: 11);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Patients",
                type: "NVARCHAR(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(14)",
                oldMaxLength: 14);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Doctors",
                type: "NVARCHAR(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(14)",
                oldMaxLength: 14);
        }
    }
}
