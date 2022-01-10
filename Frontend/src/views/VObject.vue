<template>
    <div>
        <div class='process-option-list'>
            <router-link to="/Object/0" exact>
                <div id="p-option0" class="p-option active">Vaccine</div>
            </router-link>
            <router-link to="/Object/1" >
                <div id="p-option1" class="p-option recive-spend-option">Máy thở</div>
            </router-link>
            <router-link to="/Object/2" >
                <div id="p-option2" class="p-option recive-spend-option">Giường bệnh</div>
            </router-link>
            <router-link to="/Object/3" >
                <div id="p-option3" class="p-option recive-spend-option">Kit thử</div>
            </router-link>
            <router-link to="/Object/4" >
                <div id="p-option4" class="p-option recive-spend-option">Tỉnh/Thành phố</div>
            </router-link>
        </div>
        <span class="line-bottom"></span> 
        <div class="container">
            <div class="layout-dictionary">
              <BaseTable
              :hasEdit="true"
              :listTh="listTh"
              :datas="listData"
              :lObject="listObject"
              v-on:editData="editData"
              />
            </div>
        </div>
      
      <TheObjectForm v-if="showForm"
      :type="type"
      :data="dataEdit"
      v-on:closeForm="closeForm"
      v-on:saveObject="saveObject"
      />

      <BaseLoad
      :load="showLoad"
      />

      <div id="toast"></div>
    </div>
</template>

