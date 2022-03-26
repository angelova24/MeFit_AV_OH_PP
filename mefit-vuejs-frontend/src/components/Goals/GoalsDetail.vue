<script setup>
    import { toRefs, computed, ref } from 'vue';
    import { useStore } from 'vuex';
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
            <b>{{ goalStartDateString }} - {{ goalEndDateString }}</b><br />
            (Status: <b>{{ goal.achieved ? "all Workouts completed" : "in progress..." }}</b>)
        </header>
        <main>
            <section title="Workouts status of this goal">
                This goal comprises the following workouts:
                <GoalWorkoutList  
                    v-if="JSON.stringify(goal.workouts) !== '[]'" 
                    v-bind:goalworkouts="goal.workouts"

                ></GoalWorkoutList>
            </section>
        </main>
    </div>
</template>

<style scoped>
    
    header {
        font-size: larger;
        padding: 20px 10px;
    }
    main {
        display: grid;
        grid-template-columns: 1fr;
        row-gap: 20px;
        column-gap: 10px;
        align-items:stretch;
        padding: 10px;
        
    }
    section {
        border: 1px dashed;
        border-color: white;
        box-shadow: 2px 2px 2px 1px grey;
    }

</style>
