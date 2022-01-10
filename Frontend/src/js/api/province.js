import BaseAPI from "./baseapi";

class ProvinceAPI extends BaseAPI{
    constructor(){
        super();
        this.controler = "Province";
    }
}

export default new ProvinceAPI();