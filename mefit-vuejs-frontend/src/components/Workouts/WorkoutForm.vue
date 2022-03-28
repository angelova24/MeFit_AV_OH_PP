<script setup>
    
    import { reactive, ref, toRefs, watch, computed } from 'vue';
    import { useStore } from 'vuex';
    import SetList from '../Sets/SetList.vue';
    import ExerciseList from '../Exercises/ExerciseList.vue';

    const props = defineProps({
        workout: {
            type: Object,
            required: false
        },
        header: {
            type: String,
            required: false
        }
    });
    const store = useStore();
    const { workout } = toRefs(props);
    const theWorkout = reactive({
            id: 0,
            name: "",
            type: "",
            ownerid: store.state.user.id,
            sets: []
    });
    watch(workout, (value) => {
        if(value === undefined)
        {
            theWorkout.value.id = 0;
            theWorkout.value.name = "";
            theWorkout.value.type = "";
            theWorkout.value.ownerid = store.state.user.id;
            theWorkout.value.sets = [];
        }
        else {
            theWorkout.value.id = value.id;
            theWorkout.value.name = value.name;
            theWorkout.value.type = value.type;
            theWorkout.value.ownerid = store.state.user.id;
            theWorkout.value.sets = value.sets;
        }
    })

    const onSaveWorkoutClicked = () => {
        if(theWorkout.id === 0) {
            console.log("Saving new Workout:", theWorkout.value);
            store.dispatch("addWorkout", theWorkout.value);
        }
        else {
            console.log("updating Workout:", theWorkout.value);
            store.dispatch("updateWorkout", theWorkout.value);
        }
    }
    //--- get all Exercises from Store
    const exercises = computed(() => store.state.exercises);

    const exerciseRepetitions = ref(10);
    const addSet2Workout = () => {
        if(store.state.exerciseDetailsId > 0)
        {
            const newSet = { 
                exerciseRepetitions: exerciseRepetitions.value,
                exerciseId: store.state.exerciseDetailsId,
                ownerId: store.state.user.id
            };
            store.commit("addSet", newSet);
            console.log("theWorkout.sets:", theWorkout.sets);
            theWorkout.sets.push(newSet);
        }
        else {
            alert("Please select an exercise to add...")
        }
    }
</script>

<template>
    <div class="root">
        <header>
            <b>{{ header }}</b><br />
        </header>
        <main>
            <label>Name:</label>
            <input type="text" v-model="theWorkout.name" />
            <label>Type:</label>
            <select v-model="theWorkout.type">
                <option value="">Select one...</option>
                <option value="Beginner">Beginner</option>
                <option value="Advanced">Advanced</option>
                <option value="Competent">Competent</option>
                <option value="Professional">Professional</option>              
            </select>
            <section title="Workout Sets">
                <SetList
                    header="ExerciseSets in this workout: (click to remove)"
                    v-bind:sets="theWorkout.sets"
                ></SetList>
                <button 
                    class="alignCenter"
                    v-on:click="addSet2Workout"
                >&lt;--</button>
                <div class="alignCenter">
                    <label>Number of repetitions:</label>
                    <input type="text" v-model="exerciseRepetitions" />
                </div>
                <ExerciseList
                    header="available Exercises:"
                    v-bind:exercises="exercises"
                ></ExerciseList>
            </section>
            <button class="saveButton" v-on:click="onSaveWorkoutClicked">Save Workout</button>
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
        padding: 10px;
    }
    section {
        display:grid;
        grid-template-columns: 6fr 1fr 2fr 6fr;
        column-gap: 10px;
        grid-column: 1 / 3;
    }
    .alignCenter {
        align-self: center;
    }
    .saveButton {
        grid-column: 1 / 3;
    }
    .root {
        padding: 0px 20px 50px 20px;
    }
    
</style>
