<script setup lang="ts">
import { RouterView } from 'vue-router';

</script>
<style scoped>
.aside{
   width:100%;
   height:10vh;
   background-color: rgb(20, 20, 20);
   display: flex;
   justify-content: space-around;
   position:fixed;
   align-items: center;
   bottom:0;
   z-index:3;
}
#link{
   color:aliceblue;
   text-decoration: none;
   font-size: 12px;
}
.icon{
   width:6vh;
   height:6vh;
}
.icons{
  display: flex;
  flex-direction: column;
  text-align: center;
}
</style>

<template>
   <div>
      <RouterView />
   </div>
</template>



