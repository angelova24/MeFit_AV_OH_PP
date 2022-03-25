<script setup>
    import { useStore } from 'vuex';
    import { computed } from 'vue';
    import WorkoutList from './WorkoutList.vue';
    import WorkoutDetails from './WorkoutDetails.vue';  

    const store = useStore();

    //--- get all Workouts from Store
    const workouts = computed(() => store.state.workouts);

    //--- get id of selected workout
    const workoutDetailsId = computed(() => store.state.workoutDetailsId);
    console.log(`selected workout id: ${workoutDetailsId.value}`);
    //--- get selected workout
    const workout = computed(() => store.getters.getWorkoutById(workoutDetailsId.value));
    console.log(`selected workout: ${workout.value}`);

</script>


<template>
    <div>
        <header>
            Everything about Workouts...
        </header>
        <main>
            <section v-if="JSON.stringify(workouts) !== '[]'" title="workoutList">
                <WorkoutList 
                    v-bind:workouts="workouts"
                    header="available Workouts:"
                ></WorkoutList>
            </section>
            <section v-if="workout != undefined" title="workoutDetails">
                Details of workout:
                <WorkoutDetails v-bind:workout="workout"></WorkoutDetails>
            </section>
            <section v-if="JSON.stringify(workouts) === '[]'">
                We are sorry, but currently no workouts are available.<br />
                Please try again later...
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
        grid-template-columns: 1fr 1fr;
        row-gap: 10px;
        column-gap: 10px;        
    }
    section {
        border: 1px groove;
        border-color: lightseagreen;
        box-shadow: 2px 2px 2px 1px grey;
    }

</style>
