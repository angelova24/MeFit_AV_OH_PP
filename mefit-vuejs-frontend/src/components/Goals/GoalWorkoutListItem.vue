<script setup>
    const props = defineProps({
        goalworkout: {
            type: Object,
            required: true
        }
    });
</script>

<template>
    <div>
        <header>
           {{ goalworkout.workoutId }}: {{ goalworkout.name }} - {{ goalworkout.complete }}
        </header>
    </div>
</template>

<style scoped>

</style>
