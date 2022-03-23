<script setup>
  import { useStore } from "vuex";
  import { computed, reactive, ref } from "vue";
  import GoalList from "./GoalList.vue";
  import GoalsDetail from "./GoalsDetail.vue";
  import WorkoutList from "../Workouts/WorkoutList.vue";

  const store = useStore();
  const today = new Date();
  const todayString = today.toISOString().split('T')[0];

  //--- get all Goals from Store
  const goals = computed(() => store.state.goals);
  const currentGoals = computed(() => store.getters.getCurrentGoals);

  //--- get id of selected goal
  const goalsDetailsId = computed(() => store.state.goalsDetailsId);
  console.log(`GoalsPage: selected goalId: ${goalsDetailsId.value}`);
  //--- get selected goal
  const goal = computed(() => store.getters.getGoalById(goalsDetailsId.value));
  console.log(`GoalsPage: selected goal: ${goal.value}`);

  //#region set new goal stuff
    const newGoalStartDateString = ref(today.toISOString().split('T')[0]);
    const workoutsInNewGoal = reactive([]);
    const addWorkout2NewGoal = () => {
      //--- add workout if it is not already contained in new goal
      if(!workoutsInNewGoal.includes(workout.value)) {
        workoutsInNewGoal.push(workout.value);
      }
    };
    const removeWorkoutFromNewGoal = () => {
      //--- remove workout
      if(workoutsInNewGoal.includes(workout.value)) {
        const index = workoutsInNewGoal.indexOf(workout.value);
        workoutsInNewGoal.splice(index, 1);
      }
    };
    //--- get all Workouts from Store
    const workouts = computed(() => store.state.workouts);
    //--- get id of selected workout
    const workoutDetailsId = computed(() => store.state.workoutDetailsId);
    console.log(`selected workout id: ${workoutDetailsId.value}`);
    //--- get selected workout
    const workout = computed(() => store.getters.getWorkoutById(workoutDetailsId.value));
    console.log(`selected workout: ${workout.value}`);
    const OnAddWorkOut = (event, id) => {
      console.log("WorkoutListItem has been clicked:", id, event);
      console.log("Workout:", workout.value)
      addWorkout2NewGoal();
    }
    const OnRemoveWorkOut = (event, id) => {
      console.log("WorkoutListItem has been clicked:", id, event);
      console.log("Workout:", workout.value)
      removeWorkoutFromNewGoal();
    }
    const onSetGoalClicked = () => {
      //--- add record to DB table Goals (startdate, endDate)
      //--- ProfileId is determined by BackEnd using the bearer token!!!
      const startDate = new Date(newGoalStartDateString.value);
      const endDate = new Date(startDate);
      endDate.setDate(endDate.getDate() + 7);
      const newGoal = {
        startDate,
        endDate
      }
      const withWorkOutIds = [];
      for (const workout of workoutsInNewGoal) {
        withWorkOutIds.push(workout.id);
      } 
      store.dispatch("addGoal", newGoal, withWorkOutIds)
        .then(value => {
          console.log("store.dispatch AddGoal returned:", value);
          //--- add one record per added workout to DB table GoalWorkout (GoalId, WorkoutId, Complete=false)

        });
      
    }
  //#endregion

</script>

<template>
  <div>Goals Page</div>
  <main>
    <section v-if="JSON.stringify(goals) !== '[]'" title="GoalList">
      <GoalList v-bind:goals="goals"></GoalList>
    </section>
    <section v-if="goal !== undefined" title="goalDetails">
      Details of exercise:
      <GoalsDetail v-bind:goal="goal"></GoalsDetail>
    </section>
    <section title="set new Goal" v-if="currentGoals.length === 0">
      No current goal...
      <section title="new goal">
        define new goal:
        <WorkoutList 
          v-if="workoutsInNewGoal" 
          v-bind:workouts="workoutsInNewGoal"
          v-on:workoutListItemClicked="OnRemoveWorkOut"
          header="These workouts have been added to your new goal (click to remove)"
        ></WorkoutList>
      </section>
      <section title="available workouts for new goal">
        <WorkoutList 
          v-bind:workouts="workouts" 
          v-on:workoutListItemClicked="OnAddWorkOut"
          header="You can add any of these workouts to your new goal (click to add)"
        ></WorkoutList>
      </section>
      <section title="set start date of new goal">
        my new Goal will start on:
        <input type="date" v-model="newGoalStartDateString" v-bind:min="todayString" /> 
      </section>
      <button v-on:click="onSetGoalClicked">Set Goal</button>
    </section>
    <section v-if="JSON.stringify(goals) === '[]'">
      We are sorry, but currently no goals are available.<br />
      Please try again later...
    </section>
  </main>

  <table class="Table">
    <thead>
      <tr>
        <th>Current goal of current week</th>
        <th>Period</th>
        <th>Status</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td
         v-if="workoutsInNewGoal"
         v-bind:workouts="workoutsInNewGoal"></td>
        <td>Here goes period for workout 1</td>
        <td><button>Completed</button></td>
      </tr>
      <tr>
        <td>Workout 2</td>
        <td>Here goes period for workout 2</td>
        <td><button>Completed</button></td>
      </tr>
      <tr>
        <td>Workout 3</td>
        <td>Here goes period for workout 3</td>
        <td><button>Completed</button></td>
      </tr>
    </tbody>
  </table>
  <br>
  <table class="CompletedTable">
  <thead>
    <tr>
      <th>Recently Completed</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>Completed Workout 1</td>
    </tr>
    <tr>
      <td>Completed Workout 2</td>
    </tr>
    <tr>
      <td>Completed Workout 3</td>
    </tr>
  </tbody>
</table>
</template>

<style scoped>

  header {
    font-size: larger;
  }
  main {
    display: flex;
    flex-wrap: wrap;
    row-gap: 10px;
    column-gap: 10px;
    align-items:stretch;  
  }
  section {
    flex: 1 1 45%;  /*grow | shrink | basis */
    border: 2px dashed;
    border-color: red;
  }
  table.Table {
    width: 85%;
    background-color: #fefbfb;
    border-collapse: collapse;
    border-width: 2px;
    border-color: #7e7d77;
    border-style: dashed;
    color: #000000;
  }

  table.Table td,
  table.Table th {
    border-width: 2px;
    border-color: #7e7d77;
    border-style: dashed;
    padding: 3px;
  }

  table.Table thead {
    background-color: #cfcfcf;
  }

  table.CompletedTable {
    width: 85%;
    text-align: center;
    background-color: #fefbfb;
    border-collapse: collapse;
    border-width: 2px;
    border-color: #7e7d77;
    border-style: dashed;
    color: #000000;
  }

  table.CompletedTable td, table.CompletedTable th {
    border-width: 2px;
    border-color: #7e7d77;
    border-style:dashed;
    padding: 3px;
  }

  table.CompletedTable thead {
    background-color: #05e684;
  }


</style>
