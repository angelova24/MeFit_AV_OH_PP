<script setup>
    import { ref, reactive } from "vue";
    const message = ref("Welcome message");
    const people = reactive([]);
    people.push("Olli");
    people.push("Nina");
    people.push("Hannah");
    const username = ref("Oliver");
    const reverseMe = () => { 
        message.value = message.value.split("").reverse().join("");
    }

    const result = ref("");
    const onSelectChange = event => {
        result.value = event.target.value;
    }
</script>

<template>
    <div>
        Hello from Dashboard!
        {{message}}
        <input type="text" v-bind:placeholder="message">
        <div v-if="message != ''">
            It's a msg!
        </div>
        <div v-else>
            no msg at all!
        </div>
        <ul>
            <li v-for="(person, index) in people" v-bind:key="index">
                {{index}} - {{person}}
            </li>
        </ul>
        <input type="text" v-model="username">
        <p>You typed: {{username}}</p>
        <button v-on:click="reverseMe">reverse</button>
        <button @click="reverseMe">reverse2</button>
        <select v-on:change="onSelectChange">
            <option value="">Make your choice</option>
            <option v-for="person in people" :key="person">
                {{person}}
            </option>
        </select>
        <label>{{ result }}</label>
    </div>
</template>

<style scoped>

</style>
