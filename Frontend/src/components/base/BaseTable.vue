<template>
    <div class="grid-content" id="provider-table">
        <table class="grid-table">
            <thead>
                <tr>
                    <th v-for="(v, index) in listTh" v-bind:key="index" class="th-viewer" :class="v.nameClass2">
                        {{v.name}}
                    </th>
                    <th v-if="hasEdit" class="th-viewer th-lastchild td-option" style="min-width:200px !important; width:200px !important; text-align:center">CHỨC NĂNG</th>
                </tr>
            </thead>
            <tbody v-if="datas.length > 0">
                <tr v-for="(v, index) in datas" v-bind:key="index" :id="index">
                    <td v-for="(td, i) in v"  v-bind:key="i" class="td-view" :class="td.class">{{td.field}}</td>
                    <td v-if="hasEdit" class="td-view td-option p-option" :id="'function-'+index" style="min-width:100px !important; width:100px !important">
                        <div class="function">
                            <button class="edit" @click="editData(index)">Sửa</button>
                        </div>
                    </td>
                </tr>
            </tbody>
            
        </table>
        <div v-if="datas.length == 0" class="empty-value">
            <div class="notification">
                <img src="https://actappg1.misacdn.net/img/bg_report_nodata.76e50bd8.svg">
                <div style="margin-top: 30px">Không có dữ liệu</div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "Table",
    components: { },
    props:{
        listTh : Array,
        datas : Array, 
        hasEdit : Boolean,
        lObject: Array,
    },
    methods:{
        editData(index){
            this.$emit("editData", index);
        }
    }
}
</script>

<style>
@import url(../../css/common/table.css);
.empty-value{
    height: 176px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    width: 100%;
    border: 1px solid #c7c7c7;
    border-top: unset;
    background-color: #fff;
}
.empty-value .notification{
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}
</style>