import Vue from 'vue'
import VueRouter from 'vue-router'
Vue.use(VueRouter)

import About from './views/About.vue'
import Home from './views/Home.vue'
import Workshop from './components/Workshop.vue'

const router = new VueRouter({
    mode: 'history',
    routes: [
        {
            path: '/',
            name: 'home',
            component: Home
        },
        {
            path: '/about',
            name: 'about',
            component: About
        },
        {
            path: '/workshop',
            name: 'workshop',
            component: Workshop
        }
    ]
});

export default router;