<script>
import VaccineAPI from '../js/api/vaccince'
import BreathMachineAPI from  '../js/api/breathmachine'
import HospitalBedAPI from '../js/api/hospitalbed'
import TestKitAPI from '../js/api/testkit'
import ProvinceAPI from '../js/api/province'
import BaseTable from '../components/base/BaseTable.vue';
import BaseLoad from '../components/base/BaseLoad.vue'
import TheObjectForm from '../components/layouts/forms/TheObjectForm.vue';
import Toast from '../js/entity/toast'
export default {
  components: { BaseTable, BaseLoad, TheObjectForm },
  data(){
    return{
      toast : new Toast(),
      type : "0",
      listData: [],
      listTh : [],
      showLoad: false,
      listObject: [],
      showForm: false,
      dataEdit: null,
    }
  },
  watch:{
    '$route'(to, from){
        this.type = to.params.id;
        if(!this.type){
            this.type = 0;
        }
        var tab = document.querySelectorAll(".p-option");
        tab.forEach(e=>{
            if(e.id == "p-option"+this.type){
                e.classList.add("active");
            }
            else{
                e.classList.remove("active");
            }
        })

        this.bindData();
        from;
    }
  },
  created(){
    this.bindData();
  },
  methods:{
    async bindData(){
      this.showLoad = true;
      switch(this.type){
        case "0":
          await VaccineAPI.getAll()
            .then(res=>{
              this.bindVaccine(res.data.Data);
              this.showLoad = false;
            })
            .catch(res=>{
              res;
              this.showLoad = false;
            })
          break;
        case "1":
          await BreathMachineAPI.getAll()
            .then(res=>{
              this.bindBreathMachine(res.data.Data);
              this.showLoad = false;
            })
            .catch(res=>{
              res;
              this.showLoad = false;
            })
          break;
        case "2":
          await HospitalBedAPI.getAll()
            .then(res=>{
              this.bindHospitalBed(res.data.Data);
              this.showLoad = false;
            })
            .catch(res=>{
              res;
              this.showLoad = false;
            })
          break;
        case "3":
          await TestKitAPI.getAll()
            .then(res=>{
              this.bindTestKit(res.data.Data);
              this.showLoad = false;
            })
            .catch(res=>{
              res;
              this.showLoad = false;
            })
          break;
        case "4":
          await ProvinceAPI.getAll()
            .then(res=>{
              this.bindProvince(res.data.Data);
              this.showLoad = false;
            })
            .catch(res=>{
              res;
              this.showLoad = false;
            })
          break;
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
     * Bind bang vaccine
     */
    bindVaccine(data){
      this.listTh = [];
      this.listData = [];
      this.listObject = [];
      this.listObject = data;
      var Th = [
        {name: 'STT', nameClass1: 'th-code', nameClass2: 'td-stt'},
        {name: 'MÃ VACCINE', nameClass1: 'th-code', nameClass2: 'td-code'},
        {name: 'LOẠI VACCINE', nameClass1: 'th-name', nameClass2: 'td-name'},
        {name: 'SỐ LƯỢNG', nameClass1: 'th-name', nameClass2: 'td-name'},
      ];
      this.listTh = Th;
      var d = [];
      for(var i=0; i<data.length; i++){
        var p = [{field:i+1}, {field:data[i].VaccineCode}, {field:data[i].VaccineName}, {field:this.formatAmount(data[i].Amount)}];
        d.push(p);
      }
      this.listData = d;
    },
    /**
     * Bind bang may tho
     */
    bindBreathMachine(data){
      this.listTh = [];
      this.listData = [];
      this.listObject = [];
      this.listObject = data;
      var Th = [
        {name: 'STT', nameClass1: 'th-code', nameClass2: 'td-stt'},
        {name: 'MÃ MÁY THỞ', nameClass1: 'th-code', nameClass2: 'td-code'},
        {name: 'LOẠI MÁY THỞ', nameClass1: 'th-name', nameClass2: 'td-name'},
        {name: 'SỐ LƯỢNG', nameClass1: 'th-name', nameClass2: 'td-name'},
      ];
      this.listTh = Th;
      var d = [];
      for(var i=0; i<data.length; i++){
        var p = [{field:i+1}, {field:data[i].BreathMachineCode}, {field:data[i].BreathMachineName}, {field:this.formatAmount(data[i].Amount)}];
        d.push(p);
      }
      this.listData = d;
    },

    /**
     * Bind bang giuong benh
     */
    bindHospitalBed(data){
      this.listTh = [];
      this.listData = [];
      this.listObject = [];
      this.listObject = data;
      var Th = [
        {name: 'STT', nameClass1: 'th-code', nameClass2: 'td-stt'},
        {name: 'MÃ GIƯỜNG BỆNH', nameClass1: 'th-code', nameClass2: 'td-name'},
        {name: 'SỐ LƯỢNG', nameClass1: 'th-name', nameClass2: 'td-name'},
      ];
      this.listTh = Th;
      var d = [];
      for(var i=0; i<data.length; i++){
        var p = [{field:i+1}, {field:data[i].HospitalBedCode}, {field:this.formatAmount(data[i].Amount)}];
        d.push(p);
      }
      this.listData = d;
    },

    /**
     * Bind bang kit thu
     */
    bindTestKit(data){
      this.listTh = [];
      this.listData = [];
      this.listObject = [];
      this.listObject = data;
      var Th = [
        {name: 'STT', nameClass1: 'th-code', nameClass2: 'td-stt'},
        {name: 'MÃ KIT THỬ', nameClass1: 'th-code', nameClass2: 'td-code'},
        {name: 'LOẠI KIT THỬ', nameClass1: 'th-name', nameClass2: 'td-name'},
        {name: 'SỐ LƯỢNG', nameClass1: 'th-name', nameClass2: 'td-name'},
      ];
      this.listTh = Th;
      var d = [];
      for(var i=0; i<data.length; i++){
        var p = [{field:i+1}, {field:data[i].TestKitCode}, {field:data[i].TestKitName}, {field:this.formatAmount(data[i].Amount)}];
        d.push(p);
      }
      this.listData = d;
    },

    /**
     * Bind bang Tinh/TP
     */
    bindProvince(data){
      this.listTh = [];
      this.listData = [];
      this.listObject = [];
      this.listObject = data;
      var Th = [
        {name: 'STT', nameClass1: 'th-code', nameClass2: 'td-stt'},
        {name: 'MÃ TỈNH/TP', nameClass1: 'th-code', nameClass2: 'td-default'},
        {name: 'TÊN TỈNH/TP', nameClass1: 'th-name', nameClass2: 'td-name'},
        {name: 'SỐ CA BỆNH ĐANG ĐIỀU TRỊ', nameClass1: 'th-default', nameClass2: 'td-default'},
        {name: 'SỐ CA BỆNH TỬ VONG', nameClass1: 'th-default', nameClass2: 'td-default'},
        {name: 'SL BÁC SỸ LƯU ĐỘNG', nameClass1: 'th-default', nameClass2: 'td-default'},
        {name: 'TỶ LỆ TIÊM MŨI 1', nameClass1: 'th-default', nameClass2: 'td-default'},
        {name: 'TỶ LỆ TIÊM MŨI 2', nameClass1: 'th-default', nameClass2: 'td-default'},
        {name: 'CẤP ĐỘ DỊCH BỆNH', nameClass1: 'th-default', nameClass2: 'td-default'},
      ];
      this.listTh = Th;
      var d = [];
      for(var i=0; i<data.length; i++){
        var p = [
          {field:i+1}, 
          {field:data[i].ProvinceCode}, 
          {field:data[i].ProvinceName},
          {field:this.formatAmount(data[i].NumberCase)},
          {field:this.formatAmount(data[i].NumberDeath)},
          {field:this.formatAmount(data[i].NumberDoctorDispatch)}, 
          {field:data[i].OneShotInjectionRate},
          {field:data[i].TwoShotInjectionRate},
          {field:data[i].EpidemicLevel},
        ];
        d.push(p);
      }
      this.listData = d;
    },

    closeForm(){
      this.showForm = false;
    },

    editData(index){
      this.dataEdit = null;
      this.dataEdit = this.listObject[index];
      this.showForm = true;
    },

    async saveObject(d){
      this.showLoad = true;
      this.showForm = false;
      switch(this.type){
        case "0":
          await VaccineAPI.update(d)
            .then(res=>{
              res;
              this.showLoad = false;
              this.toast.toast({
                action: "Thành công:",
                content: "Đã cập nhật",
                type: "success",
                duration: 3000,
              })
            })
            .catch(res=>{
              res;
              this.showLoad = false;
              this.toast.toast({
                action: "Thất bại:",
                content: "Không thể cập nhật",
                type: "error",
                duration: 3000,
              })
            })
          break;
        case "1":
          await BreathMachineAPI.update(d)
            .then(res=>{
              res;
              this.showLoad = false;
              this.toast.toast({
                action: "Thành công:",
                content: "Đã cập nhật",
                type: "success",
                duration: 3000,
              })
            })
            .catch(res=>{
              res;
              this.showLoad = false;
              this.toast.toast({
                action: "Thất bại:",
                content: "Không thể cập nhật",
                type: "error",
                duration: 3000,
              })
            })
          break;
        case "2":
          await HospitalBedAPI.update(d)
            .then(res=>{
              res;
              this.toast.toast({
                action: "Thành công:",
                content: "Đã cập nhật",
                type: "success",
                duration: 3000,
              })
              this.showLoad = false;
            })
            .catch(res=>{
              res;
              this.showLoad = false;
              this.toast.toast({
                action: "Thất bại:",
                content: "Không thể cập nhật",
                type: "error",
                duration: 3000,
              })
            })
          break;
        case "3":
          await TestKitAPI.update(d)
            .then(res=>{
              res;
              this.toast.toast({
                action: "Thành công:",
                content: "Đã cập nhật",
                type: "success",
                duration: 3000,
              })
              this.showLoad = false;
            })
            .catch(res=>{
              res;
              this.showLoad = false;
              this.toast.toast({
                action: "Thất bại:",
                content: "Không thể cập nhật",
                type: "error",
                duration: 3000,
              })
            })
          break;
        case "4":
          await ProvinceAPI.update(d)
            .then(res=>{
              res;
              this.toast.toast({
                action: "Thành công:",
                content: "Đã cập nhật",
                type: "success",
                duration: 3000,
              })
              this.showLoad = false;
            })
            .catch(res=>{
              res;
              this.showLoad = false;
              this.toast.toast({
                action: "Thất bại:",
                content: "Không thể cập nhật",
                type: "error",
                duration: 3000,
              })
            })
          break;
      }

      this.bindData();
    }
  }
}
</script>

<style>
#provider-table{
  margin-top: 5px;
}
@import url(../css/common/toast.css);
</style>

<style scoped>
#provider-table{
  height: 86vh;
}
</style>