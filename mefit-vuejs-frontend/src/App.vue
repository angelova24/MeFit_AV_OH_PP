<script setup>
  import TheHeader from "./components/TheHeader.vue";
  import TheFooter from "./components/TheFooter.vue";
  import { useStore } from "vuex";
  import { onMounted, toRefs, reactive, computed, ref } from "vue";
  
  const props = defineProps(["keycloak"]);
  const { keycloak } = toRefs(props);
  const store = useStore();
  const baseUrl = computed(() => store.state.baseUrl);
  const isLoading = ref(false);

  const readData = () => {
    store.commit("resetState");
    console.log("reading data from Db...");
    isLoading.value = true;
    store.dispatch("fetchExcercises");
    store.dispatch("fetchSets");
    store.dispatch("fetchWorkouts");
    store.dispatch("fetchPrograms");
    store.dispatch("fetchUser")
      .then(user => {
        console.log("User received in promise:", user)
        if (user.profileId !== 0) {
          store.dispatch("fetchProfile", user.profileId)
          .then(profile => store.dispatch("fetchGoals", profile.goals))
        }
        else{
          console.log("you dont have a profile")
        }
        setUser();
        isLoading.value = false;
      });
  }

  const setUser = () => {
    store.commit("setToken", keycloak.value.token);
    console.log("Token:", keycloak.value.token)
    const userIdentity = keycloak.value.tokenParsed;
    store.commit("setUserIdentity", userIdentity);
    console.log("userIdentity:", userIdentity)
    if(userIdentity.role !== undefined) {
      let user = store.state.user;
      if(userIdentity.role.includes("contributor")) {
        store.commit("setUser", { ...user, isContributor: true } );
      }
      else {
        store.commit("setUser", { ...user, isContributor: false } );
      }
      user = store.state.user;
      if(userIdentity.role.includes("administrator")) {
        store.commit("setUser", { ...user, isAdmin: true } );
      }
      else {
        store.commit("setUser", { ...user, isAdmin: false } );
      }
    }
  };

  const updateToken = (minValidity) => {
    keycloak.value.updateToken(minValidity)
      .then(refreshed => {
        if (refreshed) {
          console.log('Token was successfully refreshed...');
        } 
        else {
          console.log('Token is still valid...');
        }
        setUser();
      })
      .catch(error => {
        console.log("Failed to refresh the token, or the session has expired: ", error);
      });
  }
  
  keycloak.value.onReady = authenticated => { 
    if(authenticated) {
      console.log("User was successfully authenticated...");
      setUser();
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
      redirectUri: window.location.protocol + "//" + window.location.host + "login"
    };
    console.log("current Url:", options.redirectUri);
    keycloak.value.logout(options)
      .then(parameter => {
        console.log("You have been logged out...", parameter);
      });
  }

  const onChangePassword = event => {
    const options = {
      loginHint: store.state.userIdentity.email,
      action: "UPDATE_PASSWORD",
      //redirectUri: window.location.protocol + "//" + window.location.host + window.location.pathname
    };
    console.log("current options:", options);
    keycloak.value.login(options)
      .then(parameter => {
        console.log("You are back from changing your password...", parameter);
      });
  }

</script>
 
<template>
  <div v-show="isLoading" class="important">
    Please wait while data is being processed...<br>
    <img src="./assets/Loading.gif" />
  </div>
  <div v-bind:class="{ loadInProgress: isLoading }">
    <header>
      <button v-on:click="generateToken">Generate new user Token</button>
      <button v-on:click="readData">Reload data from Db</button>
      <TheHeader 
        v-on:logout="onLogout"
        v-on:changepassword="onChangePassword"
      ></TheHeader>
    </header>
    <hr />
    <main>
      <router-view></router-view>
    </main>
    <hr />
    <footer>
      <TheFooter></TheFooter>
    </footer>
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

.loadInProgress {
  opacity: 0.3;
}

.important {
  position: fixed;
  left: 40%;
  top: 30%;
  background: white;
  opacity: 1;
  border: solid 2px black;
  z-index: 10;
  box-shadow: 5px 10px #888888;
  padding: 30px;
}

</style>
