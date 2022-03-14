<script setup>
    const props = defineProps({
        workout: {
            type: Object,
            required: true
        }
    });
</script>

<template>
    <div>
        <header>
            {{ workout.name }}
        </header>
    </div>
</template>

<style scoped>

</style>
