<script setup>
    const props = defineProps({
        workout: {
            type: Object,
            required: false
        }
    });
</script>

<template>
    <div v-if="workout !== undefined">
        <header>
            <b>{{ workout.name }}</b>
        </header>
        <main>
            <section title="type">
                <b>Type:</b> {{ workout.type }}
            </section>
        </main>
    </div>
</template>

<style scoped>
    header {
        font-size: larger;
    }
    main {
        display: flex;
        flex-wrap: wrap;
        row-gap: 10px;
        column-gap: 10px;
        align-items:stretch;
        
    }
    section {
        flex: 1 1 45%;  /*grow | shrink | basis */
        border: 2px dashed;
        border-color: blue;
    }
    img {
        width: 300px;
    }
</style>
