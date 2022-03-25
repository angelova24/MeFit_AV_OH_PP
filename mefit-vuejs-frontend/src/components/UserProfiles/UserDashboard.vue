<script setup>
    import { ref, computed } from 'vue';
    
    import { useStore } from "vuex";
    import GoalsDetail from "../Goals/GoalsDetail.vue";
    import GoalList from '../Goals/GoalList.vue';
 
    const today = new Date();
    const currentDate = ref(today.toISOString().split('T')[0]);
    
    const store = useStore();
    //--- get all Goals from Store
    const goals = computed(() => store.state.goals);
    //--- get id of selected goal
    const goalDetailsId = computed(() => store.state.goalDetailsId);
    console.log(`UserDashboard: selected goalId: ${goalDetailsId.value}`);
    //--- get selected goal
    const goal = computed(() => store.getters.getGoalById(goalDetailsId.value));
    console.log(`UserDashboard: selected goal: ${goal.value}`);

</script>

<template>
    <header>
        Your MeFit Dashboard
    </header>
    <main>
        <section class="box1" title="calender">
            Today is the day: <input type="date" v-bind:value="currentDate" />
        </section>
        <section v-if="JSON.stringify(goals) !== '[]'" title="List of Goals">
            <GoalList 
                v-bind:goals="goals"
                header="click on any of your goals to view its details..."
            ></GoalList>
        </section>
        <section v-if="goal !== undefined" title="Goal details">
            <GoalsDetail v-bind:goal="goal"></GoalsDetail>
        </section>
        <section v-if="JSON.stringify(goals) === '[]'">
            Ooops, it seems you have no goals setup so far...
        </section>
    </main>
</template>

<style scoped>
    
    header {
        font-size: larger;
        padding: 50px 10px;
    }
    main {
        display: grid;
        grid-template-columns: 1fr 1fr;
        row-gap: 10px;
        column-gap: 10px;        
    }
    .box1 {
        grid-column: 1 / 3;
    }
    section {
        border: 1px groove;
        border-color: yellow;
        box-shadow: 2px 2px 2px 1px grey;
        padding: 10px;
    }


    input[type="date"] {
        background-color: rgb(250, 250, 192);
        font-size: large;
    }

</style>
