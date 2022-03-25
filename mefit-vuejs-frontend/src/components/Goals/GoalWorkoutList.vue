<script setup>
    import { useStore } from 'vuex';

    import GoalWorkoutListItem from './GoalWorkoutListItem.vue';

    const store = useStore();

    const props = defineProps({
        goalworkouts: {
            type: Array,
            required: true
        },
        header: {
            type: String,
            required: false
        }
    });

    const onGoalWorkoutListItemClicked = (event, id) => {
        console.log(`GoalWorkoutListItem with id ${id} was clicked...`)
        store.commit("setGoalWorkoutDetailsId", id);
    }

</script>

<template>
    <div>
        <header v-if="header">
            <b>{{ header }}</b>
        </header>
        <main>
            <GoalWorkoutListItem 
                v-for="goalworkout, index in goalworkouts" 
                v-bind:goalworkout="goalworkout"
                v-bind:key="index"
                v-on:click="onGoalWorkoutListItemClicked($event, goalworkout.id)">
            </GoalWorkoutListItem>
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
