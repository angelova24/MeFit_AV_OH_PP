<script setup>
    import { computed } from '@vue/runtime-core';
    import { useStore } from 'vuex';

    const store = useStore();
    const username = computed(() => store.state.user.username);

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
            <router-link to="/programs" active-class="active">
                Programs
            </router-link>
            <router-link to="/workouts" active-class="active">
                Workouts
            </router-link>
            <router-link to="/exercises" active-class="active">
                Exercises
            </router-link>
            logged in as: {{ username }}
        </nav>
    </div>
</template>

<style scoped>
    nav {
        display: flex;
        column-gap: 20px;
    }
</style>
