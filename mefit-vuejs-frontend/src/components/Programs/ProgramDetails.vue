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
            <b>{{ program.name }}</b>
        </header>
        <main>
            <section title="category">
                <b>Category:</b> {{ program.category }}
            </section>
            <section title="workouts">
                This program comprises the following wokouts:
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
