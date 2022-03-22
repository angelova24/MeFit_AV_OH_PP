<script setup>
    import { computed, ref } from '@vue/runtime-core';
    import { useStore } from 'vuex';
    import { useRouter } from 'vue-router';

    const emits = defineEmits(["logout"]);

    const store = useStore();
    const username = computed(() => store.state.user.username);
    const baseUrl = computed(() => store.state.baseUrl);
    const router = useRouter();

    const selectedUserValue = ref("username");
    const onSelectUserChange = (event) => {
        console.log(`new SelectUser value: ${event.target.value}`);
        switch(event.target.value) {
            case "showprofile":
                router.push(`${baseUrl.value}profile`)
                selectedUserValue.value = "username";
                break;
            case "logout":
                emits("logout");
                break;
        }
    }


</script>

<template>
    <div>
        <nav>
            Navigation Bar:
            <router-link v-bind:to="baseUrl + 'dashboard'" active-class="active">
                Dashboard
            </router-link>
            <router-link v-bind:to="baseUrl + 'goals'" active-class="active">
                Goals
            </router-link>
            <router-link v-bind:to="baseUrl + 'programs'" active-class="active">
                Programs
            </router-link>
            <router-link v-bind:to="baseUrl + 'workouts'" active-class="active">
                Workouts
            </router-link>
            <router-link v-bind:to="baseUrl + 'exercises'" active-class="active">
                Exercises
            </router-link>
            <select v-on:change="onSelectUserChange" v-model="selectedUserValue">
                <option value="username">logged in as: {{ username }}</option>
                <option value="showprofile">show my profile...</option>
                <option value="logout">log me out...</option>
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
