using Microsoft.EntityFrameworkCore.Migrations;

namespace MeFit.DAL.Migrations
{
    public partial class DeleteAddressTableConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Addresses_AddressId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_AddressId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine1",
                table: "Profiles",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine2",
                table: "Profiles",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Profiles",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Profiles",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Profiles",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProfileId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProfileId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileId",
                table: "Users",
                column: "ProfileId",
                unique: true,
                filter: "[ProfileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Profiles_ProfileId",
                table: "Users",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Profiles_ProfileId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressLine1",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "AddressLine2",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Profiles");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Profiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Profiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    City = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProfileId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "ProfileId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_AddressId",
                table: "Profiles",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Addresses_AddressId",
                table: "Profiles",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Users_UserId",
                table: "Profiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
