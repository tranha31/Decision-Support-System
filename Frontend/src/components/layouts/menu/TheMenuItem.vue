<template>

    <div :class="['menu-item', isChoose ? 'menu-item-active' : '']" @click="chooseItem">
        <div class="icon">
            <div class="menu-icon">
                <div class="mi mi-24" :class="typeItem"></div>
            </div>
        </div>
        <div v-if="!isMini" class="content-item">
            {{content}}
        </div>
    </div>

</template>

<script>
export default {
    name: "MenuItem",
    props:{
        content: String,
        isChoose: Boolean,
        typeItem: String,
        isMini: Boolean,
    },
    methods: {
        /**
         * Truyền sự kiện chọn item cho component cha
         */
        chooseItem(){
            this.$emit("chooseItem");
        },
    },
}
</script>

<style>
    @import url(../../../css/common/menu.css);
    
</style>