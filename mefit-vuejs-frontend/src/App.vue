<script setup>
  import TheHeader from "./components/TheHeader.vue";
  import TheFooter from "./components/TheFooter.vue";
  import { useStore } from "vuex";
  import { onMounted, toRefs } from "vue";
  
  const props = defineProps(["keycloak"]);
  const { keycloak } = toRefs(props);
  const store = useStore();
  keycloak.value.onReady = authenticated => { 
    if(authenticated) {
      keycloak.value.loadUserProfile()
        .then(profile => {
          alert(JSON.stringify(profile, null, "  "));
          store.commit("setUser", profile);
        })
        .catch(error => {
          alert("Failed to load user profile...");
          console.log("Failed to load user profile:", error);
        }); 
    }
  };
  //keycloak.value.updateToken(5);

  onMounted(() => {
    store.dispatch("fetchExcercises");
    store.dispatch("fetchSets");
    store.dispatch("fetchWorkouts");
    store.dispatch("fetchPrograms");
  })

  

</script>

<template>
  <div>
    Keycloak: {{ keycloak }}
    Token: {{ keycloak.token }}
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
