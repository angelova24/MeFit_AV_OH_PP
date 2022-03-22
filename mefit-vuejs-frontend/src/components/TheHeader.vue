<script setup>
import { computed, ref } from "@vue/runtime-core";
import { useStore } from "vuex";
import { useRouter } from "vue-router";

const emits = defineEmits(["logout"]);

const store = useStore();
const username = computed(() => store.state.user.username);
const baseUrl = computed(() => store.state.baseUrl);
const router = useRouter();

const selectedUserValue = ref("username");
const onSelectUserChange = (event) => {
  console.log(`new SelectUser value: ${event.target.value}`);
  switch (event.target.value) {
    case "showprofile":
      router.push(`${baseUrl.value}profile`);
      selectedUserValue.value = "username";
      break;
    case "logout":
      emits("logout");
      break;
  }
};
</script>

<template>
  <div>
    <nav>
      Navigation Bar:
      <ul>
        <li>
          <router-link v-bind:to="baseUrl + 'dashboard'" active-class="active">
            Dashboard
          </router-link>
        </li>
        <li>
          <router-link v-bind:to="baseUrl + 'goals'" active-class="active">
            Goals
          </router-link>
        </li>
        <li>
          <router-link v-bind:to="baseUrl + 'programs'" active-class="active">
            Programs
          </router-link>
        </li>
        <li>
          <router-link v-bind:to="baseUrl + 'workouts'" active-class="active">
            Workouts
          </router-link>
        </li>
        <li>
          <router-link v-bind:to="baseUrl + 'exercises'" active-class="active">
            Exercises
          </router-link>
        </li>
        <li style="float: right">
          <div>
            <select class="custom-select" v-on:change="onSelectUserChange" v-model="selectedUserValue">
              <option value="username">logged in as: {{ username }}</option>
              <option value="showprofile">show my profile...</option>
              <option value="logout">log me out...</option>
            </select>
          </div>
        </li>
      </ul>
    </nav>
  </div>
</template>

<style scoped>
.custom-select {
  margin: 0%;
  color: aliceblue;
  padding: 14px 16px;
  background-color: #333;
}
.select-selected {
  background-color: #04aa6d;
}
.select-selected:after {
  position: absolute;
  content: "";
  top: 14px;
  right: 10px;
  width: 0;
  height: 0;
  border: 6px solid transparent;
  border-color: #fff transparent transparent transparent;
}
ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
  overflow: hidden;
  background-color: #333;
}

li {
  float: left;
  border-right: 1px solid #bbb;
}

li:last-child {
  border-right: none;
}

li a {
  display: block;
  color: white;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
}

li a:hover:not(.active) {
  background-color: #111;
}

.active {
  background-color: #04aa6d;
}
</style>
