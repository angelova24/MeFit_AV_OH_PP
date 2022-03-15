<script setup>
  import TheHeader from "./components/TheHeader.vue";
  import TheFooter from "./components/TheFooter.vue";
  import { useStore } from "vuex";
  import { onMounted, toRefs, reactive } from "vue";
  
  const props = defineProps(["keycloak"]);
  const { keycloak } = toRefs(props);
  const store = useStore();
  
  keycloak.value.onReady = authenticated => { 
    if(authenticated) {
      keycloak.value.loadUserProfile()
        .then(profile => {
          alert(JSON.stringify(profile, null, "  "));
          store.commit("setUserIdentity", profile);
          store.commit("setToken", keycloak.value.token);
        })
        .catch(error => {
          alert("Failed to load user profile...");
          console.log("Failed to load user profile:", error);
        }); 
    }
  };

  keycloak.value.onTokenExpired = parameter => {
    alert("Token is expired...");
    console.log("Token expired:", parameter);
    keycloak.value.updateToken(5);
    store.commit("SetToken", keycloak.value.token);
  }
  
  onMounted(() => {
    store.dispatch("fetchExcercises");
    store.dispatch("fetchSets");
    store.dispatch("fetchWorkouts");
    store.dispatch("fetchPrograms");
  })

  const generateToken = () => { 
    keycloak.value.updateToken(50000)
    .then(function(refreshed) {
        if (refreshed) {
            alert('Token was successfully refreshed');
            store.commit("SetToken", keycloak.value.token);
            console.log("Token:", keycloak.value.token)
        } else {
            alert('Token is still valid');
        }
    })
    .catch(error => {
        alert("Failed to refresh the token, or the session has expired: ", error);
    });
  }

</script>
 
<template>
  <div>
    <button v-on:click="generateToken">Generate Token</button>
    <TheHeader></TheHeader>
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
