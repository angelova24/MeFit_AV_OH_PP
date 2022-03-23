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

    //--- get workout from goalworkout.workoutId
    const workout = computed(() => store.getters.getWorkoutById(goalworkout.value.workoutId));
    console.log("GoalWorkoutListItem: workout:", workout.value);

</script>

<template>
    <div>
        <header>
           {{ goalworkout.workoutId }}: {{ workout.name }} - complete: {{ goalworkout.complete }}
        </header>
    </div>
</template>

<style scoped>

</style>
