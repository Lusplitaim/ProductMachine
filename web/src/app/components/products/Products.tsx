'use client'

import { Product } from "@/models/product";
import ProductCard from "../product-card/ProductCard"
import { useState } from "react";
import { Link } from "react-router-dom";

export default function Products() {
  const products: Product[] = [
    { id: 1, name: "Pepsi", price: 85, quantity: 5, selected: false },
    { id: 2, name: "Coca-Cola", price: 96, quantity: 4, selected: false },
    { id: 3, name: "Sprite", price: 101, quantity: 1, selected: false },
    { id: 4, name: "Fanta", price: 79, quantity: 2, selected: false },
    { id: 5, name: "Mamba", price: 120, quantity: 0, selected: false },
  ];

  const [selectedProducts, setSelectedProducts] = useState([] as Product[]);

  const prodList = products.map(prod => <ProductCard product={prod} selected={true} onToggleStatus={toggleProductStatus} key={prod.id} />);

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
      <div className="container flex flex-row flex-wrap gap-x-10 scroll-smooth">
        {prodList}
      </div>
    </div>
  );
}
