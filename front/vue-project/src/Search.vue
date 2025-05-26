<template>
  <div class="container">
    <div class="cont-login">
      <p id="info">Найдите пользователя по его name</p>
      <input type="text" class="input" v-model="user" />
      <button class="btn" @click="SearchUser()">Найти</button>
      <div class="clubs-container">
        <div class="clubs" id="card" @click="GoToUserPage(userData.id)" v-show="userData.length != ' '">
          <img src="./assets/user.png" alt="" class="logo">
          <div>
            <p class="heading">{{userData.firstName}}</p>
            <p>{{userData.lastName}}</p>
            <p>{{userData.nickName}}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup>
import {ref} from 'vue'
import axios from 'axios'
import {useRouter} from 'vue-router'
let router = useRouter();
let user = ref('');
let userData = ref('');
function SearchUser(){
        console.log(user.value);
        axios.get('https://localhost:7210/api/users/name/?name='+user.value).then(function(responce){
        console.log(responce.data);
        userData.value = responce.data;
        console.log('user '+userData);
        
        return userData;
    })
}

function GoToUserPage(id){
  localStorage.setItem('userId',id);
  router.push('/home');
}
</script>
