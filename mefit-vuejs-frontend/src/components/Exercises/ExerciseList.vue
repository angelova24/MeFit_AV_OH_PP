<script setup>
    import { computed } from 'vue';
    import { useStore } from 'vuex';

    import ExerciseListItem from './ExerciseListItem.vue';

    const store = useStore();

    const props = defineProps({
        exercises: {
            type: Array,
            required: true
        }
    });

    const onExerciseListItemClicked = (event, id) => {
        console.log(`setExerciseDetailsId to ${id}...`)
        store.commit("setExerciseDetailsId", id);
    }

</script>

<template>
    <div>
        <header>
            <b>List of available exercises:</b>
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

</style>
