<template>
  <header>
    <div class="user-header">
      <div class="logo">
        <img src="./assets/user.png" alt="" class="logo" />
      </div>
      <div class="user-text">
        <div id="header-info">
          <p id="info">id:{{ userData.id }}</p>
          <br />
          <p id="info">{{ userData.firstName }}</p>
          <p id="info">{{ userData.lastName }}</p>
        </div>
        <p id="nick">{{ userData.nickName }}</p>
      </div>
      <p id="exit__button" @click="ExitAccount()">exit</p>
    </div>
    <p align="center">Создай клуб</p>
    <div>
      <div class="clubich">
        <input
          type="text"
          v-model="title"
          class="input"
          placeholder="название"
        />
        <input
          type="text"
          v-model="description"
          class="input"
          placeholder="краткое описание"
        />
        <button @click="Post()" class="btn">Создать</button>
      </div>

      <div class="clubs-container-user">
        <div id="card" v-for="club in clubs" :key="club" class="clubs">
          <p class="heading">{{club.title}}</p>
          <p>{{club.description}}</p>
          <button class="btn" @click="Delete(club.id)">Delete</button>
        </div>
     </div>
    </div>
    <layout/>
  </header>
</template>
<style>
#exit__button{
  margin-right: 3%;
}
.clubs-container-user{
  padding-bottom: 10%;
}
.logo{
    width:15vh;
    height:15vh;
}
.clubich {
  width: 40%;
  height: 20vh;
  margin: 0 auto;
}
#header-info {
  display: flex;
  gap: 2%;
}
#info {
  font-size: 1.5em;
}
.user-header {
  width: 100%;
  padding-top: 2%;
  padding-left: 10%;
  display: flex;
  justify-content: space-around;
  align-items: center;
}
#nick {
  color: rgb(200, 203, 206);
}
.user-text {
  width: 100%;
  color: aliceblue;
  font-size: 1em;
  align-items: center;
  margin-left: 5%;
}
</style>
<script setup>
import Layout from './components/layout.vue'
import axios from "axios";
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
let router = useRouter();
let clubs = ref("");
let title = ref("");
let description = ref("");
let result = ref("");
let userData = ref("");

function ExitAccount(){
  localStorage.removeItem('accessToken');
  location.reload();
}

async function getClubs() {
  let acsecc = localStorage.getItem("accessToken");
  await axios.get("https://localhost:7210/api/clubs/get-my-clubs", {headers: { Authorization: "Bearer " + acsecc }}).then(function (res) {
    console.log(res);
    return (clubs.value = res.data);
  });
}
async function Delete(id) {
  let acsecc = localStorage.getItem("accessToken");
  axios.delete('https://localhost:7210/api/clubs/'+id,{headers: { Authorization: "Bearer " + acsecc }}).then(function (res) {
    if (res) {
      getClubs();
    }
  });
  console.log(id.value);
  console.log(id);
}
async function getUser() {
  console.log("result " + result);
  await axios
    .get("https://localhost:7210/api/users/" + result.value)
    .then(function (responce) {
      console.log(responce.data);
      userData.value = responce.data;
      console.log("user " + userData);
      return userData;
    });
}
function Post() {
  let acsecc = localStorage.getItem("accessToken");
  axios.post("https://localhost:7210/api/clubs/create", { title: title.value,description: description.value} , {headers: { Authorization: "Bearer " + acsecc }})
    .then(function (responce) {
      getClubs();
    });
}

async function checkAuthorize() {
  let acsecc = localStorage.getItem("accessToken");
  let refresh = localStorage.getItem("refreshToken");
  if (acsecc != null && refresh != null) {
    console.log(acsecc + "/" + refresh);
    await axios
      .get("https://localhost:7210/api/auth/check", {
        headers: { Authorization: "Bearer " + acsecc },
      })
      .then(function (res) {
        console.log(res.status);
        result.value = res.data;
        console.log(result);
      }).catch(function(err){
        if(err.responce == 401){
          router.push('/');
        }
      })
  } else {
    router.push("/");
  }
}
onMounted(async () => {
  await checkAuthorize();
  await getClubs();
  await getUser();
});
</script>
