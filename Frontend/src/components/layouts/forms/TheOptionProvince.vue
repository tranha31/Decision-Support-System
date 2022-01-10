<template>
    <div class="form-container" id="condition-form" style="position: relative;">
      <div class="title-form title-option" style="width:400px"> 
          Tỉnh đề xuất điều động bác sỹ 
      </div>
      <div class="d-flex check-define">
      </div>
      <div id="option-content" class="d-flex flex-column">
        <div v-for="(v, index) in listP" v-bind:key="index" class="d-flex option-p">
            <div>
                <input type="checkbox" :id="`checkbox-default-`+index" class="checkbox" style="display:none !important" :checked="v.check ? 'checked' : ''">
                <label :for="`checkbox-default-`+index" :id="`label-default-`+index" style="display:flex; align-items: center">
                    <div class="mi mi-16 div-checkbox"></div>
                    <div class="label-content" style="font-weight:600">{{v.value}}</div>
                </label>
            </div>
        </div>
      </div>
      <div class="footer footer2">
        <div style="flex:1"></div>
        <div>
            <div class="btn btn-save" style="width: 103px" @click="excute">Tiếp theo</div>
        </div>
      </div>
  </div>
</template>

<script>
export default {
    props:{
        listP: Array,
        listProvince: Array,
    },
    watch:{
        
    },
    data(){
        return{
            
        }
    },
    created(){
        
    },
    methods:{
        /**
         * Thuc hien chuc nang
         */
        excute(){
            var listID = [];
            var checkBox = document.querySelectorAll("input[type=checkbox");
            for(var i=0; i<checkBox.length; i++){
                if(checkBox[i].checked){
                    var e = document.getElementById(`label-default-${i}`);
                    var p = e.querySelector(".label-content").textContent;
                    for(var j=0; j<this.listProvince.length; j++){
                        if(this.listProvince[j].ProvinceName == p){
                            listID.push(this.listProvince[j].ProvinceId);
                            break;
                        }
                    }
                }
            }
            this.$emit("goToStep3", listID);
        }
    }
}
</script>

<style>
@import url(../../../css/form/condition.css);


</style>