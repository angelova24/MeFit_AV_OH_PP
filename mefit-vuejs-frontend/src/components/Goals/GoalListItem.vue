<script setup>
    const props = defineProps({
        goal: {
            type: Object,
            required: true
        }
    });
</script>

<template>
    <div>
        <header>
            {{ goal.startDate }} - {{ goal.endDate }}
        </header>
    </div>
</template>

<style scoped>

</style>
