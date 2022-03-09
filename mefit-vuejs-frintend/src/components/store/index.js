import { createStore } from "vuex";

const store = createStore({
    state: {
        user: {
            userid: 0,
            username: ""
        },
        profile: {
            
        },
        exercises: [ 
            {
                id: 1,
                name: "Sit-up",
                description: "It begins with lying with the back on the floor, typically with the arms across the chest or hands behind the head and the knees and toes bent in an attempt to reduce stress on the back muscles and spine, and then elevating both the upper and lower vertebrae from the floor until everything superior to the buttocks is not touching the ground.",
                targetMuscleGroup: " abdominal muscles"
            },
            {
                id: 2,
                name: "Push-up",
                description: "A push-up is a common calisthenics exercise beginning from the prone position and then raising and lowering the body using the arms.",
                targetMuscleGroup: "Biceps, Forearms"
            }
        ]
    },
    mutations: {
        addExercises: (state, payload) => {
            for (const exercise of payload) {
                state.exercises.push(exercise);    
            }
        }        
    },
    actions: {
        async fetchExcercises(state) {
            const response = await fetch("http://acnhapi.com/v1/fish/1");
            const exercises = await response.json();
            state.commit("addExercises", exercises);
            console.log("FetchUser done...");
        }
    },
    getters: {
        getExercisebyName: state => name => {
            return state.exercises.filter(ex => ex.name == name);
        },
        getExercises: state => {
            return state.exercises;
        }
    }
});

export default store;