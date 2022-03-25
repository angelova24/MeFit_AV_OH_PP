<script setup>
  import { useStore } from "vuex";
  import { computed, reactive, ref } from "vue";
  import GoalList from "./GoalList.vue";
  import GoalsDetail from "./GoalsDetail.vue";
  import WorkoutList from "../Workouts/WorkoutList.vue";
  import ProgramList from "../Programs/ProgramList.vue";

  const store = useStore();
  const today = new Date();
  const todayString = today.toISOString().split('T')[0];

  //--- get all Goals from Store
  const goals = computed(() => store.state.goals);
  const currentGoals = computed(() => store.getters.getCurrentGoals);

  //--- get id of selected goal
  const goalDetailsId = computed(() => store.state.goalDetailsId);
  //--- get selected goal
  const goal = computed(() => store.getters.getGoalById(goalDetailsId.value));

  //#region set new goal stuff
    const newGoalStartDateString = ref(today.toISOString().split('T')[0]);
    const workoutsInNewGoal = reactive([]);
    const addProgram2NewGoal = () => {
      console.log("Program to be added to Goal:", program.value);
      for (const workoutId of program.value.workouts) {
        const workoutOfProgram = store.getters.getWorkoutById(workoutId);
        //--- add workout if it is not already contained in new goal
        if(!workoutsInNewGoal.includes(workoutOfProgram)) {
          workoutsInNewGoal.push(workoutOfProgram);
        }
      }
    };
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
    const programs = computed(() => store.state.programs);
    const workouts = computed(() => store.state.workouts);
    //--- get id of selected program
    const programDetailsId = computed(() => store.state.programDetailsId);
    //--- get id of selected workout
    const workoutDetailsId = computed(() => store.state.workoutDetailsId);
    console.log(`selected workout id: ${workoutDetailsId.value}`);
    //--- get selected program
    const program = computed(() => store.getters.getProgramById(programDetailsId.value));
    //--- get selected workout
    const workout = computed(() => store.getters.getWorkoutById(workoutDetailsId.value));
    console.log(`selected workout: ${workout.value}`);
    const OnAddProgram = (event, id) => {
      console.log("ProgramListItem has been clicked:", id, event);
      console.log("program:", program.value)
      addProgram2NewGoal();
    }
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
      console.log("withWorkOutIds", withWorkOutIds);
      store.dispatch("addGoal", { goal: newGoal, workoutIds: withWorkOutIds })
        .then(value => {
          console.log("store.dispatch AddGoal returned:", value);
          //--- add one record per added workout to DB table GoalWorkout (GoalId, WorkoutId, Complete=false)

        });
      
    }
    const onSetCompletedGoalWorkout = (workoutData) => {
      console.log("button clicked")
      store.dispatch("updateGoalWorkout", workoutData)      
    } 
    
  //#endregion
 //const workoutById = store.getter.getWorkoutById(workout.workoutId)
</script>

<template>
  <div>Goals Page</div>
  <main>
    <section v-if="JSON.stringify(goals) !== '[]'" title="GoalList">
      <GoalList 
        v-bind:goals="goals"
        header="your Goals:"
      ></GoalList>
      selected: {{ goalDetailsId }}
    </section>

    <section v-if="goal !== undefined" title="goalDetails">
      Details of goal:
      <GoalsDetail v-bind:goal="goal"></GoalsDetail>
    </section>

    <!-- <section class="parentSection" title="set new Goal" v-if="currentGoals.length === 0"> -->
    <section class="parentSection" title="set new Goal" v-if="currentGoals.length >= 0">
      <span class="box1">No current goal...</span>
      <section class="box2" title="new goal">
        define new goal:
        <WorkoutList 
          v-if="workoutsInNewGoal" 
          v-bind:workouts="workoutsInNewGoal"
          v-on:workoutListItemClicked="OnRemoveWorkOut"
          header="These workouts have been added to your new goal (click to remove)"
        ></WorkoutList>
      </section>
      <section title="available programs for new goal">
        <ProgramList 
          v-bind:programs="programs" 
          v-on:programListItemClicked="OnAddProgram"
          header="You can add any of these programs to your new goal (click to add)"
        ></ProgramList>
      </section>
      <section class="box3" title="available workouts for new goal">
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
      <button class="box5" v-on:click="onSetGoalClicked">Set Goal</button>
    </section>
    <section v-if="JSON.stringify(goals) === '[]'">
      We are sorry, but currently no goals are available.<br />
      Please try again later...
    </section>
  </main>

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
        
        <td>{{(store.getters.getWorkoutById(workout.workoutId).name)}}</td>
        <td>{{currentGoals[0].startDate}} -- {{currentGoals[0].endDate}}</td>
        <td><button v-on:click="onSetCompletedGoalWorkout({workout:workout, goal:currentGoals[0]})">Complete !</button></td>
      </tr>      
    </tbody>
  </table>

  <br>
  <table class="CompletedWorkoutsTable">
  <thead>
    <tr>
      <th>Recently Completed</th>
    </tr>
  </thead>
  <tbody>
    <tr v-for="workout in currentGoals[0].workouts" :key="workout.id">
      <td v-if="workout.complete === true">{{(store.getters.getWorkoutById(workout.workoutId).name) + " is now completed"}}</td>
    </tr>        
  </tbody>
</table>
<br>
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
  section.parentSection {
    display: grid;
    grid-template-columns: auto auto;
  }
  .box1 {
    grid-column: 1 / 3;
  }
  .box2 {
    grid-row: 2 / 4;
  }
  .box3 {
    grid-column: 2;
  }
  .box5 {
    grid-column: 1;
  }
  table.Table {
    width: 100%;
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

  table.CompletedWorkoutsTable {
    width: 100%;
    text-align: center;
    background-color: #fefbfb;
    border-collapse: collapse;
    border-width: 2px;
    border-color: #7e7d77;
    border-style: dashed;
    color: #000000;
  }

  table.CompletedWorkoutsTable td, table.CompletedWorkoutsTable th {
    border-width: 2px;
    border-color: #7e7d77;
    border-style:dashed;
    padding: 3px;
  }

  table.CompletedWorkoutsTable thead {
    background-color: #05e684;
  }


  /*  */
  table.CompletedGoals {
    width: 100%;
    text-align: center;
    background-color: #fefbfb;
    border-collapse: collapse;
    border-width: 2px;
    border-color: #7e7d77;
    border-style: dashed;
    color: #000000;
  }

  table.CompletedGoals td, table.CompletedGoals th {
    border-width: 2px;
    border-color: #7e7d77;
    border-style:dashed;
    padding: 3px;
  }

  table.CompletedGoals thead {
    background-color: #05e684;
  }


</style>
