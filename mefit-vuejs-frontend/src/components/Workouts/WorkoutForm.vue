<script setup>
    
    import { toRefs, watch } from 'vue';
    import { useStore } from 'vuex';

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
    const theWorkout = {
            id: 0,
            name: "",
            type: "",
            ownerid: store.state.user.id
    };
    watch(workout, (value) => {
        if(value === undefined)
        {
            theWorkout.id = 0;
            theWorkout.name = "";
            theWorkout.type = "";
            theWorkout.ownerid = store.state.user.id;
        }
        else {
            theWorkout.id = value.id;
            theWorkout.name = value.name;
            theWorkout.type = value.type;
            theWorkout.ownerid = store.state.user.id;
        }
    })
    const onSaveWorkoutClicked = () => {
        if(theWorkout.id === 0) {
            console.log("Saving new Workout:", theWorkout);
            store.dispatch("addWorkout", theWorkout);
        }
        else {
            console.log("updating Workout:", theWorkout);
            store.dispatch("updateWorkout", theWorkout);
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
            <input type="text" v-model="theWorkout.name" />
            <label>Type:</label>
            <select v-model="theWorkout.type">
                <option value="">Select one...</option>
                <option value="Beginner">Beginner</option>
                <option value="Advanced">Advanced</option>
                <option value="Competent">Competent</option>
                <option value="Professional">Professional</option>              
            </select>
            <button v-on:click="onSaveWorkoutClicked">Save Workout</button>
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
        grid-template-columns: 1fr 3fr;
        row-gap: 20px;
        column-gap: 10px;
        align-items:stretch;
        padding: 10px;
        /* border: solid;         */
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
