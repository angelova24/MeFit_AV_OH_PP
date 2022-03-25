<script setup>
    import { useStore } from 'vuex';
    import { computed } from 'vue';
    import ExerciseList from './ExerciseList.vue';
    import ExerciseDetails from './ExerciseDetails.vue';  

    const store = useStore();

    //--- get all Exercises from Store
    const exercises = computed(() => store.state.exercises);

    //--- get id of selected exercise
    const exerciseDetailsId = computed(() => store.state.exerciseDetailsId);
    console.log(`ExercisePage: selected exerciseid: ${exerciseDetailsId.value}`);
    //--- get selected exercise
    const exercise = computed(() => store.getters.getExerciseById(exerciseDetailsId.value));
    console.log(`ExercisePage: selected exercise: ${exercise.value}`);

</script>


<template>
    <div>
        <header>
            Manage exercises...
        </header>
        <main>
            <section v-if="JSON.stringify(exercises) !== '[]'" title="exerciseList">
                <ExerciseList 
                    v-bind:exercises="exercises"
                    header="Click on any of these exercises to view details..."
                ></ExerciseList>
            </section>
            <section v-if="exercise !== undefined" title="exerciseDetails">
                <ExerciseDetails 
                    v-bind:exercise="exercise"
                ></ExerciseDetails>
            </section>
            <section v-if="JSON.stringify(exercises) === '[]'">
                We are sorry, but currently no exercises are available.<br />
                Please try again later...
            </section>
        </main>
    </div>
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
    section {
        border: 1px groove;
        border-color: lightskyblue;
        box-shadow: 2px 2px 2px 1px grey;
    }

</style>
