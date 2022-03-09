<script setup>
    import { useStore } from 'vuex';
    import { computed } from 'vue';
    import ExerciseList from './ExerciseList.vue';  

    const store = useStore();

    //--- get all Exercises from Store
    const exercises = computed(() => store.state.exercises);

</script>


<template>
    <div>
        <header>
            Exercise Page
        </header>
        <main>
            <ExerciseList v-bind:exercises="exercises"></ExerciseList>
        </main>
    </div>
</template>

<style scoped>

</style>
