import axios from "axios";
import BASE_API from "./base.api";
import { Product } from "@/models/product";

interface ProductsApi {
    getProducts(): Promise<Product[]>,
}

const ProductsApi: ProductsApi = {
    getProducts: async function(): Promise<Product[]> {
        return (await axios.get<Product[]>(`${BASE_API}/products`)).data;
    },
}

export default ProductsApi;