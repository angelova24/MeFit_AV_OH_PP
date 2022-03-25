<script setup>
    import { toRefs, computed } from 'vue';
    import { useStore } from 'vuex';
    import WorkoutList from '../Workouts/WorkoutList.vue';

    const props = defineProps({
        program: {
            type: Object,
            required: false
        }
    });

    const { program } = toRefs(props);

    const store = useStore();

    const workouts = computed(() => {
        console.log("Workouts", program.value.workouts);
        const tempWorkouts = [];
        for (const workoutId of program.value.workouts) {
            console.log("workoutId:", workoutId);
            const tempWorkout = computed(() => store.getters.getWorkoutById(workoutId));
            console.log("tempWorkout:", tempWorkout.value);
            tempWorkouts.push(tempWorkout.value);
        }
        console.log(tempWorkouts);
        return tempWorkouts;
    });
</script>

<template>
    <div v-if="program !== undefined">
        <header>
            <b>{{ program.name }}</b><br />
            (Category: {{ program.category }})
        </header>
        <main>
            <section title="Workouts contained in this Program">
                This program comprises the following wokouts:
                <WorkoutList v-if="workouts[0] !== undefined" v-bind:workouts="workouts"></WorkoutList>
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
