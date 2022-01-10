<template>
  <div class="popup-container">
        <div class="background">
        </div>
        <div class="main-form">
            <div class="content-form">
                <div v-if="type=='0'">
                    <div class="header">
                        <h1>Thông tin vaccine</h1>
                    </div>
                    <div class="content d-flex flex-column">
                        <div class="w-full d-flex mb-12">
                            <div class="w-half">
                                <p class="pb-6">Mã vaccine</p>
                                <input id="first-input" type="text" v-model="dataD.VaccineCode">
                            </div>
                            <div class="w-half">
                                <p class="pb-6">Loại vaccine</p>
                                <input type="text" v-model="dataD.VaccineName">
                            </div>
                        </div>
                        <div class="w-full d-flex">
                            <div class="w-half">
                                <p class="pb-6">Số lượng</p>
                                <input type="text" v-model="dataD.Amount">
                            </div>
                        </div>
                    </div>
                    
                </div>
                <div v-else-if="type=='1'">
                    <div class="header">
                        <h1>Thông tin máy thở</h1>
                    </div>
                    <div class="content d-flex flex-column">
                        <div class="w-full d-flex mb-12">
                            <div class="w-half">
                                <p class="pb-6">Mã máy thở</p>
                                <input id="first-input" type="text" v-model="dataD.BreathMachineCode">
                            </div>
                            <div class="w-half">
                                <p class="pb-6">Loại máy thở</p>
                                <input type="text" v-model="dataD.BreathMachineName">
                            </div>
                        </div>
                        <div class="w-full d-flex">
                            <div class="w-half">
                                <p class="pb-6">Số lượng</p>
                                <input type="text" v-model="dataD.Amount">
                            </div>
                        </div>
                    </div>
                </div>
                <div v-else-if="type=='2'">
                    <div class="header">
                        <h1>Thông tin giường bệnh</h1>
                    </div>
                    <div class="content d-flex flex-column">
                        <div class="w-full d-flex mb-12">
                            <div class="w-half">
                                <p class="pb-6">Mã giường bệnh</p>
                                <input id="first-input" type="text" v-model="dataD.HospitalBedCode">
                            </div>
                            <div class="w-half">
                                <p class="pb-6">Số lượng</p>
                                <input type="text" v-model="dataD.Amount">
                            </div>
                        </div>
                    </div>
                </div>
                <div v-else-if="type=='3'">
                    <div class="header">
                        <h1>Thông tin kit thử</h1>
                    </div>
                    <div class="content d-flex flex-column">
                        <div class="w-full d-flex mb-12">
                            <div class="w-half">
                                <p class="pb-6">Mã kít thử</p>
                                <input id="first-input" type="text" v-model="dataD.TestKitCode">
                            </div>
                            <div class="w-half">
                                <p class="pb-6">Loại kít thử</p>
                                <input type="text" v-model="dataD.TestKitName">
                            </div>
                        </div>
                        <div class="w-full d-flex">
                            <div class="w-half">
                                <p class="pb-6">Số lượng</p>
                                <input type="text" v-model="dataD.Amount">
                            </div>
                        </div>
                    </div>
                </div>
                <div v-else>
                    <div class="header">
                        <h1>Thông tin tỉnh/thành phố</h1>
                    </div>
                    <div class="content d-flex flex-column">
                        <div class="w-full d-flex mb-12">
                            <div class="w-half">
                                <p class="pb-6">Mã tỉnh/thành phố</p>
                                <input id="first-input" type="text" v-model="dataD.ProvinceCode">
                            </div>
                            <div class="w-half">
                                <p class="pb-6">Tên tỉnh/thành phố</p>
                                <input type="text" v-model="dataD.ProvinceName">
                            </div>
                        </div>
                        <div class="w-full d-flex mb-12">
                            <div class="w-half">
                                <p class="pb-6">Số lượng máy thở</p>
                                <input type="text" v-model="dataD.BreathMachine">
                            </div>
                            <div class="w-half">
                                <p class="pb-6">Cấp độ dịch</p>
                                <input type="text" v-model="dataD.EpidemicLevel">
                            </div>
                        </div>
                        <div class="w-full d-flex mb-12">
                            <div class="w-half">
                                <p class="pb-6">Số lượng giường bệnh</p>
                                <input type="text" v-model="dataD.HospitalBed">
                            </div>
                            <div class="w-half">
                                <p class="pb-6">Số lượng khu công nghiệp</p>
                                <input type="text" v-model="dataD.IndustrialArea">
                            </div>
                        </div>
                        <div class="w-full d-flex mb-12">
                            <div class="w-half">
                                <p class="pb-6">Số ca bệnh đang điều trị</p>
                                <input type="text" v-model="dataD.NumberCase">
                            </div>
                            <div class="w-half">
                                <p class="pb-6">Số ca bệnh tử vong</p>
                                <input type="text" v-model="dataD.NumberDeath">
                            </div>
                        </div>
                        <div class="w-full d-flex mb-12">
                            <div class="w-half">
                                <p class="pb-6">Số lượng bác sỹ lưu động</p>
                                <input type="text" v-model="dataD.NumberDoctorDispatch">
                            </div>
                            <div class="w-half">
                                <p class="pb-6">Số lượng bệnh viện đa khoa</p>
                                <input type="text" v-model="dataD.NumberHospital">
                            </div>
                        </div>
                        <div class="w-full d-flex mb-12">
                            <div class="w-half">
                                <p class="pb-6">Tỷ lệ tiêm mũi 1</p>
                                <input type="text" v-model="dataD.OneShotInjectionRate">
                            </div>
                            <div class="w-half">
                                <p class="pb-6">Tỷ lệ tiêm mũi 2</p>
                                <input type="text" v-model="dataD.TwoShotInjectionRate">
                            </div>
                        </div>
                        <div class="w-full d-flex mb-12">
                            <div class="w-half">
                                <p class="pb-6">Mật độ dân số</p>
                                <input type="text" v-model="dataD.PopulationDensity">
                            </div>
                            <div class="w-half">
                                <p class="pb-6">Số lượng kít thử</p>
                                <input type="text" v-model="dataD.TestKit">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="footer d-flex">
                    <div style="flex: 1"></div>
                    <div class="btn btn-default" @click="closeForm">Đóng</div>
                    <div class="btn btn-confirm" @click="saveForm">Lưu</div>
                </div>
            </div>
        </div>
  </div>
</template>

<script>
export default {
    props:{
        type: String,
        data: Object,
    },
    data(){
        return{
            dataD : this.data
        }
    },
    created(){
        setTimeout(()=>{
            document.getElementById("first-input").focus();
        },10)
    },
    watch:{
        data: function(){
            this.dataD = this.data
        }
    },
    methods:{
        /**
         * dong form
         */
        closeForm(){
            this.$emit("closeForm");
        },
        /**
         * Luu doi tuong
         */
        saveForm(){
            var d = JSON.stringify(this.dataD);
            this.$emit("saveObject", d);
        }
    }
}
</script>

<style>
@import url(../../../css/form/objectform.css);
</style>