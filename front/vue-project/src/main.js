import '../src/assets/main.css'
import { createApp } from 'vue'
import App from './App.vue'
import { createWebHistory, createRouter } from 'vue-router'

import login from './login.vue'
import register from './register.vue'
import clubs from './clubs.vue'
import userpage from './usersPage.vue'
import search from './Search.vue'
const routes = [
  { path: '/', component: login },
  { path: '/clubs', component: clubs },
  { path: '/register', component: register },
  { path: '/user', component: userpage },
  { path: '/search', component: search }
]
const router = createRouter({
  history: createWebHistory(),
  routes
})
createApp(App).use(router).mount('#app')
