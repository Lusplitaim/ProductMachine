import axios from "axios";
import BASE_API from "./base.api";
import { Coin } from "@/models/coin";

interface CoinsApi {
    getCoins(): Promise<Coin[]>,
}

const CoinsApi: CoinsApi = {
    getCoins: async function(): Promise<Coin[]> {
        return (await axios.get<Coin[]>(`${BASE_API}/coins`)).data;
    },
}

export default CoinsApi;