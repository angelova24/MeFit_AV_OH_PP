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
        Dashboard
    </header>
    <main>
        <section title="calender">
            <input type="date" v-bind:value="currentDate" />
        </section>
        <section v-if="JSON.stringify(goals) !== '[]'" title="List of Goals">
            <GoalList v-bind:goals="goals"></GoalList>
        </section>
        <section v-if="goal !== undefined" title="Goal details">
            Details of goal:
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
        border-color: blue;
    }
</style>
