<template>
    <div :class="[isMini ? 'menu-bar-mini' : 'menu']">
        <div v-if="!isMini" class="logo-amis">
            <div class="menu-container-option"></div>
            <img src="../../../assets/img/logo.png" class="logo">
            <h2 class="group-name">Nhóm 2</h2>
        </div>
        <div v-else class="mini-menu">
            <div class="resize-menu" @click="resizeMenu" ></div>
        </div>
        <div class="menu-item-list">
            <router-link to="/Decision/0">
                <TheMenuItem
                typeItem="menu-item-overview"
                content="Hỗ trợ quyết định"
                :isMini="isMini"
                :isChoose="choose[0]"
                v-on:chooseItem="chooseItem(0)"
                />
            </router-link>
           <router-link to="/Object/0">
                <TheMenuItem
                typeItem="menu-item-cash"
                content="Danh mục"
                :isMini="isMini"
                :isChoose="choose[1]"
                v-on:chooseItem="chooseItem(1)"
                />
            </router-link>
        </div>
    </div>
</template>

<script>
import TheMenuItem from './TheMenuItem.vue'
export default {
    name: "Menu",
    components: { TheMenuItem },
    props:{
        isMini: Boolean
    },
    data(){
        return{
            choose: [false, false]
        }
    },
    methods:{
        /**
         * Lựa chọn item
         * @param {Number} index thứ tự của item 
         */
        chooseItem(index){
            var tmp = [];
            for(var i=0; i<2; i++){
                (i == index) ? tmp.push(true) : tmp.push(false);
            }
            this.choose = tmp;
            this.$emit("chooseItem", index);
        },
        /**
         * Truyền sự kiện phóng to menu cho component cha
         */
        resizeMenu(){
            this.$emit("resizeMenu");
        },
        /**
         * Mặc định chọn item danh mục khi load
         */
        chooseObject(){
            var tmp = [];
            for(var i=0; i<2; i++){
                (i == 0) ? tmp.push(true) : tmp.push(false);
            }
            this.choose = tmp;
        }
    }
}
</script>

<style>
    .menu-bar-mini{
        width: 52px;
        background-color: #393A3D;
        display: flex;
        flex-direction: column;
    }
    .menu-bar-mini .mini-menu{
        height: 48px;
        display: flex;
        align-items: center;
        justify-content: center;
    }
    .menu-bar-mini .mini-menu .resize-menu{
        background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat -316px -37px;
        width: 16px;
        height: 14px;
        cursor: pointer;
    }
    .menu{
        width: 178px;
        height: 100%;
        background-color: #393A3D;
        display: flex;
        flex-direction: column;
    }
    .menu .logo-amis{
        padding: 0px 24px; 
        height: 48px;
        display: flex;
        align-items: center;
    }
    .menu .logo-amis .menu-container-option{
        width: 24px;
        height: 24px;
        margin-right: 10px;
        background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat -424px -86px;
        cursor: pointer;
    }
    .menu .logo-amis .logo{
        height: 32px;
    }
    .menu .logo-amis .group-name{
        font-size: 14px;
        color: #41b883;
    }
    .menu-item-list{
        height: calc(100vh - 48px);
        padding-top: 14px;
        display: flex;
        flex-direction: column;
        overflow: auto;
    }
</style>