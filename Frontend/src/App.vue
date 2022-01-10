<template>
  <div id="app">
    <TheMenu
    :isMini="mini" 
    v-on:resizeMenu="resizeMenu" 
    v-on:chooseItem="chooseItem"
    ref="menu"
    />

    <div class="content">
        <TheHeader
        :isMini="mini"
        v-on:resizeMenu="resizeMenu"
        />

        <VProcessTab :isHidden="hidden"/>
    </div>
  </div>
</template>

<script>
import TheHeader from './components/layouts/header/TheHeader.vue'
import TheMenu from './components/layouts/menu/TheMenu.vue'
import VProcessTab from './views/VProcessTab.vue'

export default {
  name: 'App',
  components: {
    TheHeader,
    TheMenu,
    VProcessTab
    
  },
  data(){
    return{
      mini: false,
      hidden: false,
    }
  },
  mounted(){
    this.$refs.menu.chooseObject();
  },
  methods:{
    /**
     * Thay đổi kích thước menu
     */
    resizeMenu(){
      this.mini = !this.mini;
    },

    chooseItem(index){
      (index == 0)? this.hidden = false : this.hidden = true;
    },

    
  }
}
</script>

<style>
@import url(css/common/common.css);
#app{
  display: flex;
  height: 100vh;
  width: 100vw;
  overflow: hidden;
}
.content{
  position: relative !important;
  display: flex;
  flex-direction: column;
  flex: 1;
  background-color: #f8f8f8;
}
</style>
