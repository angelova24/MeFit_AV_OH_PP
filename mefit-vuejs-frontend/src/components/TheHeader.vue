<script setup>
    import { computed, ref } from '@vue/runtime-core';
    import { useStore } from 'vuex';
    import { useRouter } from 'vue-router';

    const store = useStore();
    const username = computed(() => store.state.user.username);
    const router = useRouter();

    const selectedUserValue = ref("username");
    const onSelectUserChange = (event) => {
        console.log(`new SelectUser value: ${event.target.value}`);
        if(event.target.value == "showprofile")
        {
            router.push("/profile")
            selectedUserValue.value = "username";
        }
    }


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
            <select v-on:change="onSelectUserChange" v-model="selectedUserValue">
                <option value="username">logged in as: {{ username }}</option>
                <option value="showprofile">show my profile</option>
            </select>
            
        </nav>
    </div>
</template>

<style scoped>
    nav {
        display: flex;
        column-gap: 20px;
    }
</style>
