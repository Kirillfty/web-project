<template>
    <div class="header">
        <img src="./assets/user.png" alt="" class="logo">
        <p>{{userData.firstName}}</p>
        <p>{{userData.lastName}}</p>
        <p>{{userData.nickName}}</p>
    </div>
    <div class="clubs-container-user">
        <div id="card" v-for="club in clubs" :key="club" class="clubs">
          <p class="heading">{{club.title}}</p>
          <p>{{club.description}}</p>
        </div>
    </div>
</template>


<script setup>
import {ref,onMounted} from 'vue'
import axios from 'axios'
let userId = localStorage.getItem('userId');
let userData = ref('');
let clubs = ref('');

async function getClubs() {
  let acsecc = localStorage.getItem("accessToken");
  await axios.get("https://localhost:7210/api/clubs/get-my-clubs-page/"+userId).then(function (res) {
    console.log(res);
    return clubs.value = res.data;
  });
}
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
    await getClubs();
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