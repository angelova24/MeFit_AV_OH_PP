<script setup>
    import { ref, computed } from 'vue';
    
    import { useStore } from "vuex";
    import GoalsDetail from "../Goals/GoalsDetail.vue";
 
    const today = new Date();
    const currentDate = ref(today.toISOString().split('T')[0]);

    const store = useStore();
    const goal = computed(() => store.getters.getGoalById(1));
    const numberOfDaysLeft = computed(() => {
        let result = NaN;
        if (goal.value !== undefined) {
            result = Math.ceil((goal.value.endDate - new Date(currentDate.value)) / (24 * 60 * 60 * 1000));
        }
        return result;
    });


</script>

<template>
    <header>
        Dashboard
    </header>
    <main>
        <section title="calender">
            <input type="date" v-bind:value="currentDate" />
        </section>
        <section v-if="goal !== undefined" title="currentGoal">
            <GoalsDetail v-bind:goal="goal"></GoalsDetail>
            <b>{{ numberOfDaysLeft }}</b> days left to reach this goal...
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
