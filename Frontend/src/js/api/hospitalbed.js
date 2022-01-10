import BaseAPI from "./baseapi";

class HospitalBedAPI extends BaseAPI{
    constructor(){
        super();
        this.controler = "HospitalBed";
    }
}

export default new HospitalBedAPI();