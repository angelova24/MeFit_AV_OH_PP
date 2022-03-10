<script setup>
    const props = defineProps({
        exercise: {
            type: Object,
            required: false
        }
    });
</script>

<template>
    <div v-if="exercise !== undefined">
        <header>
            <b>{{ exercise.name }}</b>
        </header>
        <main>
            <section title="description">
                {{ exercise.description }}
            </section>
            <section title="targetMuscleGroup">
                <b>Target muscle group:</b> {{ exercise.targetMuscleGroup }}
            </section>
            <section title="image">
                <img v-bind:src="exercise.imageURL" />
            </section>
            <section title="video">
                <a v-bind:href="exercise.videoURL">
                    Example-Video
                </a>
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
