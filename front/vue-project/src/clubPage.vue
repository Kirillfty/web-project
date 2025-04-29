<template>
    <h1 style="color: red;">clubPage</h1>


</template>

<script setup>
import {ref,onMounted} from 'vue'
import axios from 'axios'
let clubData = ref('');
function getDataClub(){
    let clubId = JSON.stringify(localStorage.getItem('clubId'));
    console.log(clubId);
    
    axios.get('https://localhost:7210/clubs/'+Number(clubId))
    .then(function(res){
        console.log(res.data);
        return clubData.value = res.data;
    })
}
onMounted(async()=>{
  await getDataClub();
})
</script>

<style>


</style>