<script setup>
    import { useStore } from 'vuex';
    import { computed } from 'vue';
    import ExerciseList from './ExerciseList.vue';
    import ExerciseDetail from './ExerciseDetail.vue';  

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
            Exercise Page
        </header>
        <main>
            <section title="exerciseList">
                <ExerciseList v-bind:exercises="exercises"></ExerciseList>
            </section>
            <section title="exerciseDetails">
                Details of exercise:
                <ExerciseDetail v-bind:exercise="exercise"></ExerciseDetail>
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
        border-color: red;
    }
    img {
        width: 300px;
    }
</style>
