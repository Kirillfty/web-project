<template>
    <div class="header">
        <img src="./assets/user.png" alt="" class="logo">
        <p>{{userData.firstName}}</p>
        <p>{{userData.lastName}}</p>
        <p>{{userData.nickName}}</p>
    </div>
</template>


<script setup>
import {ref,onMounted} from 'vue'
import axios from 'axios'
let userId = localStorage.getItem('userId');
let userData = ref('');


async function getUser() {
  await axios
    .get("https://localhost:7210/api/users/" + userId)
    .then(function (responce) {
      console.log(responce.data);
      userData.value = responce.data;
      console.log("user " + userData);
      return userData;
    });
}
onMounted(async function(){
    await getUser();
})
</script>


<style scoped>
.logo{
    width:15vh;
    height:15vh;
}
.header{
    display:flex;
    justify-content: space-around;
    align-items: center;
}
</style>