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
          <p>
            {{clubData.description}}
          </p>
      </section>
      <section class="sign_up_club_form">
        <button class="submitb">Вступить</button>
      </section>
    </section>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
let userData = ref('');
let clubData = ref('');
let clubId = ref('');
async function getUsersInClub(){
  clubId.value = localStorage.getItem('clubId');
  axios.get('https://localhost:7210/api/users/get-users-in-club/'+clubId.value)
  .then(function(res){
      console.log('данные пользователей клуба:');
      console.log(res.data);
      return userData.value = res.data;
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
    await getUsersInClub();
})
</script>

<style></style>