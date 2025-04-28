<template>
<div class="container">
    <div class="cont-login">   
            <p @click="Goto()">Войти в аккаунт</p>
            <br>
			<input type="text" v-model="Acc.firstName" class="input" placeholder="firstName">
            <br>
			<input type="text" class="input" placeholder="lastName" v-model="Acc.lastName">
            <br>
            <input type="text" class="input" placeholder="nickName" v-model="Acc.nickName">
            <br>
            <input type="text" v-model="Acc.password" class="input" placeholder="password">
            <br>
            <button v-show="Acc.nickName >= 2 && Acc.password >= 3 && Acc.firstName >= 2 && Acc.nickName >= 3" @click="Registration()" class="sign">Регистрация</button>

		
    </div>
</div>
</template>

<script setup>
import {ref} from 'vue'
import { useRouter} from 'vue-router'
import axios from 'axios'
let Acc = ref({
    firstName:'',
    lastName:'',
    nickName:'',
    password:''
})
let router = useRouter();
function Goto(){
    router.push('/');
}
async function Registration(){
await axios.post('https://localhost:7210/api/auth/register',{firstName:Acc.value.firstName,
lastName:Acc.value.lastName,
nickName:Acc.value.nickName,
password:Acc.value.password

}).then(function(responce){
    if(responce){
        router.push('/user');
        localStorage.clear();
        localStorage.setItem('accessToken',responce.data.accessToken);
        localStorage.setItem('refreshToken',responce.data.refreshToken);
    }
    else{
        console.log('non correct values');
    }
});
}
</script>