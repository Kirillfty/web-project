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
          <div class="user-info" @click="GoToUserPage(users.id)">
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
  <Layout></Layout>
</template>

<script setup>
import {ref, onMounted} from 'vue'
import {useRouter,useRoute} from 'vue-router'
import axios from 'axios'
import Layout from './components/layout.vue'

let route = useRoute();
let clubId = route.params.id;
let router = useRouter();
let clubData = ref('');
let usersInClub = ref('');

async function GetUsersInClub() {
  axios.get('https://localhost:7210/api/users/get-users-in-club/' + clubId)
      .then(function (res) {
        console.log(res.data)
        return usersInClub.value = res.data;
      })
}


async function getDataClub() {

  console.log(clubId);
  axios.get('https://localhost:7210/api/clubs/' + clubId)
      .then(function (res) {
        console.log(res.data);
        return clubData.value = res.data;
      })
}

async function EnterClub() {
  let acsecc = localStorage.getItem("accessToken");
  await axios.post('https://localhost:7210/api/clubs/enter-club/' + clubId, null, {headers: {Authorization: "Bearer " + acsecc}})
      .then(function (res) {
        if (res) {
          alert("вы вступили в клуб");
          GetUsersInClub();
        }
      })
}

async function ExitClub() {

  let acsecc = localStorage.getItem("accessToken");
  await axios.post('https://localhost:7210/api/clubs/exit-club/' + clubId, null, {headers: {Authorization: "Bearer " + acsecc}})
      .then(function (res) {
        if (res) {
          alert("вы вышли из клуба");
          GetUsersInClub();
        }
      })
}

function GoToUserPage(id) {
  router.push('/home/'+id);
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

.user-text {
  display: flex;
  flex-direction: column;
}

.container-users {
  width: 80%;
  margin-top: 5%;
  max-height: 50vh;
  display: flex;
  justify-content: space-around;
  align-items: center;

}

.club-description {
  max-width: 60%;
}

.sign_up_club_form {
  display: flex;
  justify-content: space-around;
  align-items: center;
  margin-top: 5%;
}
</style>