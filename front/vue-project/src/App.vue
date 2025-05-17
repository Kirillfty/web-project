<script setup lang="ts">
import { RouterView } from 'vue-router';

</script>
<style scoped>
.aside{
   width:100%;
   height:10vh;
   background-color: rgb(20, 20, 20);
   display: flex;
   justify-content: space-around;
   position:fixed;
   align-items: center;
   bottom:0;
   z-index:3;
}
#link{
   color:aliceblue;
   text-decoration: none;
   font-size: 12px;
}
.icon{
   width:6vh;
   height:6vh;
}
.icons{
  display: flex;
  flex-direction: column;
  text-align: center;
}
</style>

<template>
 <div class="container-a">
   <div>
      <RouterView />
   </div>
   <aside class="aside">
      <div class="icons">
         <img src="./assets/clubs.png" alt="" class="icon">
         <router-link to="/clubs" id="link">Clubs</router-link>
      </div>
      <div class="icons">
         <img src="./assets/user.png" alt="" class="icon">
         <router-link to="/user" id="link">User</router-link>
      </div>
      <div class="icons">
         <img src="./assets/search.png" alt="" class="icon">
         <router-link to="/search" id="link">search</router-link>
      </div>
      <div class="icons">
         <img src="./assets/login-removebg-preview.png" alt="" class="icon">
         <router-link to="/" id="link">login</router-link>
      </div>
      <div class="icons" to="/register">
         <img src="./assets/register-removebg-preview.png" alt="" class="icon">
         <router-link to="/register" id="link">register</router-link>
      </div>
   </aside>
 </div>
</template>

