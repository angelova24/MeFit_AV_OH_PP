<script setup>
    import { toRefs, computed, ref } from 'vue';
    import { useStore } from 'vuex';
    import WorkoutList from '../Workouts/WorkoutList.vue';

    const props = defineProps({
        goal: {
            type: Object,
            required: false
        }
    });

    const { goal } = toRefs(props);
    const goalStartDateString = ref(goal.value.startDate.toDateString());
    const goalEndDateString = ref(goal.value.endDate.toDateString());
    const store = useStore();

    const workouts = computed(() => {
        const tempWorkouts = [];
        if (goal.value.workouts !== undefined)
        {
            console.log("Workouts", goal.value.workouts);
            for (const workoutId of goal.value.workouts) {
                console.log("workoutId:", workoutId);
                const tempWorkout = computed(() => store.getters.getWorkoutById(workoutId));
                console.log("tempWorkout:", tempWorkout.value);
                tempWorkouts.push(tempWorkout.value);
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
                This goal comprises the following wokouts:
                <WorkoutList v-if="workouts[0] !== undefined" v-bind:workouts="workouts"></WorkoutList>
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
