   
<template>
    <div :class="[isShow? 'popup-container': 'donot-show']">
        <div class="background">

        </div>
        <div class="popup">
            <div class="popup-content">
                <div class="icon" style="padding-top:12px">
                    <div class="mi mi-48" :class="type"></div>
                </div>
                
                <div class="content">
                    <div class="title">{{ title }}</div>
                    <div>{{ message }}</div>
                </div>
            </div>
            <div v-if="type != 'error'" class="popup-footer">
                <div class="button-popup button-1" :class="[!show[0]? 'hidden' : '']" @click="cancelAction">{{ action1 }}</div>
                <div class="flex"></div>
                <div class="button-popup button-2" :class="[!show[1]? 'hidden' : '']" @click="confirmAction2">{{ action2 }}</div>
                <div class="button-popup button-3" @click="confirmAction">{{ action3 }}</div>
            </div>
            <div v-else class="popup-footer">
                <div class="button-popup button-4" @click="action">Đóng</div>
            </div>
            
        </div>
    </div>
</template>

<script>
export default {
    name: "BasePopUp",
    props:{
        isShow: Boolean,
        type: String,
        message: String,
        title: String,
        action1: String,
        action2: String,
        action3: String,
        show: Array,
    },
    methods:{
        /**
         * Xác nhận hành động
         * create by: TQHA (19/8/2021)
         */
        confirmAction(){
            this.$emit("confirmAction");
        },
        /**
         * Không thực hiện action
         * create by: TQHA (19/8/2021)
         */
        cancelAction(){
            this.$emit("cancelAction");
        },
        /**
         * Thực hiện hành động thứ 2
         * create by: TQHA (19/8/2021)
         */
        confirmAction2(){
            this.$emit("confirmAction2");
        },
        /**
         * Xác nhận hành động
         * create by: TQHA (19/8/2021)
         */
        action(){
            this.$emit("confirmAction3");
        }
    }
}
</script>

<style>
    @import url(../../css/common/popup.css);
</style>