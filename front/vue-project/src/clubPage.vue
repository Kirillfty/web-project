<template>
    <div class="clubs">
        <div class="club">
          <img src="./assets/Без имени.png" class="logo" />
          <div>
            <p>{{ clubData.title }}</p>
            <p>{{ clubData.description }}</p>
          </div>
        </div>
      </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
let clubData = ref('');
async function getDataClub() {
    let clubId = ref('');
    clubId = JSON.stringify(localStorage.getItem('clubId'));
    console.log(clubId);

    axios.get('https://localhost:7210/clubs/',{id:clubId})
        .then(function (res) {
            console.log(res.data);
            return clubData.value = res.data;
        })
}
onMounted(async () => {
    await getDataClub();
})
</script>

<style></style>