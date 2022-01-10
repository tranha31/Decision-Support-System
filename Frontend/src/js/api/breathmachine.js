import BaseAPI from "./baseapi";

class BreathMachineAPI extends BaseAPI{
    constructor(){
        super();
        this.controler = "BreathMachine";
    }
}

export default new BreathMachineAPI();