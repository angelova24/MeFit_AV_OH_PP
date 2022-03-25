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

    header {
        padding: 10px;
        background: rgb(219, 248, 219);
        border: 1px solid rgb(236, 250, 222);
    }

    header.selected {
        padding: 10px;
        background: lightgreen;
        border: 1px solid rgb(236, 250, 222);
    }
    
</style>
