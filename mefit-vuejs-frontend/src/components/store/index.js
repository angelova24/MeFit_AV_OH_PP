import { createStore } from "vuex";

const store = createStore({
    state: {
        userIdentity: {
            id: "",
            username: "",
            firstName: "",
            lastName: "",
            email: "",
            emailVerified: ""
        },
        user: {
            //#region test data - only until API endpoint is available
            id: 1,
            username: "oliver hauck",
            firstName: "Oliver",
            lastName: "Hauck",
            disabilities: "none"
            //#endregion
        },
        profile: {
            //#region test data - only until API endpoint is available
            id: 1,
            weight: 78.05,
            height: 1.7,
            medicalCondition: "healthy",
            isContribuor: false,
            isAdmin: false,
            addressLine1: "Schloßallee 17",
            addressLine2: "please ring twice",
            postalCode: "12345",
            city: "Monopolis",
            country: "BoardGameland"
            //#endregion
        },
        exercises: [],
        exerciseDetailsId: 0,
        sets: [
            //#region some test data
            // {
            //     id: 1,
            //     exerciseRepetitions: 10,
            //     exerciseId: 1
            // },
            // {
            //     id: 2,
            //     exerciseRepetitions: 10,
            //     exerciseId: 2
            // },
            // {
            //     id: 3,
            //     exerciseRepetitions: 10,
            //     exerciseId: 3
            // },
            // {
            //     id: 4,
            //     exerciseRepetitions: 10,
            //     exerciseId: 4
            // },
            // {
            //     id: 5,
            //     exerciseRepetitions: 10,
            //     exerciseId: 5
            // },
            // {
            //     id: 6,
            //     exerciseRepetitions: 15,
            //     exerciseId: 1
            // },
            // {
            //     id: 7,
            //     exerciseRepetitions: 15,
            //     exerciseId: 2
            // },
            // {
            //     id: 8,
            //     exerciseRepetitions: 15,
            //     exerciseId: 3
            // },
            // {
            //     id: 9,
            //     exerciseRepetitions: 15,
            //     exerciseId: 4
            // },
            // {
            //     id: 10,
            //     exerciseRepetitions: 15,
            //     exerciseId: 5
            // }
            //#endregion
        ],
        workouts: [],
        workoutDetailsId: 0,
        programs: [
            //#region some test data only until API endpoint is available
            // {
            //     id: 1,
            //     name: "Get in summer shape",
            //     category: "loose fat",
            //     workouts: [1, 2]
            // },
            // {
            //     id: 2,
            //     name: "Covid - do not leave home",
            //     category: "no gym",
            //     workouts: [3]
            // },
            // {
            //     id: 3,
            //     name: "Conquer my weaker self",
            //     category: "beginner"
            // }
            //#endregion
        ],
        programDetailsId: 0
    },
    mutations: {
        setUserIdentity: (state, payload) => {
            state.userIdentity = payload;
        },
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
        fetchExcercises: async state => {
            const response = await fetch("https://localhost:44390/api/Exercises");
            if(!response.ok)
            { 
                console.log(`FetchExercises from Db failed...!!!`);
            }
            else
            {
                const exercises = await response.json();
                state.commit("addExercises", exercises);
                console.log("FetchExercises from Db done...");
                console.log("Exercises received:", exercises);
            }
        },
        fetchSets: async state => {
            const response = await fetch("https://localhost:44390/api/Sets");
            if(!response.ok)
            { 
                console.log(`fetchSets from Db failed...!!!`);
            }
            else
            {
                const sets = await response.json();
                state.commit("addSets", sets);
                console.log("FetchSets from Db done...");
                console.log("Sets received:", sets);
            }
        },
        fetchWorkouts: async state => {
            const response = await fetch("https://localhost:44390/api/Workouts");
            if(!response.ok)
            { 
                console.log(`FetchWorkouts from Db failed...!!!`);
            }
            else
            {
                const workouts = await response.json();
                state.commit("addWorkouts", workouts);
                console.log("FetchWorkouts from Db done...");
                console.log("Workouts received:", workouts);
            }
        },
        fetchPrograms: async state => {
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