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
                state.workouts.push(program);    
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
        async fetchWorkouts(state) {
            const response = await fetch("https://localhost:44390/api/Workouts");
            const workouts = await response.json();
            state.commit("addWorkouts", workouts);
            console.log("FetchWorkouts from Db done...");
        },
        async fetchPrograms(state) {
            const response = await fetch("https://localhost:44390/api/Programs");
            const programs = await response.json();
            state.commit("addPrograms", programs);
            console.log("FetchPrograms from Db done...");
        }
    },
    getters: {
        getExerciseById: state => id => {
            return state.exercises.find(ex => ex.id === id);
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