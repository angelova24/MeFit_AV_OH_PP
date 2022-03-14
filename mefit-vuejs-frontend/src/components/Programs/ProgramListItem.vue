<script setup>
    const props = defineProps({
        program: {
            type: Object,
            required: true
        }
    });
</script>

<template>
    <div>
        <header>
            {{ program.name }}
        </header>
    </div>
</template>

<style scoped>

</style>
