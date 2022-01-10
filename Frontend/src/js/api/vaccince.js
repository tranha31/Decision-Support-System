import BaseAPI from "./baseapi";

class VaccineAPI extends BaseAPI{
    constructor(){
        super();
        this.controler = "Vaccine";
    }
}

export default new VaccineAPI();