<script setup>
    import { useStore } from 'vuex';

    import GoalWorkoutListItem from './GoalWorkoutListItem.vue';

    const store = useStore();

    const props = defineProps({
        goalworkouts: {
            type: Array,
            required: true
        }
    });

    const onGoalWorkoutListItemClicked = (event, id) => {
        console.log(`GoalWorkoutListItem with id ${id} was clicked...`)
        store.commit("setGoalWorkoutDetailsId", id);
    }

</script>

<template>
    <div>
        <header>
            <b>List of goal's workouts:</b>
        </header>
        <main>
            <GoalWorkoutListItem 
                v-for="goalworkout, index in goalworkouts" 
                v-bind:goalworkout="goalworkout"
                v-bind:key="index"
                v-on:click="onGoalWorkoutListItemClicked($event, goalworkout.workoutId)">
            </GoalWorkoutListItem>
        </main>
    </div>
</template>

<style scoped>

</style>
