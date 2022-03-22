import { createRouter, createWebHistory } from "vue-router";
import UserDashboard from "../UserProfiles/UserDashboard.vue";
import LoginPage from "../UserProfiles/LoginPage.vue";
import GoalsPage from "../Goals/GoalsPage.vue";
import ExercisesPage from "../Exercises/ExercisesPage.vue";
import WorkoutPage from "../Workouts/WorkoutsPage.vue";
import ProgramsPage from "../Programs/ProgramsPage.vue";
import ProfilePage from "../UserProfiles/ProfilePage.vue";
import store from "../store";

const baseUrl = "/" + window.location.pathname.split("/")[1] + "/";
console.log("baseUrl:", baseUrl);
console.log("store:", store);
store.commit("setBaseUrl", baseUrl);
const routes = [
    {
        path: `${baseUrl}/dashboard`,
        component: UserDashboard
    },
    {
        path: `/login`,
        component: LoginPage
    },
    {
        path: `${baseUrl}/goals`,
        component: GoalsPage
    },
    {
        path: `${baseUrl}/exercises`,
        component: ExercisesPage
    },
    {
        path: `${baseUrl}/workouts`,
        component: WorkoutPage
    },
    {
        path: `${baseUrl}/programs`,
        component: ProgramsPage
    },
    {
        path: `${baseUrl}/profile`,
        component: ProfilePage
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from) => {
    console.log(`page redirection occured from: ${from.fullPath} to: ${to.fullPath}`);
    let isAuthenticated = true;
    if(!isAuthenticated && to.fullPath !== `${baseUrl}login`) {
        return { path: `${baseUrl}login`};
    }
})

export default router;