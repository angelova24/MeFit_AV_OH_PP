<script setup>
import { useStore } from "vuex";
import { computed, reactive, ref } from "vue";
import GoalList from "./GoalList.vue";
import GoalsDetail from "./GoalsDetail.vue";
import WorkoutList from "../Workouts/WorkoutList.vue";
import ProgramList from "../Programs/ProgramList.vue";

const store = useStore();
const today = new Date();
const todayString = today.toISOString().split("T")[0];

//--- get all Goals from Store
const goals = computed(() => store.state.goals);
const currentGoals = computed(() => store.getters.getCurrentGoals);

//--- get id of selected goal
const goalDetailsId = computed(() => store.state.goalDetailsId);
//--- get selected goal
const goal = computed(() => store.getters.getGoalById(goalDetailsId.value));

//#region set new goal stuff
const newGoalStartDateString = ref(today.toISOString().split("T")[0]);
const workoutsInNewGoal = reactive([]);
const addProgram2NewGoal = () => {
  console.log("Program to be added to Goal:", program.value);
  for (const workoutId of program.value.workouts) {
    const workoutOfProgram = store.getters.getWorkoutById(workoutId);
    //--- add workout if it is not already contained in new goal
    if (!workoutsInNewGoal.includes(workoutOfProgram)) {
      workoutsInNewGoal.push(workoutOfProgram);
    }
  }
};
const addWorkout2NewGoal = () => {
  //--- add workout if it is not already contained in new goal
  if (!workoutsInNewGoal.includes(workout.value)) {
    workoutsInNewGoal.push(workout.value);
  }
};
const removeWorkoutFromNewGoal = () => {
  //--- remove workout
  if (workoutsInNewGoal.includes(workout.value)) {
    const index = workoutsInNewGoal.indexOf(workout.value);
    workoutsInNewGoal.splice(index, 1);
  }
};
//--- get all Workouts from Store
const programs = computed(() => store.state.programs);
const workouts = computed(() => store.state.workouts);
//--- get id of selected program
const programDetailsId = computed(() => store.state.programDetailsId);
//--- get id of selected workout
const workoutDetailsId = computed(() => store.state.workoutDetailsId);
console.log(`selected workout id: ${workoutDetailsId.value}`);
//--- get selected program
const program = computed(() =>
  store.getters.getProgramById(programDetailsId.value)
);
//--- get selected workout
const workout = computed(() =>
  store.getters.getWorkoutById(workoutDetailsId.value)
);
console.log(`selected workout: ${workout.value}`);
const OnAddProgram = (event, id) => {
  console.log("ProgramListItem has been clicked:", id, event);
  console.log("program:", program.value);
  addProgram2NewGoal();
};
const OnAddWorkOut = (event, id) => {
  console.log("WorkoutListItem has been clicked:", id, event);
  console.log("Workout:", workout.value);
  addWorkout2NewGoal();
};
const OnRemoveWorkOut = (event, id) => {
  console.log("WorkoutListItem has been clicked:", id, event);
  console.log("Workout:", workout.value);
  removeWorkoutFromNewGoal();
};
const onSetGoalClicked = () => {
  //--- add record to DB table Goals (startdate, endDate)
  //--- ProfileId is determined by BackEnd using the bearer token!!!
  const startDate = new Date(newGoalStartDateString.value);
  const endDate = new Date(startDate);
  endDate.setDate(endDate.getDate() + 7);
  const newGoal = {
    startDate,
    endDate,
  };
  const withWorkOutIds = [];
  for (const workout of workoutsInNewGoal) {
    withWorkOutIds.push(workout.id);
  }
  console.log("withWorkOutIds", withWorkOutIds);
  store
    .dispatch("addGoal", { goal: newGoal, workoutIds: withWorkOutIds })
    .then((value) => {
      console.log("store.dispatch AddGoal returned:", value);
      //--- add one record per added workout to DB table GoalWorkout (GoalId, WorkoutId, Complete=false)
    });
};
const onSetCompletedGoalWorkout = (workoutData) => {
  console.log("button clicked");
  store.dispatch("updateGoalWorkout", workoutData);
};

//#endregion
//const workoutById = store.getter.getWorkoutById(workout.workoutId)
</script>

