<script setup>
import { useStore } from "vuex";
import { computed } from "vue";
import ProgramList from "./ProgramList.vue";
import ProgramDetails from "./ProgramDetails.vue";

const store = useStore();

//--- get all Programs from Store
const programs = computed(() => store.state.programs);
console.log(JSON.stringify(programs.value));
//--- get id of selected program
const programDetailsId = computed(() => store.state.programDetailsId);
console.log(`selected program id: ${programDetailsId.value}`);
//--- get selected program
const program = computed(() =>
  store.getters.getProgramById(programDetailsId.value)
);
console.log(`selected program: ${program.value}`);
</script>


<template>
  <div class="backgroundImage">
    <header>
      Everything about MeFit fitness programs...
    </header>
    <main>
      <section v-if="JSON.stringify(programs) !== '[]'" title="Program List">
        <ProgramList
          v-bind:programs="programs"
          header="Click on any of these programs to view details..."
        ></ProgramList>
      </section>
      <section v-if="program !== undefined" title="programDetails">
        <ProgramDetails v-bind:program="program"></ProgramDetails>
      </section>
      <section v-if="JSON.stringify(programs) === '[]'">
        We are sorry, but currently no programs are available.<br />
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
