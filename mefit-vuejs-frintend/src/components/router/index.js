import { createRouter, createWebHistory } from "vue-router";
import UserDashboard from "../UserProfiles/UserDashboard.vue"

const routes = [
    {
        path: "",
        component: UserDashboard
    }
]

export default createRouter({
    history: createWebHistory(),
    routes
})