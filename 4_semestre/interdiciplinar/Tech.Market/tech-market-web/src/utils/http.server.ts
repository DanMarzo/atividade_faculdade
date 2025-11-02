import axios from "axios";
import https from "https";

const httpServer = axios.create({
    baseURL: process.env.URL,
    httpsAgent: new https.Agent({
        rejectUnauthorized: false
    })
});


export { httpServer }