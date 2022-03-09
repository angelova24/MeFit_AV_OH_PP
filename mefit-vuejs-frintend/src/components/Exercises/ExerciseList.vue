<script setup>
    import { computed } from 'vue';
    import { useStore } from 'vuex';

    import ExerciseListItem from './ExerciseListItem.vue';

    const props = defineProps({
        exercises: {
            type: Array,
            required: true
        }
    });

</script>

<template>
    <div>
        <header>
            Exercise List
        </header>
        <main>
            <ExerciseListItem 
                v-for="exercise in exercises" 
                v-bind:exercise="exercise"
                v-bind:key="exercise.id">
            </ExerciseListItem>
        </main>
    </div>
</template>

<style scoped>

</style>
