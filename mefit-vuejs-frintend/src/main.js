import { createApp } from 'vue';
import App from './App.vue';
import router from "./components/router"
import store from "./components/store"

createApp(App)
    .use(router)
    .use(store)
    .mount('#app');
