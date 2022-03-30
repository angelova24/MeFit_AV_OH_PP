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
  <div class="backgroundImage">
    <header>
      Everything about Workouts...
    </header>
    <main>
      <section v-if="JSON.stringify(workouts) !== '[]'" title="workoutList" class="workoutList">
        <a href="#">
        <WorkoutList
          v-bind:workouts="workouts"
          header="Click on any of these workouts to view details..."
        ></WorkoutList>
        </a>
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
</template>

<style scoped>

  header {
    font-size: larger;
    font-weight: bold;
    padding: 50px 10px;
    color:white;
  }
  main {
    display: grid;
    grid-template-columns: 1fr 1fr;
    row-gap: 10px;
    column-gap: 10px;
  }

  section {
    border: 1px groove;
    border-color: grey;
    background-color: white;
  }

  .backgroundImage {
    background-image: url("../../src/assets/crossfitGreen.jpg");
    background-repeat: no-repeat;
    /* background-size: 2600px 1100px; */
    padding: 0px 50px 25px 50px;
    height: 900px;
  }

</style>
