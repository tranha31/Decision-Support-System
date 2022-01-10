<template>
    <div :class="[load? 'loader' : 'loader-none']">
        <div class="background">

        </div>
        <object :data="loadUrl" class="load"></object>
        <!-- <img src="../../assets/icons/loading.svg" class="spinner"> -->
    </div>
</template>

<script>
export default {
    name: "Load",
    props:{
        load: Boolean,
    },
    data(){
        return{
            loadUrl: require("../../assets/icons/loading.svg"),
        }
    }
}
</script>

<style>
    .loader-none{
        display: none;
    }
    .loader{
        width: 100vw;
        height: 100vh;
        position: fixed;
        top : 0;
        left: 0;
        display: flex;
        align-items: center;
        justify-content: center;
        z-index: 555555555;
        
    }
    .loader .background{
        background: rgba(0,0,0,.4);
        opacity: 0.5;
        width: 100%;
        height: 100%;
        position: absolute;
    }
    .load{
        width: 50px;
        height: 50px;
        z-index: 1;
    }
    .spinner{
        width: 50px;
        height: 50px;
        animation: spinner 1.5s linear infinite;
        z-index: 1;
    }
    @keyframes spinner{
        from{
            transform: rotate(0deg);
        }
        to{
            transform: rotate(360deg);
        }
    }
</style>