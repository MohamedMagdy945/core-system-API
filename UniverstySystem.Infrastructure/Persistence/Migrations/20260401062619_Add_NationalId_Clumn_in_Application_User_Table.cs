using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniverstySystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_NationalId_Clumn_in_Application_User_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "AspNetUsers");
        }
    }
}
