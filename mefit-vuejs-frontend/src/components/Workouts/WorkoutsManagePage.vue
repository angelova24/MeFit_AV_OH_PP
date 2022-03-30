<script setup>
    import { useStore } from 'vuex';
    import { computed, onBeforeMount } from 'vue';
    import WorkoutList from './WorkoutList.vue';
    import WorkoutForm from './WorkoutForm.vue';  
    import { useRouter } from 'vue-router';

    const store = useStore();
    const router = useRouter();

    //--- get all Workouts from current user from Store
    const workouts = computed(() => store.getters.getWorkoutsByOwnerId(store.state.user.id));

    //--- get id of selected workout
    const workoutDetailsId = computed(() => store.state.workoutDetailsId);
    //--- get selected workout
    const workout = computed(() => store.getters.getWorkoutById(workoutDetailsId.value));

    onBeforeMount(() => {
        if(!store.state.user.isContributor) {
            router.push(store.state.baseUrl + "dashboard");
        }
    })

</script>


<template>
    <div class="backgroundImage">
        <header>
            Manage workouts...
        </header>
        <main>
            <section v-if="workouts.length > 0" title="List of Workouts">
                <WorkoutList 
                    v-bind:workouts="workouts"
                    header="These Workouts have already been added by you:"
                ></WorkoutList>
            </section>
            <section v-if="workouts.length <= 0">
                You have no workouts added yet...
            </section>
            <section title="Details of Workout">
                <WorkoutForm
                    v-bind:exercise="workout"
                ></WorkoutForm>
            </section>
        </main>
    </div>
</template>

<style scoped>
    
    header {
        font-size: larger;
        font-weight: bold;
        padding: 50px 10px;
        color:white;
    }
    main {
        display: grid;
        grid-template-columns: 1fr 1fr;
        row-gap: 10px;
        column-gap: 10px;
    }

    section {
        border: 1px groove;
        border-color: grey;
        background-color: white;
    }

    .backgroundImage {
        background-image: url("../../src/assets/crossfitGreen.jpg");
        background-repeat: no-repeat;
        /* background-size: 2600px 1100px; */
        padding: 0px 50px 25px 50px;
        height: 900px;
    }

</style>
