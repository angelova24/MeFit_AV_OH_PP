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
            {{ exercise.name }}
        </header>
    </div>
</template>

<style scoped>

    header {
        padding: 10px;
        background: lightblue;
        border: 1px solid lightcyan;
    }

    header.selected {
        padding: 10px;
        background: rgb(113, 204, 235);
        border: 1px solid lightcyan;   
    }
    
</style>
