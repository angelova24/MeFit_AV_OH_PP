<script setup>
    import { toRefs, computed, ref } from 'vue';
    import { useStore } from 'vuex';
    //import WorkoutList from '../Workouts/WorkoutList.vue';
    import GoalWorkoutList from './GoalWorkoutList.vue';

    const props = defineProps({
        goal: {
            type: Object,
            required: true
        }
    });

    const { goal } = toRefs(props);
    const store = useStore();
    const goalStartDateString = ref(goal.value.startDate.toDateString());
    const goalEndDateString = ref(goal.value.endDate.toDateString());
    
</script>

<template>
    <div v-if="goal !== undefined">
        <header>
            <b>{{ goalStartDateString }} - {{ goalEndDateString }}</b>
        </header>
        <main>
            <section title="achieved">
                <b>completed:</b> {{ goal.achieved }}
            </section>
            <section title="workouts">
                This goal comprises the following workouts:
                <GoalWorkoutList  v-if="JSON.stringify(goal.workouts) !== '[]'" v-bind:goalworkouts="goal.workouts"></GoalWorkoutList>
            </section>
        </main>
    </div>
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
    img {
        width: 300px;
    }
</style>
