import './bootstrap';
import 'vuetify/dist/vuetify.min.css'
import Vue from 'vue'
import Vuetify from 'vuetify'
import router from './router.js';

import App from './App.vue'
import Nav from './components/Nav.vue';

Vue.use(Vuetify)

Vue.component('globalNav', Nav);

new Vue({
    router,
    render: h => h(App)
}).$mount('#app');
