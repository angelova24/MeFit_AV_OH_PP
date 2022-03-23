using Microsoft.EntityFrameworkCore.Migrations;

namespace MeFit.DAL.Migrations
{
    public partial class Add_GoalWorkouts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalWorkout_Goals_GoalId",
                table: "GoalWorkout");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalWorkout_Workouts_WorkoutId",
                table: "GoalWorkout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoalWorkout",
                table: "GoalWorkout");

            migrationBuilder.RenameTable(
                name: "GoalWorkout",
                newName: "GoalWorkouts");

            migrationBuilder.RenameIndex(
                name: "IX_GoalWorkout_WorkoutId",
                table: "GoalWorkouts",
                newName: "IX_GoalWorkouts_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_GoalWorkout_GoalId",
                table: "GoalWorkouts",
                newName: "IX_GoalWorkouts_GoalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoalWorkouts",
                table: "GoalWorkouts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalWorkouts_Goals_GoalId",
                table: "GoalWorkouts",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalWorkouts_Workouts_WorkoutId",
                table: "GoalWorkouts",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalWorkouts_Goals_GoalId",
                table: "GoalWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_GoalWorkouts_Workouts_WorkoutId",
                table: "GoalWorkouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoalWorkouts",
                table: "GoalWorkouts");

            migrationBuilder.RenameTable(
                name: "GoalWorkouts",
                newName: "GoalWorkout");

            migrationBuilder.RenameIndex(
                name: "IX_GoalWorkouts_WorkoutId",
                table: "GoalWorkout",
                newName: "IX_GoalWorkout_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_GoalWorkouts_GoalId",
                table: "GoalWorkout",
                newName: "IX_GoalWorkout_GoalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoalWorkout",
                table: "GoalWorkout",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalWorkout_Goals_GoalId",
                table: "GoalWorkout",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GoalWorkout_Workouts_WorkoutId",
                table: "GoalWorkout",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
