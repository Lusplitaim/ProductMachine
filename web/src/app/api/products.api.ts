import axios from "axios";
import BASE_API from "./base.api";
import { Product } from "@/models/product";
import { ProductFilters } from "@/models/productFilters";

interface ProductsApi {
    getProducts(filters: ProductFilters | undefined): Promise<Product[]>,
}

const ProductsApi: ProductsApi = {
    getProducts: async function(filters: ProductFilters): Promise<Product[]> {
        return (await axios.get<Product[]>(`${BASE_API}/products`, { params: { maxPrice: filters.maxPrice, brand: filters.brand } })).data;
    },
}

export default ProductsApi;