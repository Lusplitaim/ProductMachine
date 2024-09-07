import { InsertedCoin } from "./insertedCoin";
import { OrderedProduct } from "./orderedProduct";

export interface CreateOrderModel {
    products: OrderedProduct[];
    coins: InsertedCoin[];
}