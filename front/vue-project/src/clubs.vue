<template>
    <div class="clubs-container">
      <div id="card" v-for="club in clubs" :key="club" class="clubs">
        <p class="heading">{{club.title}}</p>
        <p>{{club.description.substring(0,17)}}</p>
        <button class="btn" @click="GetClubId(club.id)">Посмотреть</button>
      </div>
    </div>
</template>
<style scoped>
.logo-id{
  width:10vh;
  height:10vh;
  border-radius:50%;
}
.clubs-container{
  width:99%;
  padding-bottom: 10%;
}
.club{
  background-color: rgb(73, 69, 69);
  width:40%;
  margin-top: 2%;
  padding:2%;
  color:white;
  display: flex;
  justify-content: space-around;
  align-items: center;
  border-radius: 5px;
}
.clubs{
  width:90%;
  display: flex;
  justify-content: center;
  align-items: center;
  
}
</style>
<script setup>
import {ref,onMounted} from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'
let clubs = ref([])
let router = useRouter();
async function fetchData()
{
   await axios.get('https://localhost:7210/api/clubs')
     .then(function (res) {
       console.log(res);
       return clubs.value = res.data;
})
}
function GetClubId(id){
    console.log(id);
    localStorage.setItem('clubId',JSON.parse(id));
    router.push('/club-page');
}
onMounted(async()=>{
  await fetchData();
})






</script>