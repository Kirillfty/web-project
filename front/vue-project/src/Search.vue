<template>
  <div class="container">
    <div class="cont-login">
      <p id="info">Найдите пользователя по его id</p>
      <input type="text" class="input" v-model="user" />
      <button class="sign" @click="SearchUser()">Найти</button>
    </div>
  </div>
</template>
<script setup>
import {ref} from 'vue'
let user = ref('');
let userData = ref('');
function SearchUser(){
    console.log(user.value);
        axios.get('https://localhost:7210/api/users/'+ user.value).then(function(responce){
        console.log(responce.data);
        userData.value = responce.data;
        console.log('user '+userData);
        return userData;
    })
}
</script>
