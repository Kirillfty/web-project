<template>
  <div class="form">
    <p class="title">Register </p>
    <p class="message">Signup now and get full access to our app. </p>
    <div class="flex">
      <label>
        <input class="input" type="text" placeholder="" required="" v-model="Acc.firstName">
        <span>Firstname</span>
      </label>

      <label>
        <input class="input" type="text" placeholder="" required="" v-model="Acc.lastName">
        <span>Lastname</span>
      </label>
    </div>

    <label>
      <input class="input" type="text" placeholder="" required="" v-model="Acc.nickName">
      <span>nickName</span>
    </label>

    <label>
      <input class="input" type="password" placeholder="" required="" v-model="Acc.password">
      <span>Password</span>
    </label>
    <button class="submit" @click="Registration()">Submit</button>
    <p class="signin" @click="Goto()">Already have an acount ? <a href="#">Signin</a></p>
    <p v-if="error != null">{{error}}</p>
  </div>
</template>

<script setup>
import {ref} from 'vue'
import {useRouter} from 'vue-router'
import axios from 'axios'

const Acc = ref({
  firstName: '',
  lastName: '',
  nickName: '',
  password: ''
})
let error = ref(null);
let router = useRouter();

function Goto() {
  router.push('/');
}

async function Registration() {
  if (Acc.value.firstName.trim() == '' ||
      Acc.value.lastName.trim() == '' ||
      Acc.value.nickName.trim() == '' ||
      Acc.value.password.trim() == ''
  ) {
    error.value = 'вы ввeли неверные данные'
  }
  else{
    await axios.post('https://localhost:7210/api/auth/register', {
      firstName: Acc.value.firstName,
      lastName: Acc.value.lastName,
      nickName: Acc.value.nickName,
      password: Acc.value.password

    }).then(function (responce) {
      if (responce) {
        router.push('/user');
        localStorage.clear();
        localStorage.setItem('accessToken', responce.data.accessToken);
        localStorage.setItem('refreshToken', responce.data.refreshToken);
      } else {
        console.log('non correct values');
      }
    });
  }
}
</script>