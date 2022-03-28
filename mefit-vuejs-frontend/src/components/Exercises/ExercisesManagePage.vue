<script setup>
    import { useStore } from 'vuex';
    import { computed, onBeforeMount } from 'vue';
    import ExerciseList from './ExerciseList.vue';
    import ExerciseForm from './ExerciseForm.vue';  
    import { useRouter } from 'vue-router';

    const store = useStore();
    const router = useRouter();

    //--- get all Exercises from current user from Store
    const exercises = computed(() => store.getters.getExercisesByOwnerId(store.state.user.id));

    //--- get id of selected exercise
    const exerciseDetailsId = computed(() => store.state.exerciseDetailsId);
    console.log(`ExerciseManagePage: selected exerciseid: ${exerciseDetailsId.value}`);
    //--- get selected exercise
    const exercise = computed(() => store.getters.getExerciseById(exerciseDetailsId.value));
    console.log(`ExercisePage: selected exercise: ${exercise.value}`);

    onBeforeMount(() => {
        if(!store.state.user.isContributor) {
            router.push(store.state.baseUrl + "dashboard");
        }
    })

</script>


<template>
    <div>
        <header>
            Manage exercises...
        </header>
        <main>
            <section v-if="exercises.length > 0" title="exerciseList">
                <ExerciseList 
                    v-bind:exercises="exercises"
                    header="These Exercises have already been added by you:"
                ></ExerciseList>
            </section>
            <section v-if="exercises.length <= 0">
                You have no exercises added yet...
            </section>
            <section title="Details of Exercise">
                <ExerciseForm
                    v-bind:exercise="exercise"
                ></ExerciseForm>
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
        grid-template-columns: 1fr 2fr;
        row-gap: 10px;
        column-gap: 10px;        
    }
    section {
        border: 1px groove;
        border-color: lightskyblue;
        box-shadow: 2px 2px 2px 1px grey;
    }

</style>
