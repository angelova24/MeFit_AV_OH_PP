<script setup>
    import { computed } from "@vue/runtime-core";
    import { useStore } from "vuex";

    const props = defineProps({
        workout: {
            type: Object,
            required: true
        }
    });
    const store = useStore();
    const selectedWorkoutId = computed(() => store.state.workoutDetailsId);

</script>

<template>
    <div>
        <header v-bind:class="{ selected: workout.id === selectedWorkoutId }">
            {{ workout.name }}
        </header>
    </div>
</template>

<style scoped>

    div {
        padding: 10px;
        background: whitesmoke;
        border: 1px black solid;
    }

    div.selected {
        padding: 10px;
        background: lightgrey;
        border: black solid;
    }
    
</style>
