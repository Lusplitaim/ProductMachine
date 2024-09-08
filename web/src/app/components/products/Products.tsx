'use client'

import { Product } from "@/models/product";
import ProductCard from "../product-card/ProductCard"
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import brandsApi from "@/app/api/brands.api";
import { Brand } from "@/models/brand";
import productsApi from "@/app/api/products.api";
import { ProductFilters } from "@/models/productFilters";

export default function Products() {
  useEffect(() => {
    const getBrands = async () => {
      const brands = await brandsApi.getBrands();

      setBrands(brands);
    };

    getBrands();
    getProducts();
  }, []);

  const [selectedProducts, setSelectedProducts] = useState([] as Product[]);
  const [brands, setBrands] = useState([] as Brand[]);
  const [products, setProducts] = useState([] as Product[]);
  const navigate = useNavigate();
  
  const [maxPrice, setMaxPrice] = useState(0);
  const [minPrice, setMinPrice] = useState(0);
  const [selectedMaxPrice, setSelectedMaxPrice] = useState<number>(0);
  const [selectedBrand, setSelectedBrand] = useState(0);

  const prodList = products.map(prod => <ProductCard product={prod} selected={prod.selected} onToggleStatus={toggleProductStatus} key={prod.id} />);

  const brandsList = brands.map(b => <option key={b.id} value={b.id}>{b.name}</option>);

  const getProducts = async () => {
    const filters: ProductFilters = {
      maxPrice: !selectedMaxPrice ? undefined : selectedMaxPrice,
      brand: !selectedBrand ? undefined : selectedBrand,
    };
    const products = await productsApi.getProducts(filters);

    setProducts(products);
    setMinPrice(_ => {
      return Math.min(...products.map(p => p.price));
    });
    setMaxPrice(_ => {
      return Math.max(...products.map(p => p.price));
    });
    setSelectedMaxPrice(_ => {
      return Math.max(...products.map(p => p.price));
    });
  };

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
      <section className="container section">
        <h1 className="title">Газированные напитки</h1>
        <div className="flex flex-row justify-start gap-10">
          <div className="select is-link">
            <select className="w-80" value={selectedBrand} onInput={e => setSelectedBrand(+e.currentTarget.value)}>
              {[<option key="0" value="0">Все бренды</option>, ...brandsList]}
            </select>
          </div>
          <div className="flex flex-col justify-end">
            <div className="flex justify-between">
              <h3>{minPrice} руб.</h3>
              <h3>{maxPrice} руб.</h3>
            </div>
            <input className="w-80" type="range" value={selectedMaxPrice} min={minPrice} max={maxPrice} onInput={e => setSelectedMaxPrice(+e.currentTarget.value)} />
            <h3>{selectedMaxPrice}</h3>
          </div>
        </div>
        <div className="flex justify-between">
          <button className="button is-info is-medium" onClick={getProducts}>Найти</button>
          <button className="button is-success is-medium" disabled={!selectedProducts.length} onClick={navigateToBasket}>Выбрано: {selectedProducts.length}</button>
        </div>
      </section>
      <hr />
      <div className="container flex flex-row flex-wrap gap-x-10 scroll-smooth my-10">
        {prodList}
      </div>
    </div>
  );
}
