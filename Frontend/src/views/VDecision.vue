<template>
    <div>
        <div class='process-option-list'>
            <router-link to="/Decision/0" exact>
                <div id="p-option0" class="p-option active">Vaccine</div>
            </router-link>
            <router-link to="/Decision/1" >
                <div id="p-option1" class="p-option recive-spend-option">Máy thở</div>
            </router-link>
            <router-link to="/Decision/2" >
                <div id="p-option2" class="p-option recive-spend-option">Giường bệnh</div>
            </router-link>
            <router-link to="/Decision/3" >
                <div id="p-option3" class="p-option recive-spend-option">Kit thử</div>
            </router-link>
            <router-link to="/Decision/4" >
                <div id="p-option4" class="p-option recive-spend-option">Bác sỹ</div>
            </router-link>
        </div>
        <span class="line-bottom"></span> 
        <div class="container">
            <div class="layout-dictionary d-flex">
                <div class="w-half domain">
                    <BaseTable
                    :listTh="listTh"
                    :datas="listData"
                    :hasEdit="false"
                    />
                </div>

                <div class="w-half domain" style="padding-top: 0">
                    <TheDecisionForm v-if="showForm1"
                    :conditions="listCondition"
                    :conditionE="listConditionE"
                    :weight="weight"
                    :typeF="type"
                    :step="step"
                    v-on:excuteMethod="excuteMethod"
                    ref="DecisionForm"
                    />

                    <TheOptionProvince v-else
                    :listP="listP"
                    :listProvince="province"
                    v-on:goToStep3="goToStep3"
                    />
                </div>
            </div>

            <BaseLoad
            :load="showLoad"
            />
        </div>
    </div>
  
</template>

