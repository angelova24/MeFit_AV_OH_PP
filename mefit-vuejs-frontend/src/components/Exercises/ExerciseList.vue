<script setup>
    import { useStore } from 'vuex';

    import ExerciseListItem from './ExerciseListItem.vue';

    const store = useStore();

    const props = defineProps({
        exercises: {
            type: Array,
            required: true
        },
        header: {
            type: String,
            required: false
        }
    });

    const onExerciseListItemClicked = (event, id) => {
        console.log(`ExerciseListItem with id ${id} was clicked...`)
        store.commit("setExerciseDetailsId", id);
    }

</script>

<template>
    <div>
        <header>
            <b>{{ header }}</b>
        </header>
        <main>
            <ExerciseListItem 
                v-for="exercise in exercises" 
                v-bind:exercise="exercise"
                v-bind:key="exercise.id"
                v-on:click="onExerciseListItemClicked($event, exercise.id)">
            </ExerciseListItem>
        </main>
    </div>
</template>

<style scoped>

    header {
        background: black;
        color:white;
        padding: 10px;
    }
    
</style>
