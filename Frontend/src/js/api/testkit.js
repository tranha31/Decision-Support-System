import BaseAPI from "./baseapi";

class TestKitAPI extends BaseAPI{
    constructor(){
        super();
        this.controler = "TestKit";
    }
}

export default new TestKitAPI();