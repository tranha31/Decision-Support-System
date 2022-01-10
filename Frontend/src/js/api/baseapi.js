import Repository from "./repository";

export default class BaseAPI {

    constructor() {
        this.controler = null;
    }
    /**
     * Phương thức lấy tất cả dữ liệu
     */
    getAll() {
        return Repository.get(`${this.controler}`);
    }
    /**
     * Hàm cập nhật dữ liệu
     * @param {*} body 
     */
    update(body) {
        return Repository.put(`${this.controler}`, body);
    }
    /**
     * hàm dss
     */
    getDsss(topK, body){
        return Repository.post(`${this.controler}/dss?k=${topK}`, body);
    }
    /**
     * Hàm electre
     */
    getElectre(body){
        return Repository.post(`${this.controler}/electre`, body);
    }
}