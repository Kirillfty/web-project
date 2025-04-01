<template>
<div class="container">
	<div class="cont-login">
			<p @click="GoTo()">go to Registration</p>
			<br>
			<input type="text" v-model="Account.nickName" class="input" placeholder="nickName">
			<br>
			<input type="text" v-model="Account.password" class="input" placeholder="password">
			<br>
			<button @click="Login()" class="sign">click</button>
    </div>
</div>
</template>

<script setup>
import { useRouter } from 'vue-router'
import {ref}  from 'vue'
import axios from 'axios'

let Account = ref({
	nickName:null,
	password:null
})
function Login(){
	axios.post('https://localhost:7210/api/auth/login',{nickName:Account.value.nickName,password:Account.value.password})
	.then(function(res){
		localStorage.clear();
        localStorage.setItem('accessToken',res.data.accessToken);
        localStorage.setItem('refreshToken',res.data.refreshToken);
		console.log('ok');
		router.push('/clubs')
	})
}
let router = useRouter()
function GoTo(){  
 router.push('/register');
}
 



</script>