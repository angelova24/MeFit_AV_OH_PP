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
  <body>
    <div>
      <main>
        <h3 id="h3tag">Everything about MeFit fitness programs...</h3>
        <section v-if="JSON.stringify(programs) !== '[]'" title="programList">
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
  width: 70%;
  position: relative;
  left: 290px;
  background-color: aliceblue;
}
body {
  background-image: url("../../src/assets/crossfitGreen.jpg");
  background-repeat: no-repeat;
  position: relative;
  background-size: 2600px 1100px;
  padding-bottom: 25px;
}
#h3tag {
  color: rgb(255, 255, 255);
  padding: 1px;
  padding-top: 10px;
  position: relative;
  left: 10px;
}
</style>
