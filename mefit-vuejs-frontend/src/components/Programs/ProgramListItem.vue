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
        background: rgb(250, 250, 192);
        border: 1px solid rgb(248, 248, 235);
    }

    header.selected {
        padding: 10px;
        background: rgb(250, 250, 135);
        border: 1px solid rgb(248, 248, 235);
    }

</style>
