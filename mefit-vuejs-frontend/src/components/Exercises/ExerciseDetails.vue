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
            <b>{{ exercise.name }}</b><br />
            (Target muscle group: {{ exercise.targetMuscleGroup }})
        </header>
        <main>
            <section class="box1" title="description">
                <b>This is how you do it:</b><br /><br />
                {{ exercise.description }}
            </section>
            <section class="box2" title="image">
                <img 
                    class="exerciseImage" 
                    v-if="exercise.imageURL !== null" 
                    v-bind:src="exercise.imageURL" 
                />
                <div v-if="exercise.imageURL === null">
                    Unfortunately, we have no image available for this exercise...<br /><br />
                    <img class="noImage" src="../../assets/noimage.gif" />
                </div>
            </section>
            <section title="video">
                <b>Video:</b><br />
                <a v-if="!exercise.videoURL.toLowerCase().endsWith('.gif')" v-bind:href="exercise.videoURL">
                    Example-Video
                </a>
                <img class="exerciseImage" v-if="exercise.videoURL.toLowerCase().endsWith('.gif')" v-bind:src="exercise.videoURL" />
            </section>
        </main>
    </div>
</template>

<style scoped>
    header {
        font-size: larger;
        padding: 20px 10px;
    }
    main {
        display: grid;
        grid-template-columns: 1fr 1fr;
        row-gap: 20px;
        column-gap: 10px;
        align-items:stretch;
        padding: 10px;
        
    }
    section {
        border: 1px dashed;
        border-color: white;
    }
    .box1 {
        grid-column: 1 / 3;
    }
    .box2 {
        align-self: center;
        padding: 10px;
    }
    .exerciseImage {
        width: 300px;
    }
    .noImage {
        width: 100px;
    }
</style>
