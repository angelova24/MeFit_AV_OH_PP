<script setup>
    import {ref} from "vue";
    let username = ref("");
    const onLoginClick = event => {
        console.log(`Button ${event.target.value} was clicked...`);
        console.log(`entered username: ${username.value}`);
        
    }
</script>

<template>
    <div>
        <header>
            Login Page
        </header>
        <main>
            <label>UserName: </label>
            <input type="text" v-model.trim="username" placeholder="username goes here...">
            <button type="submit" v-on:click="onLoginClick">Login</button>
        </main>
    </div>
</template>

<style scoped>

</style>
