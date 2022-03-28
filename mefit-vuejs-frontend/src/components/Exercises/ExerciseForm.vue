<script setup>
    
    import { toRefs, watch } from 'vue';
    import { useStore } from 'vuex';

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
    const store = useStore();
    const { exercise } = toRefs(props);
    const theExercise = {
            id: 0,
            name: "",
            description: "",
            targetMuscleGroup: "",
            videoUrl: "",
            imageUrl: "",
            ownerid: store.state.user.id
    };
    watch(exercise, (value) => {
        console.log("value:", value);
        if(value === undefined)
        {
            theExercise.id = 0;
            theExercise.name = "";
            theExercise.description = "";
            theExercise.targetMuscleGroup = "";
            theExercise.videoUrl = "";
            theExercise.imageUrl = "";
            theExercise.ownerid = store.state.user.id;
        }
        else {
            theExercise.id = value.id;
            theExercise.name = value.name;
            theExercise.description = value.description;
            theExercise.targetMuscleGroup = value.targetMuscleGroup;
            theExercise.videoUrl = value.videoUrl;
            theExercise.imageUrl = value.imageUrl;
            theExercise.ownerid = store.state.user.id;
        }
        console.log("theExercise:", theExercise);
    })
    const onSaveExerciseClicked = () => {
        if(theExercise.id === 0) {
            console.log("Saving new Exercise:", theExercise);
            store.dispatch("addExercise", theExercise);
        }
        else {
            console.log("updating Exercise:", theExercise);
            store.dispatch("updateExercise", theExercise);
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
            <input type="text" v-model="theExercise.name" />
            <label>Description:</label>
            <textarea v-model="theExercise.description"></textarea>
            <label>Target muscle group:</label>
            <select v-model="theExercise.targetMuscleGroup">
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
            <input type="url" v-model="theExercise.videoUrl" />
            <label>Image Url:</label>
            <input type="url" v-model="theExercise.imageUrl" />
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
    
    div {
        padding: 0px 20px 50px 20px;
    }
    
</style>
