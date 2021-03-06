<script setup>
import { computed, ref } from "@vue/runtime-core";
import { useStore } from "vuex";
import { useRouter } from "vue-router";

const store = useStore();
const router = useRouter();
const username = computed(() => store.state.user.username);
const baseUrl = computed(() => store.state.baseUrl);
const isContributor = computed(() => store.state.user.isContributor);

const emits = defineEmits(["logout", "changepassword"]);

const selectedUserValue = ref("username");
const onSelectUserChange = (event) => {
  console.log(`new SelectUser value: ${event.target.value}`);
  switch (event.target.value) {
    case "showprofile":
      router.push(`${baseUrl.value}profile`);
      break;
    case "logout":
      emits("logout");
      break;
    case "changepassword":
      emits("changepassword");
      break;
  }
  selectedUserValue.value = "username";
};
</script>

<template> 
<div id="headerImage"></div> 
  <div>
    <nav>
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
            <select
              class="custom-select"
              v-on:change="onSelectUserChange"
              v-model="selectedUserValue"
            >
              <option value="username">logged in as: {{ username }}</option>
              <option value="showprofile">show my profile...</option>
              <option value="changepassword">change password...</option>
              <option value="logout">log me out...</option>
            </select>
          </div>
        </li>
      </ul>
    </nav>
    <nav v-if="isContributor">
      Contributor Area:
      <ul>
        <li>
          <router-link 
            v-bind:to="baseUrl + 'contribute/exercises'" 
            active-class="active"
          >
            manage Exercises
          </router-link>
        </li>
        <li>
          <router-link 
            v-bind:to="baseUrl + 'contribute/workouts'" 
            active-class="active"
          >
            manage Workouts
          </router-link>
        </li>
      </ul>
    </nav>
  </div>
  
</template>

<style scoped>
#headerImage {
  border:none;
  position: relative;
  right: 30px;
  padding:65px;
  background-image: url("../../src/assets/MeFit.png");
  background-repeat: no-repeat;
  background-size: 400px;
}
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
