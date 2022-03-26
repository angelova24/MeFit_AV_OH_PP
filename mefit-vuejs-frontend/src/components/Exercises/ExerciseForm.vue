<script setup>
    
    import { ref, toRefs } from 'vue';

    const props = defineProps({
        exercise: {
            type: Object,
            required: false
        },
        header: {
            type: String,
            required: false
        }
    });
    let { exercise } = props;

    const exName = ref("");
    const description = ref("");
    const targetMuscleGroup = ref("");
    const videoUrl = ref("");
    const imageUrl = ref("");

    const onSaveExerciseClicked = () => {
        if(exercise === undefined || exercise === null) {
            exercise = { 
                name: exName.value,
                description: description.value,
                targetMuscleGroup: targetMuscleGroup.value,
                videoUrl: videoUrl.value,
                imageUrl: imageUrl.value
            };    
            console.log("Saving Exercise:", exercise);
            
        }
        

    }

</script>

<template>
    <div>
        <header>
            <b>{{ header }}</b><br />
        </header>
        <main>
            <label>Name:</label>
            <input type="text" v-model="exName" />
            <label>Description:</label>
            <textarea v-model="description"></textarea>
            <label>Target muscle group:</label>
            <select v-model="targetMuscleGroup">
                <option value="">Select one...</option>
                <option value="Shoulders">Shoulders</option>
                <option value="Chest">Chest</option>
                <option value="Back">Back</option>
                <option value="Abdominals">Abdominals</option>
                <option value="Arms">Arms</option>
                <option value="Biceps">Biceps (front of upper arm)</option>
                <option value="Triceps">Triceps (back of upper arms)</option>
                <option value="Glutes">Glutes (butt and hips)</option>
                <option value="Legs">Legs</option>
                <option value="Quadriceps">Quadriceps (front of upper leg)</option>
                <option value="Hamstrings">Hamstrings (back of upper leg)</option>
                <option value="Calves">Calves (lower legs)</option>                
            </select>
            <label>Video Url:</label>
            <input type="url" v-model="videoUrl" />
            <label>Image Url:</label>
            <input type="url" v-model="imageUrl" />
            <button v-on:click="onSaveExerciseClicked">Save Exercise</button>
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
        grid-template-columns: 1fr 2fr;
        row-gap: 20px;
        column-gap: 10px;
        align-items:stretch;
        padding: 10px;
        
    }
    .exerciseImage {
        width: 300px;
    }
    .noImage {
        width: 100px;
    }
    label {
        text-align: right;
    }
    button {
        grid-column: 1 / 3;
    }
    
</style>
