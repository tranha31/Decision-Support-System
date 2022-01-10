import BaseAPI from "./baseapi";

class DoctorAPI extends BaseAPI{
    constructor(){
        super();
        this.controler = "Doctor";
    }
}

export default new DoctorAPI();