using Microsoft.EntityFrameworkCore.Migrations;

namespace MeFit.DAL.Migrations
{
    public partial class Program_Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[] { 1, "loose fat", "Get in summer shape" });

            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[] { 2, "no gym", "Covid - do not leave home" });

            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "Id", "Category", "Name" },
                values: new object[] { 3, "beginner", "Conquer one's weaker self" });

            migrationBuilder.InsertData(
                table: "ProgramWorkout",
                columns: new[] { "ProgramsId", "WorkoutsId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ProgramWorkout",
                columns: new[] { "ProgramsId", "WorkoutsId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "ProgramWorkout",
                columns: new[] { "ProgramsId", "WorkoutsId" },
                values: new object[] { 2, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProgramWorkout",
                keyColumns: new[] { "ProgramsId", "WorkoutsId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProgramWorkout",
                keyColumns: new[] { "ProgramsId", "WorkoutsId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProgramWorkout",
                keyColumns: new[] { "ProgramsId", "WorkoutsId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "Programs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Programs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Programs",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
