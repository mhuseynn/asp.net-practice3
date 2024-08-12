using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lesson_8_EF.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FateherName",
                table: "Teachers",
                newName: "FatherName");

            migrationBuilder.RenameColumn(
                name: "FateherName",
                table: "Students",
                newName: "FatherName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FatherName",
                table: "Teachers",
                newName: "FateherName");

            migrationBuilder.RenameColumn(
                name: "FatherName",
                table: "Students",
                newName: "FateherName");
        }
    }
}
