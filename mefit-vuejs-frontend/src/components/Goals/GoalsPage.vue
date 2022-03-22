<script setup>
import { useStore } from "vuex";
import { computed } from "vue";
import GoalList from "./GoalList.vue";
import GoalsDetail from "./GoalsDetail.vue";

const store = useStore();

//--- get all Goals from Store
const goals = computed(() => store.state.goals);

//--- get id of selected goal
const goalsDetailsId = computed(() => store.state.goalsDetailsId);
console.log(`GoalsPage: selected goalId: ${goalsDetailsId.value}`);
//--- get selected goal
const goal = computed(() => store.getters.getGoalById(goalsDetailsId.value));
console.log(`GoalsPage: selected goal: ${goal.value}`);
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
        <td>Workout 1</td>
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
