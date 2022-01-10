<template>
  <div class="router-content process-tab">
        <div style="height: 100%"> 
            <router-view></router-view>
        </div>
      
  </div>
</template>

<script>
export default {
  components: {  },
    props:{
        isMini: Boolean,
        isHidden: Boolean,
    },
    data(){
        return{
            show: true,
        }
    },
    created(){

    },
    methods:{
        
    },
}
</script>

<style>
@import url(../css/content/process.css);
.hidden{
    display: none;
}
</style>