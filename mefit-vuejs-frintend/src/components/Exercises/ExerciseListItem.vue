<script setup>
    const props = defineProps({
        exercise: {
            type: Object,
            required: true
        }
    });
</script>

<template>
    <div>
        <header>
            {{ exercise.name }}
        </header>
        <main>
            {{ exercise.description }}
        </main>
    </div>
</template>

<style scoped>

</style>
