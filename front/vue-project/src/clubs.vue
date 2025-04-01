<template>
    <div>
      <div v-for="club in clubs" :key="club" class="clubs">
        <div class="club">
          <img src="./assets/Без имени.png" class="logo-id">
          <div>
            <p>{{club.title}}</p>
            <p>{{club.description}}</p>
          </div>
        </div>
      </div>
    </div>
    
</template>
<style scoped>
/* img{
  width:20%;
  height:5vh;
} */
.logo-id{
  width:10vh;
  height:10vh;
  border-radius:50%;
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
  width:100%;
  display: flex;
  justify-content: center;
  align-items: center;
  
}
</style>
<script setup>
import {ref,onMounted} from 'vue'
import axios from 'axios'
let clubs = ref([])

async function fetchData()
{

   await axios.get('https://localhost:7210/clubs')
     .then(function (res) {
       console.log(res);
       return clubs.value = res.data;
})

}

onMounted(async()=>{
  await fetchData();
})






</script>