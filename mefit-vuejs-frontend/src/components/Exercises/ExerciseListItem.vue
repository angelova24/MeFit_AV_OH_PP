<script setup>
import { computed } from "@vue/runtime-core";
import { useStore } from "vuex";

    const props = defineProps({
        exercise: {
            type: Object,
            required: true
        }
    });
    const store = useStore();
    const selectedExerciseId = computed(() => store.state.exerciseDetailsId);

</script>

<template>
    <div>
        <header v-bind:class="{ selected: exercise.id === selectedExerciseId }">
            <!-- exercise.id: {{ exercise.id }}, selectedExerciseId: {{ selectedExerciseId }} -->
            {{ exercise.name }}
        </header>
    </div>
</template>

<style scoped>

    header {
        padding: 10px;
        background: whitesmoke;
        border: 1px black solid;
    }

    header.selected {
        padding: 10px;
        background: lightgrey;
        border: black solid;
    }
    
</style>
