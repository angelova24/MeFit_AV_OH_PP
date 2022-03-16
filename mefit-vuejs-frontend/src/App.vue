<script setup>
  import TheHeader from "./components/TheHeader.vue";
  import TheFooter from "./components/TheFooter.vue";
  import { useStore } from "vuex";
  import { onMounted, toRefs, reactive } from "vue";
  
  const props = defineProps(["keycloak"]);
  const { keycloak } = toRefs(props);
  const store = useStore();
  
  const readData = () => {
    console.log("reading data from Db...");
    store.dispatch("fetchExcercises");
    store.dispatch("fetchSets");
    store.dispatch("fetchWorkouts");
    store.dispatch("fetchPrograms");
    store.dispatch("fetchUser")
      .then(user => {
        console.log("User received in promise:", user)
        store.dispatch("fetchProfile", user.profileId)
          .then(profile => store.dispatch("fetchGoals", profile.goals))
      });
  }

  const updateToken = (minValidity) => {
    keycloak.value.updateToken(minValidity)
      .then(refreshed => {
        if (refreshed) {
          console.log('Token was successfully refreshed...');
        } 
        else {
          console.log('Token is still valid...');
        }
        store.commit("setToken", keycloak.value.token);
        console.log("Token:", keycloak.value.token)
      })
      .catch(error => {
        console.log("Failed to refresh the token, or the session has expired: ", error);
      });
  }
  
  keycloak.value.onReady = authenticated => { 
    if(authenticated) {
      console.log("User was successfully authenticated...");
      //--- write current token in store
      store.commit("setToken", keycloak.value.token);
      readData();
    }
    else
    {
      console.log("User authentication failed...");
    }
  };

  keycloak.value.onTokenExpired = parameter => {
    console.log("Token expired:", parameter);
    updateToken(5);
  }

  const generateToken = () => { 
    updateToken(50000);
  }

  const onLogout = event => {
    const options = {
      redirectUri: window.location.protocol + "//" + window.location.host + "/login"
    };
    console.log("current Url:", options.redirectUri);
    keycloak.value.logout(options)
      .then(parameter => {
        console.log("You have been logged out...", parameter);
      });
  }

</script>
 
<template>
  <div>
    <button v-on:click="generateToken">Generate Token</button>
    <button v-on:click="readData">Read data from Db</button>
    <TheHeader v-on:logout="onLogout"></TheHeader>
    <hr />
    <router-view></router-view>
    <hr />
    <TheFooter></TheFooter>
  </div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
