<script setup>
    import { useStore } from 'vuex';

    import WorkoutListItem from './WorkoutListItem.vue';

    const store = useStore();

    const props = defineProps({
        workouts: {
            type: Array,
            required: true
        },
        header: {
            type: String,
            required: false
        }
    });
    const emits = defineEmits(["workoutListItemClicked"]);

    const onWorkoutListItemClicked = (event, id) => {
        console.log(`WorkoutListItem with id ${id} was clicked...`)
        store.commit("setWorkoutDetailsId", id);
        emits("workoutListItemClicked", event, id);
    }

</script>

<template>
    <div>
        <header v-if="header">
            <b>{{ header }}</b>
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

    header {
        background: grey;
        color:white;
        padding: 10px;
    }
    
</style>
