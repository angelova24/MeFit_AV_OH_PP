<script setup>
import { useStore } from "vuex";
import { computed } from "vue";
import WorkoutList from "./WorkoutList.vue";
import WorkoutDetails from "./WorkoutDetails.vue";

const store = useStore();

//--- get all Workouts from Store
const workouts = computed(() => store.state.workouts);

//--- get id of selected workout
const workoutDetailsId = computed(() => store.state.workoutDetailsId);
console.log(`selected workout id: ${workoutDetailsId.value}`);
//--- get selected workout
const workout = computed(() =>
  store.getters.getWorkoutById(workoutDetailsId.value)
);
console.log(`selected workout: ${workout.value}`);
</script>


<template>
  <body>
    <div>
      <h3 id="h3tag">Everything about Workouts...</h3>
      <main>
        <section v-if="JSON.stringify(workouts) !== '[]'" title="workoutList">
          <WorkoutList
            v-bind:workouts="workouts"
            header="Click on any of these workouts to view details..."
          ></WorkoutList>
        </section>
        <section v-if="workout != undefined" title="workoutDetails">
          <WorkoutDetails v-bind:workout="workout"></WorkoutDetails>
        </section>
        <section v-if="JSON.stringify(workouts) === '[]'">
          We are sorry, but currently no workouts are available.<br />
          Please try again later...
        </section>
      </main>
    </div>
  </body>
</template>

<style scoped>
header {
  font-size: larger;
  padding: 50px 10px;
}
main {
  display: -moz-box;
  grid-template-columns: 1fr 1fr;
  row-gap: 10px;
  column-gap: 10px;
}
section {
  border: 1px groove;
  border-color: lightskyblue;
  box-shadow: 2px 2px 2px 1px grey;
  width: 80%;
  position: relative;
  left: 180px;
  background-color: aliceblue;
}
body {
  background-image: url("../../src/assets/crossfitGreen.jpg");
  background-size: 2600px 1100px;
  background-repeat: no-repeat;
  padding-bottom: 25px;
}
#h3tag {
  color:rgb(255, 255, 255);
  padding: 1px;
  padding-top: 10px;
  position: relative;
  left: 10px;
}
</style>
