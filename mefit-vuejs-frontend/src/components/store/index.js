import { createStore } from "vuex";

//const apiUrl = "https://localhost:5001/api";
//const apiUrl = "https://localhost:49153/api";
//const apiUrl = "https://localhost:44390/api";
const apiUrl = "https://mefitapi-va-pp-oh.azurewebsites.net/api";

const store = createStore({
    state: {
        baseUrl: "",
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
            profileId: -1
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
        goalDetailsId: 0,
        goalWorkoutDetailsId: 0
    },
    mutations: {
        resetState: (state) => {
            state.exerciseDetailsId = 0;
            state.exercises = [];
            state.goalDetailsId = 0;
            state.goalWorkoutDetailsId = 0;
            state.goals = [];
            state.profile = {};
            state.programDetailsId = 0;
            state.programs = [];
            state.sets = [];
            state.user = {};
            state.workoutDetailsId = 0;
            state.workouts = [];
        },
        setBaseUrl: (state, payload) => {
            state.baseUrl = payload;
        },
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
        setCompletedWorkout: (state, payload) =>{
            const {goal, workout} = payload;
            const storeGoal = state.goals.find(g => g.id === goal.id);
            const storeWorkout = storeGoal.workouts.find(w => w.workoutId === workout.workoutId)
            storeWorkout.complete = true;
        },
        setGoalWorkoutComplete: (state, payload) =>{
            const goalWorkout = payload;
            const storeGoal = state.goals.find(g => g.id === goalWorkout.goalId);
            const storeWorkout = storeGoal.workouts.find(w => w.workoutId === goalWorkout.workoutId)
            storeWorkout.complete = true;
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
        addGoal: (state, payload) => {
            state.goals.push(payload);    
        },
        setGoalDetailsId: (state, payload) => {
            state.goalDetailsId = payload;
        },
        setGoalWorkoutDetailsId: (state, payload) => {
            state.goalWorkoutDetailsId = payload;
        },
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
        addExercise: async (store, newExercise) => {
            console.log("adding Exercise to Db:", newExercise);
            let addedExercise = newExercise;
            const response = await fetch(`${apiUrl}/exercise`, {
                method: "POST",
                headers: {
                    "Authorization": "Bearer " + store.state.token,
                    "Content-Type": "application/json",
                },                
                body: JSON.stringify(newExercise)
            })
            .catch(reason => {
                console.log("addExercise to DB failed, because:", reason);
            });
            if(!response.ok)
            {
                console.log("addExercise to DB failed...!!!", newExercise);
            }
            else{
                addedExercise = await response.json();
                console.log("addExercise to DB done...", addedExercise);                
                store.commit("addExercise", addedExercise);   //--- mutation must be created, but missing ownerId in API endpoint yet... 
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
            const response = await fetch(`${apiUrl}/profile/${id}`, {
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
            const response = await fetch(`${apiUrl}/user`, {
                method: "GET",
                headers: {
                    "Authorization": "Bearer " + store.state.token,
                    'Content-Type': 'application/json'
                }
            })
            .catch(reason => {
                console.log(`fetchUser from Db failed, because:`, reason);
            });
            if(response != undefined && !response.ok)
            { 
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
        addGoal: async (store, payload) => {
            const { goal, workoutIds } = payload;
            console.log("calling addGoal store action:", goal, workoutIds);
            let addedGoal = goal;
            const response = await fetch(`${apiUrl}/goal/`, {
                    method: "POST",
                    headers: {
                        "Authorization": "Bearer " + store.state.token,
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(goal)
            });
            if(!response.ok)
            { 
                console.log(`addGoal to Db failed...!!!`, goal);
            }
            else
            {
                addedGoal = await response.json();
                console.log(`addGoal to Db done...`, addedGoal);
                const workoutResponse = await fetch(`${apiUrl}/goal/${addedGoal.id}/AddWorkouts`, {
                    method: "PATCH",
                    headers: {
                        "Authorization": "Bearer " + store.state.token,
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(workoutIds)
                });
                if(!workoutResponse.ok)
                { 
                    console.log(`addWorkouts for Goal to Db failed...!!!`, addedGoal);
                }
                else
                {
                    //--- workouts for new goal were successfully written to Db
                    console.log("add workoutIds for new goal to Db done:", workoutIds);
                    addedGoal.workouts = workoutIds;
                    console.log("add new goal to store:", addedGoal);
                    store.commit("addGoal", addedGoal);
                } 
            }    
        },
        addProfile: async (store, newProfile) => {
            let addedProfile = newProfile;
            console.log(newProfile);
            const response = await fetch(`${apiUrl}/profile`, {
                method: "POST",
                headers: {
                    "Authorization": "Bearer " + store.state.token,
                    "Content-Type": "application/json",
                    
                },                
                body: JSON.stringify(newProfile)
            });
            if(!response.ok)
            {
                console.log("addProfile to DB failed...!!!", newProfile)                
            }
            else{
                addedProfile = await response.json();
                console.log("addProfile to DB done...", addedProfile);                
                store.commit("setProfile", addedProfile);
            }
        },
        updateProfile: async (store, profile) => {
            let updatedProfile = profile;
            console.log(profile);
            const response = await fetch(`${apiUrl}/profile/${profile.id}`, {
                method: "PUT",
                headers: {
                    "Authorization": "Bearer " + store.state.token,
                    "Content-Type": "application/json",
                    
                },                
                body: JSON.stringify(profile)
            });
            if(!response.ok)
            {
                console.log("updateProfile to DB failed...!!!", profile)                
            }
            else{                
                console.log("updateProfile to DB done...", updatedProfile);                
                store.commit("setProfile", updatedProfile);
            }
        },
        updateGoalWorkout: async (store, goalWorkoutData) => {           
            const {goal, workout} = goalWorkoutData;
            const response = await fetch(`${apiUrl}/goal/${goal.id}/workout/${workout.workoutId}/SetCompleted`, {
                method: "PATCH",
                headers: {
                    "Authorization": "Bearer " + store.state.token,
                    "Content-Type": "application/json"
                },                                
            });
            if(!response.ok)
            {
                console.log("updateGoalWorkout to DB failed...!!!", workout)                
            }
            else{                
                console.log("updatedGoalWorkout in DB done...", workout);                
                store.commit("setCompletedWorkout", goalWorkoutData);
            }
        },
        setGoalWorkoutComplete: async (store, goalWorkout) => {           
            const response = await fetch(`${apiUrl}/goal/${goalWorkout.goalId}/workout/${goalWorkout.workoutId}/SetCompleted`, {
                method: "PATCH",
                headers: {
                    "Authorization": "Bearer " + store.state.token,
                    "Content-Type": "application/json"
                },                                
            })
            .catch(reason => {
                console.log("setGoalWorkoutComplete to DB failed!!!:", reason)
            });
            if(!response.ok)
            {
                console.log("setGoalWorkoutComplete to DB failed...!!!", goalWorkout)
            }
            else{                
                console.log("setGoalWorkoutComplete in DB done...", goalWorkout);                
                store.commit("setGoalWorkoutComplete", goalWorkout);
            }
        }
    },
    getters: {
        getExerciseById: state => id => {
            return state.exercises.find(ex => ex.id === id);
        },
        getExercisesByOwnerId: state => id => {
            return state.exercises.filter(ex => ex.ownerId === id);
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
        },
        getCurrentGoals: state => {
            const currentGoals = state.goals.filter(g => (new Date(g.endDate) >= new Date()));
            console.log("current goals:", currentGoals);
            // if(currentGoals !== undefined) {
            //     for (const goal of currentGoals) {
            //         goal.startDate = (new Date(goal.startDate));
            //         goal.endDate = (new Date(goal.endDate));
            //     }
            // }
            return currentGoals;
        }
    }
});

export default store;
