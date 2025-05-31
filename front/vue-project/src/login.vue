<template>
	<div class="form">
		<p class="title">Sign in</p>
		<p class="message">Signup now and get full access to our app.</p>
		<label>
			<input class="input" type="texxt" placeholder="" required="" v-model="Account.nickName" />
			<span>nickName</span>
		</label>

		<label>
			<input class="input" type="password" placeholder="" required="" v-model="Account.password" />
			<span>Password</span>
		</label>
		<button class="submit" @click="Login()">Submit</button>
		<p class="signin" @click="GoTo()">
			Already dont have an acount ? <a href="#">Registration</a>
		</p>
    <p v-if="error != null">{{error}}</p>
    <p v-if="error == 400">такого пользователя нету</p>
	</div>
</template>


<script setup>

import { useRouter } from "vue-router";
import { ref } from "vue";
import axios from "axios";

let Account = ref({
	nickName: '',
	password: '',
});

let error = ref(null);
 async function Login() {
  if(Account.value.nickName.trim() == '' || Account.value.password.trim() == ''){
    error.value = 'вы ввели неверные данные'
  }
  else{
   await axios
        .post("https://localhost:7210/api/auth/login", {
          nickName: Account.value.nickName,
          password: Account.value.password,
        })
        .then(function (res) {
          localStorage.clear();
          localStorage.setItem("accessToken", res.data.accessToken);
          localStorage.setItem("refreshToken", res.data.refreshToken);
          console.log("ok");
          router.push("/user");
        });
  }

}
let router = useRouter();
function GoTo() {
	router.push("/register");
}
</script>

