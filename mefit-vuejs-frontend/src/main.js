import Keycloak from 'keycloak-js';
import { createApp } from 'vue';
import App from './App.vue';
import router from "./components/router"
import store from "./components/store"

//#region Keycloak initialization
const keycloakInitOptions =
    {
        "clientId": "MeFit_VA_OH_PP",
        "realm": "MeFit_OH",
        "auth-server-url": "https://mefit-keycloak1.herokuapp.com/auth",
        "url": "https://mefit-keycloak1.herokuapp.com/auth",
        "ssl-required": "external",
        "resource": "MeFit_VA_OH_PP",
        "public-client": true,
        "confidential-port": 0
    }
const keycloak = new Keycloak(keycloakInitOptions);

keycloak.init({
    onLoad: "login-required",
    silentCheckSsoRedirectUri: window.location.origin + "/silent-check-sso.html"
})
// keycloak.init({ 
//     onLoad: "check-sso",
//     silentCheckSsoRedirectUri: window.location.origin + "/silent-check-sso.html"
// })
//keycloak.init({ })
    .then(authenticated => {
        //alert(authenticated ? "authenticated" : "not authenticated");
    })
    .catch(error => {
        alert("failed to initialize: " + error);
        console.log("failed to initialize:", error);
    });
//#endregion

createApp(App, {keycloak: keycloak})
    .use(router)
    .use(store)
    //.use(keycloak.middleware)
    .mount('#app');
