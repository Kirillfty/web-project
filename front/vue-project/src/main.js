import '../src/assets/main.css'
import '../src/assets/club-card.css'
import { createApp } from 'vue'
import App from './App.vue'
import { createWebHistory, createRouter } from 'vue-router'

import login from './login.vue'
import register from './register.vue'
import clubs from './clubs.vue'
import userpage from './usersPage.vue'
import search from './Search.vue'
import clubPage from './clubPage.vue'
const routes = [
  { path: '/', component: login },
  { path: '/clubs', component: clubs },
  { path: '/register', component: register },
  { path: '/user', component: userpage },
  { path: '/search', component: search },
  { path: '/club-page', component: clubPage }
]
const router = createRouter({
  history: createWebHistory(),
  routes
})
createApp(App).use(router).mount('#app')
