import { createRouter, createWebHistory } from "vue-router";
import UserDashboard from "../UserProfiles/UserDashboard.vue";
import LoginPage from "../UserProfiles/LoginPage.vue";
import GoalsPage from "../Goals/GoalsPage.vue";
import ExercisesPage from "../Exercises/ExercisesPage.vue";
import ExercisesManagePage from "../Exercises/ExercisesManagePage.vue";
import WorkoutPage from "../Workouts/WorkoutsPage.vue";
import WorkoutsManagePage from "../Workouts/WorkoutsManagePage.vue";
import ProgramsPage from "../Programs/ProgramsPage.vue";
import ProfilePage from "../UserProfiles/ProfilePage.vue";
import store from "../store";

const baseUrl = "/" + window.location.pathname.split("/")[1] + "/";
console.log("baseUrl:", baseUrl);
console.log("store:", store);
store.commit("setBaseUrl", baseUrl);
const routes = [
    {
        path: `${baseUrl}`,
        component: UserDashboard
    },
    {
        path: `${baseUrl}dashboard`,
        component: UserDashboard
    },
    {
        path: `/login`,
        component: LoginPage
    },
    {
        path: `${baseUrl}goals`,
        component: GoalsPage
    },
    {
        path: `${baseUrl}exercises`,
        component: ExercisesPage
    },
    {
        path: `${baseUrl}contribute/exercises`,
        component: ExercisesManagePage
    },
    {
        path: `${baseUrl}workouts`,
        component: WorkoutPage
    },
    {
        path: `${baseUrl}contribute/workouts`,
        component: WorkoutsManagePage
    },
    {
        path: `${baseUrl}programs`,
        component: ProgramsPage
    },
    {
        path: `${baseUrl}profile`,
        component: ProfilePage
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from) => {
    console.log(`page redirection occured from: ${from.fullPath} to: ${to.fullPath}`);
    //--- ensure non-contributors cannot access contributor pages
    if (!store.state.user.isContributor && to.fullPath.includes("/contribute/")) {
        return { path: `${baseUrl}dashboard`};
    }
    //--- ensure users without profiles are redirected to profilespage to enter data
    if ((Object.keys(store.state.profile).length === 0 || store.state.profile.id === 0) && store.state.user.id != 0 && !to.fullPath.startsWith(baseUrl + "profile")) {
        return { path: `${baseUrl}profile`};
    }
})

export default router;