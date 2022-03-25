<script setup>
    import { computed, toRefs } from 'vue';
    import { useStore } from 'vuex';

    const props = defineProps({
        goalworkout: {
            type: Object,
            required: true
        }
    });
    const {goalworkout} = toRefs(props);
    const store = useStore();
    const selectedGoalWorkoutId = computed(() => store.state.goalWorkoutDetailsId);

    //--- get workout from goalworkout.workoutId
    const workout = computed(() => store.getters.getWorkoutById(goalworkout.value.workoutId));
    console.log("GoalWorkoutListItem: workout:", workout.value);

    const onSetCompleteClicked = () => {

    }

</script>

<template>
    <div>
        <header v-bind:class="{ selected: goalworkout.id === selectedGoalWorkoutId }">
            <span>
                <b>{{ workout.name }}</b> - Status: <i>{{ goalworkout.complete ? "completed" : "in progress" }}</i>
            </span>
            <span>
                <button v-bind:click="onSetCompleteClicked">Set completed</button>
            </span>
        </header>
    </div>
</template>

<style scoped>

    header {
        padding: 10px;
        background: rgb(235, 220, 235);
        border: 1px solid rgb(248, 238, 248);
    }

    header.selected {
        padding: 10px;
        background: rgb(216, 195, 216);
        border: 1px solid rgb(248, 238, 248);
    }
    span {
        padding: 0px 30px;
    }

</style>
