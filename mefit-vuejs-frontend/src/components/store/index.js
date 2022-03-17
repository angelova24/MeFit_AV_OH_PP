import { createStore } from "vuex";

//const apiUrl = "https://localhost:44390/api";
const apiUrl = "https://mefitapi-va-pp-oh.azurewebsites.net/api";

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
        token: "",
        user: {
            id: 0,
            username: "",
            name: "",
            isContributor: false,
            isAdmin: false,
            profileId: 0
            //#region test data - only until API endpoint is available
            // id: 5,
            // username: "oliver hauck",
            // firstName: "Oliver",
            // lastName: "Hauck",
            // isContributor: false,
            // isAdmin: false,
            // profileId: 3
            //#endregion
        },
        profile: {
            id: 0,
            weight: 0,
            height: 0,
            medicalCondition: "",
            disabilities: "",
            addressLine1: "",
            addressLine2: "",
            postalCode: "",
            city: "",
            country: "",
            goalIds: []
            //#region test data - only until API endpoint is available
            // id: 3,
            // weight: 75,
            // height: 178,
            // medicalCondition: "healthy",
            // disabilities: "none",
            // addressLine1: "Schloßallee 17",
            // addressLine2: "please ring twice",
            // postalCode: "12345",
            // city: "Monopolis",
            // country: "BoardGameland",
            // goalIds: [1]
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
        programDetailsId: 0,
        goals: [
            //#region some test data - only until API is available
            // {
            //     id: 1,
            //     startDate: "2022-03-15T00:00:00.000Z",
            //     endDate: "2022-03-22T12:00:00.000Z",
            //     achieved: false,
            //     profileId: 3,
            //     workouts: [1, 2]
            // }
            //#endregion
        ],
        goalDetailsId: 0
    },
    mutations: {
        setUserIdentity: (state, payload) => {
            state.userIdentity = payload;
        },
        setToken: (state, payload) => {
            console.log("token in store will be set to:", payload);
            state.token = payload;
        },
        setUser: (state, payload) => {
            console.log("user will be set to:", payload);
            state.user = payload;
        },
        setProfile: (state, payload) => {
            console.log("profile will be set to:", payload);
            state.profile = payload;
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
        },
        addGoals: (state, payload) => {
            for (const goal of payload) {
                state.goals.push(goal);    
            }
        },
        setGoalDetailsId: (state, payload) => {
            state.goalDetailsId = payload;
        }
    },
    actions: {
        fetchExcercises: async store => {
            console.log("fetching exercises from Db...");
            const response = await fetch(`${apiUrl}/Exercises`, {
                    method: "GET",
                    headers: {
                        "Authorization": "Bearer " + store.state.token,
                        'Content-Type': 'application/json'
                    }
            });
            if(!response.ok)
            { 
                console.log(`FetchExercises from Db failed...!!!`);
            }
            else
            {
                const exercises = await response.json();
                store.commit("addExercises", exercises);
                console.log("FetchExercises from Db done...");
                console.log("Exercises received:", exercises);
            }
        },
        fetchSets: async store => {
            const response = await fetch(`${apiUrl}/Sets`, {
                method: "GET",
                headers: {
                    "Authorization": "Bearer " + store.state.token,
                    'Content-Type': 'application/json'
                }
            });
            if(!response.ok)
            { 
                console.log(`fetchSets from Db failed...!!!`);
            }
            else
            {
                const sets = await response.json();
                store.commit("addSets", sets);
                console.log("FetchSets from Db done...");
                console.log("Sets received:", sets);
            }
        },
        fetchWorkouts: async store => {
            const response = await fetch(`${apiUrl}/Workouts`, {
                method: "GET",
                headers: {
                    "Authorization": "Bearer " + store.state.token,
                    'Content-Type': 'application/json'
                }
            });
            if(!response.ok)
            { 
                console.log(`FetchWorkouts from Db failed...!!!`);
            }
            else
            {
                const workouts = await response.json();
                store.commit("addWorkouts", workouts);
                console.log("FetchWorkouts from Db done...");
                console.log("Workouts received:", workouts);
            }
        },
        fetchPrograms: async store => {
            const response = await fetch(`${apiUrl}/Programs`, {
                method: "GET",
                headers: {
                    "Authorization": "Bearer " + store.state.token,
                    'Content-Type': 'application/json'
                }
            });
            if(!response.ok)
            { 
                console.log(`fetchPrograms from Db failed...!!!`);
            }
            else
            {
                const programs = await response.json();
                store.commit("addPrograms", programs);
                console.log("FetchPrograms from Db done...");
                console.log("programs received:", programs);
            }
        },
        fetchProfile: async (store, id) => {
            const response = await fetch(`${apiUrl}/Profiles/${id}`, {
                method: "GET",
                headers: {
                    "Authorization": "Bearer " + store.state.token,
                    'Content-Type': 'application/json'
                }
            });
            if(!response.ok)
            { 
                console.log(`fetchProfile from Db failed...!!!`);
            }
            else
            {
                const profile = await response.json();
                store.commit("setProfile", profile);
                console.log("FetchProfile from Db done...");
                console.log("profile received:", profile);
                return profile;
            }
        },
        fetchUser: async store => {
            const response = await fetch(`${apiUrl}/user/`, {
                method: "GET",
                headers: {
                    "Authorization": "Bearer " + store.state.token
                }
            })
            .catch(reason => {
                console.log(`fetchUser from Db failed, because:`, reason);
            });
            if(response != undefined && !response.ok)
            { 
                //--- check whether response is 303
                console.log(`fetchUser from Db failed...!!! ResponseCode: ${response}`);
            }
            else
            {
                const user = await response.json();
                store.commit("setUser", user);
                console.log("FetchUser from Db done...");
                console.log("user received:", user);
                return user;
            }
        },
        fetchGoals: async (store, goalIds) => {
            const goals = [];
            for (const goalId of goalIds) {
                const response = await fetch(`${apiUrl}/goal/${goalId}`, {
                    method: "GET",
                    headers: {
                        "Authorization": "Bearer " + store.state.token,
                        'Content-Type': 'application/json'
                    }
                });
                if(!response.ok)
                { 
                    console.log(`FetchGoals for GoalId ${goalId} from Db failed...!!!`);
                }
                else
                {
                    const goal = await response.json();
                    goals.push(goal);                    
                }
            }
            store.commit("addGoals", goals);
            console.log("FetchGoals from Db done...");
            console.log("Goals received:", goals);
            return goals
        },
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
        },
        getGoalById: state => id => {
            const goal = state.goals.find(g => g.id === id);
            if(goal != undefined) {
                goal.startDate = (new Date(goal.startDate));
                goal.endDate = (new Date(goal.endDate));
            }
            return goal;
        }
    }
});

export default store;

// export function apiUrl() {
//     inject: ["apiUrl"]
// }