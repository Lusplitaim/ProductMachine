import axios from "axios";
import { Brand } from "@/models/brand";
import BASE_API from "./base.api";

interface BrandsApi {
    getBrands(): Promise<Brand[]>,
}

const BrandsApi: BrandsApi = {
    getBrands: async function(): Promise<Brand[]> {
        return (await axios.get<Brand[]>(`${BASE_API}/productbrands`)).data;
    },
}

export default BrandsApi;