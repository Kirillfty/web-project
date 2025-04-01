<template>
     <div>
            <button @click="$emit('close')" >x</button>
            <input type="text" v-model="clubs.title" class="input">
            <input type="text" v-model="clubs.description" class="input">
            <button @click="Post()" class="sign">click</button>
    </div>
</template>
<script>
export default{
    props:{
        clubs:{
            type:String,
            required:true
        }
    },
    methods:{
        Post(){
            axios.post('https://localhost:7210/clubs/create',{
                title:clubs.title,
                description:clubs.description
            })
        },
        close(){
            this.$emit('close')
        }
    }
}
</script>