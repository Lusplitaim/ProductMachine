'use client'

import { Product } from "@/models/product";
import ProductCard from "../product-card/ProductCard"
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import brandsApi from "@/app/api/brands.api";
import { Brand } from "@/models/brand";
import productsApi from "@/app/api/products.api";

export default function Products() {
  useEffect(() => {
    const getBrands = async () => {
      const brands = await brandsApi.getBrands();

      setBrands(brands);
    };

    const getProducts = async () => {
      const products = await productsApi.getProducts();

      setProducts(products);
    };

    getBrands();
    getProducts();
  }, []);

  const [selectedProducts, setSelectedProducts] = useState([] as Product[]);
  const [brands, setBrands] = useState([] as Brand[]);
  const [products, setProducts] = useState([] as Product[]);
  const navigate = useNavigate();

  const prodList = products.map(prod => <ProductCard product={prod} selected={prod.selected} onToggleStatus={toggleProductStatus} key={prod.id} />);

  const brandsList = brands.map(b => <option key={b.id}>{b.name}</option>);

  function toggleProductStatus(id: number, selected: boolean) {
    const prod = products.find(prod => prod.id === id);
    if (selected) {
      setSelectedProducts(prev => [...prev, prod!]);
    } else {
      setSelectedProducts(prev => prev.filter(p => p.id !== prod?.id));
    }
  }

  function navigateToBasket() {
    localStorage.setItem('selectedProducts', JSON.stringify(selectedProducts));
    navigate('basket');
  }

  return (
    <div>
      <section className="container section flex flex-col">
        <h1 className="title">Газированные напитки</h1>
        <div className="flex flex-row justify-start">
          <div className="select is-link">
            <select>
              {[<option key="0">Все бренды</option>, ...brandsList]}
            </select>
          </div>
          <div className="input is-link">
            <input type="range" min="0" max="100" />
          </div>
        </div>
        <button className="button is-info" disabled={!selectedProducts.length} onClick={navigateToBasket}>Выбрано: {selectedProducts.length}</button>
      </section>
      <hr />
      <div className="container flex flex-row flex-wrap gap-x-10 scroll-smooth my-10">
        {prodList}
      </div>
    </div>
  );
}
