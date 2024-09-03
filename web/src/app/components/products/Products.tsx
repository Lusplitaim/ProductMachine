'use client'

import { Product } from "@/models/product";
import ProductCard from "../product-card/ProductCard"
import { useState } from "react";
import { Link } from "react-router-dom";

export default function Products() {
  const products: Product[] = [
    { id: 1, name: "Pepsi", price: 85, maxQuantity: 5, selected: false, quantity: 1 },
    { id: 2, name: "Coca-Cola", price: 96, maxQuantity: 4, selected: false, quantity: 1 },
    { id: 3, name: "Sprite", price: 101, maxQuantity: 1, selected: false, quantity: 1 },
    { id: 4, name: "Fanta", price: 79, maxQuantity: 2, selected: false, quantity: 1 },
    { id: 5, name: "Mamba", price: 120, maxQuantity: 0, selected: false, quantity: 0 },
  ];

  const [selectedProducts, setSelectedProducts] = useState([] as Product[]);

  const prodList = products.map(prod => <ProductCard product={prod} selected={prod.selected} onToggleStatus={toggleProductStatus} key={prod.id} />);

  function toggleProductStatus(id: number, selected: boolean) {
    const prod = products.find(prod => prod.id === id);
    if (selected) {
      setSelectedProducts(prev => [...prev, prod!]);
    } else {
      setSelectedProducts(prev => prev.filter(p => p.id !== prod?.id));
    }
  }

  return (
    <div>
      <section className="section flex flex-col">
        <h1 className="title">Газированные напитки</h1>
        <div className="flex flex-row justify-start">
          <div className="select is-link">
            <select>
              <option>Все бренды</option>
              <option>Pepsi</option>
            </select>
          </div>
          <div className="input is-link">
            <input type="range" min="0" max="100" />
          </div>
        </div>
        <button className="button is-info"><Link to={`basket`}>Выбрано: {selectedProducts.length}</Link></button>
      </section>
      <hr />
      <div className="container flex flex-row flex-wrap gap-x-10 scroll-smooth my-10">
        {prodList}
      </div>
    </div>
  );
}
