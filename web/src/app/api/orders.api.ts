import axios from "axios";
import BASE_API from "./base.api";
import { CreateOrderModel } from "@/models/createOrderModel";

interface OrdersApi {
    createOrder(model: CreateOrderModel): Promise<void>,
}

const ordersApi: OrdersApi = {
    createOrder: async function(model: CreateOrderModel): Promise<void> {
        return await axios.post(`${BASE_API}/orders`, model);
    },
}

export default ordersApi;