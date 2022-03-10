import { createRouter, createWebHistory } from "vue-router";
import UserDashboard from "../UserProfiles/UserDashboard.vue";
import LoginPage from "../UserProfiles/LoginPage.vue";
import GoalsPage from "../Goals/GoalsPage.vue";
import ExercisesPage from "../Exercises/ExercisesPage.vue";

const routes = [
    {
        path: "/dashboard",
        component: UserDashboard
    },
    {
        path: "/login",
        component: LoginPage
    },
    {
        path: "/goals",
        component: GoalsPage
    },
    {
        path: "/exercises",
        component: ExercisesPage
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from) => {
    console.log(`from: ${from.fullPath} to: ${to.fullPath}`);
    let isAuthenticated = true;
    if(!isAuthenticated && to.fullPath !== "/login") {
        return { path: "/login"};
    }
})

export default router;