<template>
  <body>
    <h3 id="h3tag">Goals Page</h3>
    <div class="styleWrapper">
      <main>
        <!-- <section class="parentSection" title="set new Goal" v-if="currentGoals.length === 0"> -->
        <div title="set new Goal" v-if="currentGoals.length >= 0">
          <section title="available programs for new goal">
            <ProgramList
              v-bind:programs="programs"
              v-on:programListItemClicked="OnAddProgram"
              header="You can add any of these programs to your new goal (click to add)"
            ></ProgramList>
          </section>
          <section
            class="availableWorkouts"
            title="available workouts for new goal"
          >
            <WorkoutList
              v-bind:workouts="workouts"
              v-on:workoutListItemClicked="OnAddWorkOut"
              header="You can add any of these workouts to your new goal (click to add)"
            ></WorkoutList>
          </section>
          <section class="newGoalBox" title="new goal">
            <WorkoutList
              v-if="workoutsInNewGoal"
              v-bind:workouts="workoutsInNewGoal"
              v-on:workoutListItemClicked="OnRemoveWorkOut"
              header="These workouts have been added to your new goal (click to remove)"
            ></WorkoutList>
            <button class="setGoalButton" v-on:click="onSetGoalClicked">
              Set Goal
            </button>
          </section>
          <section title="set start date of new goal">
            Select a start date for goal:
            <input
              type="date"
              v-model="newGoalStartDateString"
              v-bind:min="todayString"
            />
          </section>
          <br />
          <br />
          <section
            v-if="JSON.stringify(goals) !== '[]'"
            title="GoalList"
            class="scrollList"
          >
            <a href="#">
              <GoalList v-bind:goals="goals" header="Your goals:"></GoalList>
            </a>
          </section>
          <br />

          <section
            v-if="goal !== undefined"
            title="goalDetails"
            class="goalDetails"
          >
            <h3>Details of goal:</h3>
            <GoalsDetail v-bind:goal="goal"></GoalsDetail>
          </section>
        </div>
        <section v-if="JSON.stringify(goals) === '[]'">
          We are sorry, but currently no goals are available.<br />
          Please try again later...
        </section>
        <table class="Table">
          <thead>
            <tr>
              <th>Current workouts</th>
              <th>Period</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody v-if="currentGoals.length > 0">
            <tr v-for="workout in currentGoals[0].workouts" :key="workout.id">
              <td>
                {{ store.getters.getWorkoutById(workout.workoutId).name }}
              </td>
              <td>
                {{ currentGoals[0].startDate }} -- {{ currentGoals[0].endDate }}
              </td>
              <td>
                <button
                  v-on:click="
                    onSetCompletedGoalWorkout({
                      workout: workout,
                      goal: currentGoals[0],
                    })
                  "
                >
                  Complete !
                </button>
              </td>
            </tr>
          </tbody>
        </table>
        <table class="CompletedWorkoutsTable">
          <thead>
            <tr>
              <th>Recently Completed</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="workout in currentGoals[0].workouts" :key="workout.id">
              <td v-if="workout.complete === true">
                {{
                  store.getters.getWorkoutById(workout.workoutId).name +
                  " is now completed"
                }}
              </td>
            </tr>
          </tbody>
        </table>
        <table class="CompletedGoals">
          <thead>
            <tr>
              <th>Completed Goals</th>
            </tr>
          </thead>
          <tbody>
            <tr v-bind:goals="goals">
              <td v-if="JSON.stringify(goals) !== '[]'">goals is empty</td>
            </tr>
          </tbody>
        </table>
      </main>
    </div>
  </body>
</template>

<style scoped>
header {
  font-size: larger;
}
main {
  display: -moz-box;
  grid-template-columns: 1fr 1fr;
  row-gap: 10px;
  column-gap: 10px;
}
#h3tag {
  color: white;
  position: relative;
  top: 5px;
  padding: 2px;
}
.scrollList a {
  color: black;
  text-decoration: none;
}
.scrollList {
  width: 1000px;
  height: 150px;
  overflow-y: auto;
  position: relative;
  bottom: 40px;
  left: 800px;
}
.goalDetails {
  width: 1000px;
  height: 220px;
  overflow-y: auto;
  position: relative;
  bottom: 58px;
  left: 800px;
  background-color: aliceblue;
}
section {
  border: 1px groove;
  border-color: lightskyblue;
  width: 1000px;
  position: relative;
  left: 800px;
  background-color: aliceblue;
}
body {
  background-image: url("../../src/assets/crossfitGreen.jpg");
  background-repeat: no-repeat;
  background-size: 2600px 1100px;
}
.setGoalButton {
  grid-column: 1;
  padding: 2px;
  width: 10%;
}
table.Table {
  width: 1000px;
  background-color: #fefbfb;
  border-collapse: collapse;
  border-width: 2px;
  border-style: double;
  color: #000000;
  position: relative;
  bottom: 58px;
  left: 800px;
}

table.Table td {
  border-width: 2px;
  border-color: #252521;
  border-style: double;
  padding: 4px;
}
table.Table thead {
  background-color: #cfcfcf;
}
table.CompletedWorkoutsTable {
  width: 1000px;
  text-align: center;
  background-color: #fefbfb;
  border-collapse: collapse;
  border-width: 2px;
  border-color: #252521;
  border-style: double;
  color: #000000;
  position: relative;
  bottom: 58px;
  left: 800px;
}

table.CompletedWorkoutsTable td,
table.CompletedWorkoutsTable th {
  border-width: 2px;
  border-color: #252521;
  border-style: double;
  padding: 3px;
}

table.CompletedWorkoutsTable thead {
  background-color: #05e684;
}

/*  */
table.CompletedGoals {
  width: 1000px;
  text-align: center;
  background-color: #fefbfb;
  border-collapse: collapse;
  border-width: 2px;
  border-color: #252521;
  border-style: double;
  color: #000000;
  position: relative;
  bottom: 58px;
  left: 800px;
}

table.CompletedGoals td,
table.CompletedGoals th {
  border-width: 2px;
  border-color: #252521;
  border-style: double;
  padding: 3px;
}

table.CompletedGoals thead {
  background-color: #05e684;
}

@media (max-width: 2559px) {
  body {
    background-size: 1900px 1100px;
  }
  .styleWrapper{
    position: relative;
    right: 350px;
  }   
}
</style>
