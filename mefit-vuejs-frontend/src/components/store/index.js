import { createStore } from "vuex";

const store = createStore({
    state: {
        user: {
            userid: 0,
            username: ""
        },
        profile: {
            
        },
        exercises: [],
        exerciseDetailsId: 0,
        sets: [
            {
                id: 1,
                exerciseRepetitions: 10,
                exerciseId: 1
            },
            {
                id: 2,
                exerciseRepetitions: 10,
                exerciseId: 2
            },
            {
                id: 3,
                exerciseRepetitions: 10,
                exerciseId: 3
            },
            {
                id: 4,
                exerciseRepetitions: 10,
                exerciseId: 4
            },
            {
                id: 5,
                exerciseRepetitions: 10,
                exerciseId: 5
            },
            {
                id: 6,
                exerciseRepetitions: 15,
                exerciseId: 1
            },
            {
                id: 7,
                exerciseRepetitions: 15,
                exerciseId: 2
            },
            {
                id: 8,
                exerciseRepetitions: 15,
                exerciseId: 3
            },
            {
                id: 9,
                exerciseRepetitions: 15,
                exerciseId: 4
            },
            {
                id: 10,
                exerciseRepetitions: 15,
                exerciseId: 5
            }
        ],
        workouts: [],
        workoutDetailsId: 0,
        programs: [],
        programDetailsId: 0
    },
    mutations: {
        addExercises: (state, payload) => {
            for (const exercise of payload) {
                state.exercises.push(exercise);    
            }
        },
        setExerciseDetailsId: (state, payload) => {
            state.exerciseDetailsId = payload;
        },
        addSets: (state, payload) => {
            for (const set of payload) {
                state.sets.push(set);    
            }
        },
        addWorkouts: (state, payload) => {
            for (const workout of payload) {
                state.workouts.push(workout);    
            }
        },
        setWorkoutDetailsId: (state, payload) => {
            state.workoutDetailsId = payload;
        },
        addPrograms: (state, payload) => {
            for (const program of payload) {
                state.programs.push(program);    
            }
        },
        setProgramDetailsId: (state, payload) => {
            state.programDetailsId = payload;
        }
    },
    actions: {
        async fetchExcercises(state) {
            const response = await fetch("https://localhost:44390/api/Exercises");
            const exercises = await response.json();
            state.commit("addExercises", exercises);
            console.log("FetchExercises from Db done...");
        },
        async fetchSets(state) {
            const response = await fetch("https://localhost:44390/api/Sets");
            if(!response.ok)
            { 
                console.log(`fetchSets from Db failed...!!!`);
            }
            else
            {
                const sets = await response.json();
                state.commit("addsets", sets);
                console.log("FetchSets from Db done...");
            }
        },
        async fetchWorkouts(state) {
            const response = await fetch("https://localhost:44390/api/Workouts");
            const workouts = await response.json();
            state.commit("addWorkouts", workouts);
            console.log("FetchWorkouts from Db done...");
        },
        async fetchPrograms(state) {
            const response = await fetch("https://localhost:44390/api/Programs");
            if(!response.ok)
            { 
                console.log(`fetchPrograms from Db failed...!!!`);
            }
            else
            {
                const programs = await response.json();
                state.commit("addPrograms", programs);
                console.log("FetchPrograms from Db done...");
            }
        }
    },
    getters: {
        getExerciseById: state => id => {
            return state.exercises.find(ex => ex.id === id);
        },
        getSetById: state => id => {
            return state.sets.find(s => s.id === id);
        },
        getWorkoutById: state => id => {
            return state.workouts.find(w => w.id === id);
        },
        getProgramById: state => id => {
            return state.programs.find(p => p.id === id);
        }
    }
});

export default store;