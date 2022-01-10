<template>
    <div class="header">
        <div v-if="!isMini" class="mi mi-24 resize-menu" @click="resizeMenu" ></div>
        <div class="title">
            Áp dụng trong khuôn khổ môn Hệ trợ giúp ra quyết định
        </div>  
    </div>
</template>

<script>
export default {
    name: "Header",
    props:{
        isMini: Boolean,
    },
    methods:{
        /**
         * Thu nhỏ menu
         * create by: TQHA (17/8/2021)
         */
        resizeMenu(){
            this.$emit("resizeMenu");
        }
    }
}
</script>

<style>

@import url(../../../css/common/header.css);

</style>