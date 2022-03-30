<script setup>

    import { computed } from "vue";
    import { useStore } from "vuex";

    const props = defineProps({
        program: {
            type: Object,
            required: true
        }
    });
    const store = useStore();
    const selectedProgramId = computed(() => store.state.programDetailsId);

</script>

<template>
    <div>
        <header v-bind:class="{ selected: program.id === selectedProgramId }">
            {{ program.name }}
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
