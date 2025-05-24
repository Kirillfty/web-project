<template>
  <div class="user-header">
    <div class="logo">
      <img src="./assets/user.png" alt="" class="logo"/>
    </div>
    <div class="user-text">
      <div id="header-club">
        <p id="info">id: {{ clubData.id }}</p>
        <br/>
        <p id="info">название: {{ clubData.title }}</p><br>
        <p id="nick">создатель:</p>
        <br>
      </div>
    </div>
  </div>
  <section class="container-users">
    <section class="club-description">
      <div>
        <p>{{ clubData.description }}</p>
      </div>
    </section>
    <div>
      <p>В клубе состоят:</p>
      <div class="container-users" v-for="users in usersInClub" :key="users">
        <div class="user-info">
          <img src="./assets/user.png" alt="" id="user-logo">
          <div class="user-text">
            <p>{{ users.firstName }}</p>
            <p>{{ users.lastName }}</p>
            <p>{{ users.nickName }}</p>
          </div>
        </div>
      </div>
    </div>
  </section>
  <section class="sign_up_club_form">
    <button class="submit" @click="EnterClub()">Вступить</button>
    <button class="submit" @click="ExitClub()">Выйти</button>
  </section>
</template>

<script setup>
import {ref, onMounted} from 'vue'
import axios from 'axios'

let userData = ref('');
let clubData = ref('');
let clubId = ref('');
let usersInClub = ref('');

async function GetUsersInClub() {
  clubId.value = localStorage.getItem('clubId');
  axios.get('https://localhost:7210/api/users/get-users-in-club/' + clubId.value)
      .then(function (res) {
        console.log(res.data)
        return usersInClub.value = res.data;
      })
}


async function getDataClub() {
  clubId.value = localStorage.getItem('clubId');
  console.log(clubId.value);
  axios.get('https://localhost:7210/api/clubs/' + clubId.value)
      .then(function (res) {
        console.log(res.data);
        return clubData.value = res.data;
      })
}

async function EnterClub() {
  clubId.value = localStorage.getItem('clubId');
  let acsecc = localStorage.getItem("accessToken");
  await axios.post('https://localhost:7210/api/clubs/enter-club/' + clubId.value, null, {headers: {Authorization: "Bearer " + acsecc}})
      .then(function (res) {
        if (res) {
          alert("вы вступили в клуб");
          GetUsersInClub();
        }
      })
}

async function ExitClub() {
  clubId.value = localStorage.getItem('clubId');
  let acsecc = localStorage.getItem("accessToken");
  await axios.post('https://localhost:7210/api/clubs/exit-club/' + clubId.value, null, {headers: {Authorization: "Bearer " + acsecc}})
      .then(function (res) {
        if (res) {
          alert("вы вышли из клуба");
          GetUsersInClub();
        }
      })
}

onMounted(async () => {
  await getDataClub();
  await GetUsersInClub();
})
</script>


<style scoped>
hr {
  color: aliceblue;
}

.user-info {
  width: 80%;
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.user-text{
  display:flex;
  flex-direction: column;
}
.container-users{
  width:80%;
  margin-top:5%;
  max-height: 50vh;
  display:flex;
  justify-content: space-around;
  align-items: center;

}
.club-description{
  max-width:60%;
}
.sign_up_club_form{
  display:flex;
  justify-content: space-around;
  align-items: center;
  margin-top:5%;
}
</style>