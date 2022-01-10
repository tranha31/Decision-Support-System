import axios from "axios";

const baseDomain = "https://localhost:44358";
const baseURL = `${baseDomain}/v1/`;

export default axios.create({
    baseURL,
    headers: {
        "Content-Type" : "application/json-patch+json",
        "Authorization": "Bearer yourToken", 
    } ,
    
})