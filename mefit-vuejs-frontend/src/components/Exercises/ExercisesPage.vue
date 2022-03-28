<script setup>
import { useStore } from "vuex";
import { computed } from "vue";
import ExerciseList from "./ExerciseList.vue";
import ExerciseDetails from "./ExerciseDetails.vue";

const store = useStore();

//--- get all Exercises from Store
const exercises = computed(() => store.state.exercises);

//--- get id of selected exercise
const exerciseDetailsId = computed(() => store.state.exerciseDetailsId);
console.log(`ExercisePage: selected exerciseid: ${exerciseDetailsId.value}`);
//--- get selected exercise
const exercise = computed(() =>
  store.getters.getExerciseById(exerciseDetailsId.value)
);
console.log(`ExercisePage: selected exercise: ${exercise.value}`);
</script>


<template>
  <body>
    <div>
      <main>
        <h3 id="h3tag">Everything about exercises...</h3>
        <section v-if="JSON.stringify(exercises) !== '[]'" title="exerciseList">
          <ExerciseList
            v-bind:exercises="exercises"
            header="Click on any of these exercises to view details..."
          ></ExerciseList>
        </section>
        <section v-if="exercise !== undefined" title="exerciseDetails">
          <ExerciseDetails v-bind:exercise="exercise"></ExerciseDetails>
        </section>
        <section v-if="JSON.stringify(exercises) === '[]'">
          We are sorry, but currently no exercises are available.<br />
          Please try again later...
        </section>
      </main>
    </div>
  </body>
</template>

<style scoped>
header {
  font-size: larger;
  font-family: "Franklin Gothic Medium", "Arial Narrow", Arial, sans-serif;
  padding: auto;
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
  left: 280px;
  background-color: aliceblue;  
}
body {
  background-image: url("../../src/assets/crossfitGreen.jpg");
  background-size: 2600px 1100px;
  background-repeat: no-repeat;
  padding-bottom: 25px;
  position: relative;
}

#h3tag {
  color:rgb(255, 255, 255);
  padding: 1px;
  padding-top: 10px;
  position: relative;
  left: 10px;
}
@media (max-width: 2559px) {
  body {
    background-size: 1900px 1100px;
  }
  section {
  border: 1px groove;
  border-color: lightskyblue;
  box-shadow: 2px 2px 2px 1px grey;
  width: 80%;
  position: relative;
  left: 200px;
  background-color: aliceblue;  
}    
}
</style>
