using Microsoft.EntityFrameworkCore.Migrations;

namespace MeFit.DAL.Migrations
{
    public partial class Exercise_Set_Workout_Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complete",
                table: "Workouts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Exercises",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Description", "ImageURL", "Name", "TargetMuscleGroup", "VideoURL" },
                values: new object[,]
                {
                    { 1, "Conditioning exercise in which a person squats, places the palms of the hands on the floor in front of the feet, jumps back into a push-up position", null, "Burpees", "Whole body", "https://www.thephysedexpress.com/uploads/3/1/1/1/31119283/burpee_orig.gif" },
                    { 2, "Raise your heels slowly, keeping your knees extended. Pause for one second when you're standing as much on the tips of your toes as you can. Lower your heels back to the ground, returning to the starting position.", null, "Calf raises", "Legs", "https://www.thephysedexpress.com/uploads/3/1/1/1/31119283/calf-raises_orig.gif" },
                    { 3, "Jump squats are bodyweight exercises characterized by leaping directly upwards at the top of the movement", null, "Jump squats", "Legs", "https://www.thephysedexpress.com/uploads/3/1/1/1/31119283/jump-squats_orig.gif" },
                    { 4, "The jump lunge is an advanced variation of a basic walking lunge exercise, bumping up the intensity by adding a jump.", null, "Jumping lunges", "Legs", "https://www.thephysedexpress.com/uploads/3/1/1/1/31119283/jumping-lunges_orig.gif" },
                    { 5, "Lay flat with your arms at your sides and legs stretched out next to each other, then raise those legs.", null, "Leg raises", "Abs", "https://www.thephysedexpress.com/uploads/3/1/1/1/31119283/leg-raises_orig.gif" }
                });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Basic", "Beginner" },
                    { 2, "Basic+", "Beginner" },
                    { 3, "Jump", "Competent" }
                });

            migrationBuilder.InsertData(
                table: "Sets",
                columns: new[] { "Id", "ExerciseId", "ExerciseRepetitions" },
                values: new object[,]
                {
                    { 1, 1, 10 },
                    { 6, 1, 15 },
                    { 2, 2, 10 },
                    { 7, 2, 15 },
                    { 3, 3, 10 },
                    { 8, 3, 15 },
                    { 4, 4, 10 },
                    { 9, 4, 15 },
                    { 5, 5, 10 },
                    { 10, 5, 15 }
                });

            migrationBuilder.InsertData(
                table: "SetWorkout",
                columns: new[] { "SetsId", "WorkoutsId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 2 },
                    { 8, 3 },
                    { 9, 3 },
                    { 5, 1 },
                    { 5, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SetWorkout",
                keyColumns: new[] { "SetsId", "WorkoutsId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "SetWorkout",
                keyColumns: new[] { "SetsId", "WorkoutsId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "SetWorkout",
                keyColumns: new[] { "SetsId", "WorkoutsId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "SetWorkout",
                keyColumns: new[] { "SetsId", "WorkoutsId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "SetWorkout",
                keyColumns: new[] { "SetsId", "WorkoutsId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "SetWorkout",
                keyColumns: new[] { "SetsId", "WorkoutsId" },
                keyValues: new object[] { 8, 3 });

            migrationBuilder.DeleteData(
                table: "SetWorkout",
                keyColumns: new[] { "SetsId", "WorkoutsId" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Sets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workouts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AddColumn<bool>(
                name: "Complete",
                table: "Workouts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Exercises",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
