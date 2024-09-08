import axios from "axios";
import BASE_API from "./base.api";
import { CreateOrderModel } from "@/models/createOrderModel";
import { CoinChange } from "@/models/coinChange";

interface OrdersApi {
    createOrder(model: CreateOrderModel): Promise<CoinChange[]>,
}

const ordersApi: OrdersApi = {
    createOrder: async function(model: CreateOrderModel): Promise<CoinChange[]> {
        return (await axios.post<CoinChange[]>(`${BASE_API}/orders`, model)).data;
    },
}

export default ordersApi;