using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatBloodType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BloodTybe",
                table: "Members",
                newName: "BloodType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BloodType",
                table: "Members",
                newName: "BloodTybe");
        }
    }
}
