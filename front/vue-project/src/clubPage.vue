<template>
    <div class="user-header">
      <div class="logo">
        <img src="./assets/user.png" alt="" class="logo" />
      </div>
      <div class="user-text">
        <div id="header-club">
          <p id="info">id: {{ clubData.id }}</p>
          <br />
          <p id="info">название: {{ clubData.title }}</p><br>
          <p id="nick">создатель: пока не сделал</p>
          <br>
        </div>
      </div>
    </div>
    <section>
      <section class="club-description">
        <div>
          <p>{{clubData.description}}</p>
        </div>
        <div>
          <div class="container-users" v-for="users in usersInClub" :key="users">
            <div class="user-info">
                <img src="./assets/user.png" alt="" id="user-logo">
                <p>{{users.firstName}}</p>
                <p>{{users.lastName}}</p>
                <p>{{users.nickName}}</p>
            </div>
          </div>
        </div>
      </section>
      <section class="sign_up_club_form">
        <button class="submit">Вступить</button>
      </section>
    </section>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
let clubData = ref('');
let clubId = ref('');
let usersInClub = ref('');
async function GetUsersInClub(){
  clubId.value = localStorage.getItem('clubId');
  axios.get('https://localhost:7210/api/users/get-users-in-club/'+clubId.value)
  .then(function(res){
    console.log(res.data)
    return usersInClub.value = res.data;
  })
}

async function getDataClub() {
    clubId.value = localStorage.getItem('clubId');
    console.log(clubId.value);

    axios.get('https://localhost:7210/api/clubs/'+clubId.value)
        .then(function (res) {
            console.log(res.data);
            return clubData.value = res.data;
        })
}
onMounted(async () => {
    await getDataClub();
    await GetUsersInClub();
})
</script>

<style>
.club-description{
  display:flex;
  justify-content: space-around;
}
.user-info{
  width: 100%;
  background-color: rgb(70, 70, 70);
  display:flex;
  justify-content: space-around;
  align-items: center;
}
</style>