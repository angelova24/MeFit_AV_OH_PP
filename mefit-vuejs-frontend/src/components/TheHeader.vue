<script setup>
    
</script>

<template>
    <div>
        <nav>
            Navigation Bar:
            <router-link to="/dashboard" active-class="active">
                Dashboard
            </router-link>
            <router-link to="/goals" active-class="active">
                Goals
            </router-link>
            <router-link to="/exercises" active-class="active">
                Exercises
            </router-link>
            <router-link to="/workouts" active-class="active">
                Workouts
            </router-link>
        </nav>
    </div>
</template>

<style scoped>
    nav {
        display: flex;
        column-gap: 20px;
    }
</style>
