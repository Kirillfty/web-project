<template>
  <div class="container">
    <div class="cont-login">
      <p id="info">Найдите пользователя по его name</p>
      <input type="text" class="input" v-model="user" />
      <button class="btn" @click="SearchUser()">Найти</button>
      <div class="clubs-container">
        <div class="clubs" id="card" @click="GoToUserPage(userData.id)" v-show="userData.length != ' '&& errorRequest != 400">
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
  <layout/>
</template>
<script setup>
import Layout from './components/layout.vue'
import {ref} from 'vue'
import axios from 'axios'
import {useRouter} from 'vue-router'
let router = useRouter();
let user = ref('');
let userData = ref('');
let errorRequest = ref('');
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
  router.push('/home/'+id);
}
</script>