<script>
import BaseLoad from '../components/base/BaseLoad.vue'
import BaseTable from '../components/base/BaseTable.vue'
import TheDecisionForm from '../components/layouts/forms/TheDecisionForm.vue'
import TheOptionProvince from '../components/layouts/forms/TheOptionProvince.vue'
import VaccineAPI from '../js/api/vaccince'
import BreathMachineAPI from  '../js/api/breathmachine'
import HospitalBedAPI from '../js/api/hospitalbed'
import TestKitAPI from '../js/api/testkit'
import DoctorAPI from '../js/api/doctor'
import ProvinceAPI from '../js/api/province'
export default {
    components: { BaseTable, TheDecisionForm, TheOptionProvince, BaseLoad },
    data(){
        return{
            type : null,
            step: 1,
            showLoad: false,
            listTh : [
                {name: 'STT', nameClass1: 'th-code', nameClass2: 'td-stt'},
                {name: 'TÊN TỈNH', nameClass1: 'th-name', nameClass2: 'td-name'},
                {name: 'SỐ LƯỢNG HỖ TRỢ', nameClass1: 'th-name', nameClass2: 'td-amount'},
                
            ],

            listData : [],
            listCondition : ["Số bệnh nhân đang điều trị", "Mức độ dịch", "Mật độ dân số", "Số lượng giường bệnh Covid-19",
                            "Số lượng kit thử Covid-19", "Số lượng bệnh nhân tử vong", "Số lượng bác sỹ có thể điều động", "Số lượng bệnh viện đa khoa", 
                            "Tỷ lệ tiêm mũi 1", "Tỷ lệ tiêm mũi 2", "Số lượng khu công nghiệp"],
            listConditionE : ["NumberCase", "EpidemicLevel", "PopulationDensity", "HospitalBed", 
                            "TestKit", "NumberDeath", "NumberDoctorDispatch", "NumberHospital",
                            "OneShotInjectionRate", "TwoShotInjectionRate", "IndustrialArea"],
            weight : [null, null, null, null, null, null, null, null, null, null, null],

            listP : [],
            province: [],
            listIDEx : [],
            showForm1: true,
        }
    },
    watch:{
        '$route'(to, from){
            this.type = to.params.id;
            if(!this.type){
                this.type = 0;
            }
            this.step = 1;
            this.showForm1 = true;
            setTimeout(()=>{
                this.$refs.DecisionForm.resetForm();
            },10)
            
            this.listData = [];
            var tab = document.querySelectorAll(".p-option");
            tab.forEach(e=>{
                if(e.id == "p-option"+this.type){
                    e.classList.add("active");
                }
                else{
                    e.classList.remove("active");
                }
            })
            from;
        }
    },
    created(){
        this.type = this.$route.params.id;
        this.step = 1;
    },
    methods:{
        async excuteMethod(indexChoose, w, topK, status){
            var condition;
            if(this.step == 1){
                var tmp = [];
                for(var i=0; i<indexChoose.length; i++){
                    var d = {
                        Property: this.listConditionE[indexChoose[i]],
                        Status: status[indexChoose[i]],
                        Weight: Number(w[indexChoose[i]]),
                    }

                    tmp.push(d);
                }
                condition = tmp;
            }
            else{
                var cd = [];
                for(i=0; i<indexChoose.length; i++){
                    d = {
                        Property: this.listConditionE[indexChoose[i]],
                        Status: status[indexChoose[i]],
                        Weight: Number(w[indexChoose[i]]),
                    }

                    cd.push(d);
                }
                var pe = [];
                for(i=0; i<this.listIDEx.length; i++){
                    pe.push(this.listIDEx[i]);
                }

                var pr = {
                    Conditions: cd,
                    ProvinceExcept: pe
                }

                condition = pr;
            }
            
            condition = JSON.stringify(condition);
            this.showLoad = true;
            switch(this.type){
                case "0":
                    await VaccineAPI.getDsss(topK, condition)
                    .then(res=>{
                        this.bindDataTabe(res.data.Data);
                        this.showLoad = false;
                    })
                    .catch(res=>{
                        res;
                        this.showLoad = false;
                    })
                    this.showForm1 = true;
                    this.step = 1;
                    break;
                case "1":
                    await BreathMachineAPI.getDsss(topK, condition)
                    .then(res=>{
                        this.bindDataTabe(res.data.Data);
                        this.showLoad = false;
                    })
                    .catch(res=>{
                        res;
                        this.showLoad = false;
                    })
                    this.showForm1 = true;
                    this.step = 1;
                    break;
                case "2":
                    await HospitalBedAPI.getDsss(topK, condition)
                    .then(res=>{
                        this.bindDataTabe(res.data.Data);
                        this.showLoad = false;
                    })
                    .catch(res=>{
                        res;
                        this.showLoad = false;
                    })
                    this.showForm1 = true;
                    this.step = 1;
                    break;
                case "3":
                    await TestKitAPI.getDsss(topK, condition)
                    .then(res=>{
                        this.bindDataTabe(res.data.Data);
                        this.showLoad = false;
                    })
                    .catch(res=>{
                        res;
                        this.showLoad = false;
                    })
                    this.showForm1 = true;
                    this.step = 1;
                    break;
                case "4":
                    if(this.step == 1){
                        await DoctorAPI.getElectre(condition)
                        .then(res=>{
                            this.step2Electre(res.data.Data);
                            this.showLoad = false;
                            this.showForm1 = false;
                        })
                        .catch(res=>{
                            res;
                            this.showLoad = false;
                        })
                        this.step = 2;
                    }
                    else{
                        await DoctorAPI.getDsss(topK, condition)
                        .then(res=>{
                            this.bindDataTabe(res.data.Data);
                            this.showLoad = false;
                            this.showForm1 = true;
                        })
                        .catch(res=>{
                            res;
                            this.showLoad = false;
                        })
                        this.step = 2;
                    }
                    break;
            }
            
        },

        /**
         * Bind data table
         */
        bindDataTabe(data){
            this.listData = [];
            var resultDevide = data.ListDevide;
            var province = data.ListProvince;
            for(var i=0; i<province.length; i++){
                if(i < resultDevide.length){
                    var p = [{field:i+1}, {field:province[i].ProvinceName}, {field:this.formatAmount(resultDevide[i])}];
                    this.listData.push(p);
                }
                else{
                    p = [{field:i+1}, {field:province[i].ProvinceName}, {field:0}];
                    this.listData.push(p);
                }
            }
        },
        /**
         * Định dạng lại số cho dễ đọc
         */
        formatAmount(value){
            let val = (value / 1).toFixed(0).replace(".", ",");
            return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        },
        /**
         * Buoc 2 cua electre
         */
        async step2Electre(data){
            await ProvinceAPI.getAll()
                .then(res=>{
                    this.province = res.data.Data;
                })
                .catch(res=>{
                    res;
                })
            this.listP = [];
            for(var i=0; i<data.length; i++){
                var p = {value: data[i].ProvinceName, check: true};
                this.listP.push(p);
            }

            for(i=0; i<this.province.length; i++){
                var tmp = false;
                for(var j=0; j<data.length; j++){
                    if(this.province[i].ProvinceName == data[j].ProvinceName){
                        tmp = true;
                        break;
                    }
                }

                if(!tmp){
                    this.listP.push({value: this.province[i].ProvinceName, check: false})
                }
            }

        },

        goToStep3(listID){
            this.listIDEx = listID;
            this.showForm1 = true;
        }
    }
}
</script>

<style>
@import url(../css/content/content.css);
</style>

<style scoped>
#provider-table{
  height: 82vh;
}
</style>