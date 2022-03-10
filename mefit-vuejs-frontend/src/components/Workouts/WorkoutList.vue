<script setup>
    import { computed } from 'vue';
    import { useStore } from 'vuex';

    import WorkoutListItem from './WorkoutListItem.vue';

    const store = useStore();

    const props = defineProps({
        workouts: {
            type: Array,
            required: true
        }
    });

    const onWorkoutListItemClicked = (event, id) => {
        console.log(`WorkoutListItem with id ${id} was clicked...`)
        store.commit("setWorkoutDetailsId", id);
    }

</script>

<template>
    <div>
        <header>
            <b>List of available workouts:</b>
        </header>
        <main>
            <WorkoutListItem 
                v-for="workout in workouts" 
                v-bind:workout="workout"
                v-bind:key="workout.id"
                v-on:click="onWorkoutListItemClicked($event, workout.id)">
            </WorkoutListItem>
        </main>
    </div>
</template>

<style scoped>

</style>
