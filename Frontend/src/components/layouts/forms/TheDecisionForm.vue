<template>
  <div class="form-container" id="condition-form" style="position: relative;">
      <div class="title-form" style="width:400px"> 
          Lựa chọn điều kiện quyết định <span v-if="step==2">(2)</span>
      </div>
      <div class="d-flex">
          <table>
              <thead>
                    <th class="th-viewer th-checkbox th-employee">
                        <label class="checkbox" @click="chooseAll($event)"></label>
                    </th> 
                    <th class="th-viewer td-condition">ĐIỀU KIỆN</th>
                    <th class="th-viewer td-weight">TRỌNG SỐ</th>
                    <th class="th-viewer td-type">LOẠI THUỘC TÍNH</th>
              </thead>
              <tbody>
                    <tr v-for="(v, index) in conditions" v-bind:key="index">
                        <td class="td-view td-checkbox">
                            <label class="checkbox" @click="chooseCondition($event, index)"></label>
                        </td>
                        <td class="td-view td-condition">{{v}}</td>
                        <td class="td-view td-weight">
                            <input type="text" v-model="w[index]" style="text-align:right">
                        </td>
                        <td class="td-view" style="padding-left:25px">
                            <div style="margin-bottom:8px">
                                <input type="checkbox" :id="`checkbox-type1-`+index" style="display:none !important" checked>
                                <label :for="`checkbox-type1-`+index" style="display:flex; align-items: center" @click="chooseTypeProperty(1, 2, index)">
                                    <div class="mi mi-16 div-checkbox"></div>
                                    <div class="label-content">Lợi ích</div>
                                </label>
                            </div>
                            <div>
                                <input type="checkbox" :id="`checkbox-type2-`+index" style="display:none !important">
                                <label :for="`checkbox-type2-`+index" style="display:flex; align-items: center" @click="chooseTypeProperty(2, 1, index)">
                                    <div class="mi mi-16 div-checkbox"></div>
                                    <div class="label-content">Giá</div>
                                </label>
                            </div>
                        </td>
                    </tr>
                    
              </tbody>
          </table>

          <div id="amount-result" class="d-flex flex-column">
            <label>Số lượng kết quả <span style="color:red">*</span></label>
            <div>
                <input type="text" v-model="topK" style="text-align:right">
            </div>
            
          </div>
      </div>
      <div class="footer">
          <div style="flex:1">
            <input type="checkbox" id="checkbox-default-A" style="display:none !important">
            <label for="checkbox-default-A" style="display:flex; align-items: center" @click="chooseDefault">
                <div class="mi mi-16 div-checkbox" ></div>
                <div class="label-content" style="font-weight:600">Lựa chọn mặc định</div>
            </label>
            
            </div>
        
        <div>
            <div class="btn btn-save" style="width: 103px" @click="excute">Thực hiện</div>
        </div>
      </div>

        <BasePopUp
        :isShow="showPopUp"
        :type="type"
        :message="message"
        :title="title"
        :action1="action1"
        :action2="action2"
        :action3="action3"
        :show="optionPopUp"
        v-on:confirmAction3="closePopUp"
        />
  </div>
</template>

