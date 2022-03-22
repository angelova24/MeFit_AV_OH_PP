<script setup>
    import { toRefs, computed, ref } from 'vue';
    import { useStore } from 'vuex';
    import WorkoutList from '../Workouts/WorkoutList.vue';
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
    const workouts = computed(() => {
        const tempWorkouts = [];
        if (goal.value.workouts !== undefined)
        {
            console.log("Workouts", goal.value.workouts);
            for (const workout of goal.value.workouts) {
                console.log("workoutId:", workout.workoutId);
                const tempWorkout = store.getters.getWorkoutById(workout.workoutId);
                console.log("tempWorkout:", tempWorkout);
                tempWorkouts.push(tempWorkout);
            }
        }
        console.log(tempWorkouts);
        return tempWorkouts;
    });
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
                <!-- <WorkoutList v-if="workouts[0] !== undefined" v-bind:workouts="workouts"></WorkoutList> -->
                <GoalWorkoutList v-if="goal.workouts !== undefined" v-bind:goalworkouts="goal.workouts"></GoalWorkoutList>
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