<script>
import BasePopUp from '../../base/BasePopUp.vue';
export default {
  components: { BasePopUp },
    props:{
        conditions: Array,
        conditionE : Array,
        weight : Array,
        typeF: String,
        step: Number,
    },
    watch:{
        weight : function(){
            this.w = this.weight;
        }
    },
    data(){
        return{
            w : [],
            indexChoose: [],
            topK: 0,
            status: [],
            showPopUp : false,
            type: "error",
            message: null,
            title: null,
            action1: "Hủy",
            action2 : "Đóng",
            action3 : "Lưu",
            optionPopUp : [false, false],

            typeError: -1,
        }
    },
    created(){
        for(var i=0; i<this.conditions.length; i++){
            this.w.push(0);
            this.status.push(1);
        }
    },
    methods:{
        /**
         * Làm mới form
         */
        resetForm(){
            this.w = [];
            for(var i=0; i<this.conditions.length; i++){
                this.w.push(0);
            }
            var label = document.querySelectorAll(".checkbox");
            label.forEach(element => {
                element.classList.remove("checkbox-active");
            });
            this.indexChoose = [];
            this.topK = 0;
            document.getElementById("checkbox-default-A").checked = false;
            
        },
        /**
         * Chọn tất cả điều kiện
         */
        chooseAll(event){
            var label = document.querySelectorAll(".checkbox");
            if(event.target.classList.contains("checkbox-active")){
                label.forEach(element => {
                    element.classList.remove("checkbox-active");
                });

                this.indexChoose = [];
            }
            else{
                label.forEach(element => {
                    element.classList.add("checkbox-active");
                });

                this.indexChoose = [];
                var tmp = [];
                for(var i=0; i<this.conditions.length; i++){
                    tmp.push(i);
                }
                this.indexChoose = tmp;
            }
            document.getElementById("checkbox-default-A").checked = false;
        },
        /**
         * Chọn từng điều kiện
         */
        chooseCondition(event, index){
            if(event.target.classList.contains("checkbox-active")){

                event.target.classList.remove("checkbox-active");
            }
            else{
                event.target.classList.add("checkbox-active");
            }
            const tmp = this.indexChoose.indexOf(index);
            if(tmp == -1){
                this.indexChoose.push(index);
            }
            else{
                this.indexChoose.splice(tmp, 1);
            }
            document.getElementById("checkbox-default-A").checked = false;
        },
        /**
         * Chọn loại thuộc tính
         */
        chooseTypeProperty(type, type2, index){
            var id1 = 'checkbox-type'+type+'-'+index;
            var id2 = 'checkbox-type'+type2+'-'+index;
            setTimeout(()=>{
                document.getElementById(id1).checked = true;
                document.getElementById(id2).checked = false;
            }, 10);

            if(type == 1){
                this.status[index] = 1;
            }
            else{
                this.status[index] = 0;
            }            
        },
        /**
         * Chon mac dinh
         */
        chooseDefault(){
            var checkBox = document.getElementById("checkbox-default-A");
            var label = document.querySelectorAll(".checkbox");
            if(checkBox.checked){
                label.forEach(element => {
                    element.classList.remove("checkbox-active");
                });
                setTimeout(()=>{
                    checkBox.checked = false
                }, 10)
                this.indexChoose = [];
            }
            else{
                setTimeout(()=>{
                    checkBox.checked = true
                }, 10)
                
                switch(this.typeF){
                    case "0":
                        this.indexChoose = [];
                        var tmp1 = [8, 9, 1, 2, 10];
                        this.indexChoose = tmp1;
                        this.w = [];
                        var tmp2 = [0, 0.25, 0.1, 0, 0, 0, 0, 0, 0.3, 0.25, 0.1]
                        this.w = tmp2;
                        this.chooseTypeProperty(2, 1, 8);
                        this.chooseTypeProperty(2, 1, 9);
                        this.chooseTypeProperty(1, 2, 1);
                        this.chooseTypeProperty(1, 2, 2);
                        this.chooseTypeProperty(1, 2, 10);
                        label[9].classList.add("checkbox-active");
                        label[10].classList.add("checkbox-active");
                        label[2].classList.add("checkbox-active");
                        label[3].classList.add("checkbox-active");
                        label[11].classList.add("checkbox-active");
                        break;
                    case "1":
                        this.indexChoose = [];
                        tmp1 = [0, 5, 1, 2, 10];
                        this.indexChoose = tmp1;
                        this.w = [];
                        tmp2 = [0.3, 0.2, 0.1, 0, 0, 0.3, 0, 0, 0, 0, 0.1]
                        this.w = tmp2;
                        this.chooseTypeProperty(1, 2, 0);
                        this.chooseTypeProperty(1, 2, 5);
                        this.chooseTypeProperty(1, 2, 1);
                        this.chooseTypeProperty(1, 2, 2);
                        this.chooseTypeProperty(1, 2, 10);
                        label[1].classList.add("checkbox-active");
                        label[6].classList.add("checkbox-active");
                        label[2].classList.add("checkbox-active");
                        label[3].classList.add("checkbox-active");
                        label[11].classList.add("checkbox-active");
                        break;
                    case "2":
                        this.indexChoose = [];
                        tmp1 = [0, 1, 3, 2, 10, 7];
                        this.indexChoose = tmp1;
                        this.w = [];
                        tmp2 = [0.3, 0.3, 0.1, 0.15, 0, 0, 0, 0.05, 0, 0, 0.1]
                        this.w = tmp2;
                        this.chooseTypeProperty(1, 2, 0);
                        this.chooseTypeProperty(1, 2, 1);
                        this.chooseTypeProperty(2, 1, 3);
                        this.chooseTypeProperty(1, 2, 2);
                        this.chooseTypeProperty(1, 2, 10);
                        this.chooseTypeProperty(1, 2, 7);
                        label[1].classList.add("checkbox-active");
                        label[2].classList.add("checkbox-active");
                        label[4].classList.add("checkbox-active");
                        label[3].classList.add("checkbox-active");
                        label[11].classList.add("checkbox-active");
                        label[8].classList.add("checkbox-active");
                        break;
                    case "3":
                        this.indexChoose = [];
                        tmp1 = [1, 4, 2, 10, 0, 7];
                        this.indexChoose = tmp1;
                        this.w = [];
                        tmp2 = [0.1, 0.4, 0.1, 0, 0.25, 0, 0, 0.05, 0, 0, 0.1]
                        this.w = tmp2;
                        this.chooseTypeProperty(1, 2, 0);
                        this.chooseTypeProperty(1, 2, 1);
                        this.chooseTypeProperty(2, 1, 4);
                        this.chooseTypeProperty(1, 2, 2);
                        this.chooseTypeProperty(1, 2, 10);
                        this.chooseTypeProperty(1, 2, 7);
                        label[1].classList.add("checkbox-active");
                        label[2].classList.add("checkbox-active");
                        label[5].classList.add("checkbox-active");
                        label[3].classList.add("checkbox-active");
                        label[11].classList.add("checkbox-active");
                        label[8].classList.add("checkbox-active");
                        break;
                    case "4":
                        if(this.step == 1){
                            this.indexChoose = [];
                            tmp1 = [0, 1, 6, 2, 10];
                            this.indexChoose = tmp1;
                            this.w = [];
                            tmp2 = [0.2, 0.2, 0.2, 0, 0, 0, 0.2, 0, 0, 0, 0.2]
                            this.w = tmp2;
                            this.chooseTypeProperty(2, 1, 0);
                            this.chooseTypeProperty(2, 1, 1);
                            this.chooseTypeProperty(1, 2, 6);
                            this.chooseTypeProperty(2, 1, 2);
                            this.chooseTypeProperty(2, 1, 10);
                            label[1].classList.add("checkbox-active");
                            label[2].classList.add("checkbox-active");
                            label[7].classList.add("checkbox-active");
                            label[3].classList.add("checkbox-active");
                            label[11].classList.add("checkbox-active");
                            break;
                        }
                        else{
                            this.indexChoose = [];
                            tmp1 = [0, 1, 6, 2, 10];
                            this.indexChoose = tmp1;
                            this.w = [];
                            tmp2 = [0.3, 0.25, 0.15, 0, 0, 0, 0.2, 0, 0, 0, 0.1]
                            this.w = tmp2;
                            this.chooseTypeProperty(1, 2, 0);
                            this.chooseTypeProperty(1, 2, 1);
                            this.chooseTypeProperty(2, 1, 6);
                            this.chooseTypeProperty(1, 2, 2);
                            this.chooseTypeProperty(1, 2, 10);
                            label[1].classList.add("checkbox-active");
                            label[2].classList.add("checkbox-active");
                            label[7].classList.add("checkbox-active");
                            label[3].classList.add("checkbox-active");
                            label[11].classList.add("checkbox-active");
                            break;
                        }
                }
            }
        },
        /**
         * Thực hiện chức năng
         */
        excute(){
            var check = true;
            if(this.topK == 0 || this.topK > 63){
                if(this.typeF != "4"){
                    this.showPopUp = true;
                    this.message = "Số lượng kết quả phải lớn hơn 0 và nhỏ hơn 64";
                    this.typeError = 0;
                    check = false;
                }
                else if(this.step == 2){
                    this.showPopUp = true;
                    this.message = "Số lượng kết quả phải lớn hơn 0 và nhỏ hơn 64";
                    this.typeError = 0;
                    check = false;
                }
            }
            
            if(this.indexChoose.length == 0){
                this.showPopUp = true;
                this.message = "Bạn phải chọn ít nhất một điều kiện";
                this.typeError = 1;
                check = false;
            }
            else{
                var sumW = 0;
                for(var i=0; i<this.indexChoose.length; i++){
                    sumW += Number(this.w[this.indexChoose[i]]);
                }
                if(sumW != 1){
                    this.showPopUp = true;
                    this.message = "Tổng trọng số của các điều kiện được chọn phải bằng 1";
                    this.typeError = 1;
                    check = false;
                }
            }
                
            
            if(check){
                this.$emit('excuteMethod', this.indexChoose, this.w, this.topK, this.status);
            }
        },
        /**
         * Đóng popup
         */
        closePopUp(){
            this.showPopUp = false;
            if(this.typeError == 0){
                document.querySelector("#amount-result input").focus();
                this.typeError = -1;
            }
        }
    }
}
</script>

<style>
@import url(../../../css/form/condition.css);


</style>