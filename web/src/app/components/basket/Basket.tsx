import { Product } from "@/models/product";
import Image from "next/image";
import NumberInput from "../number-input/NumberInput";
import { Link } from "react-router-dom";

export default function Basket() {
    const products: Product[] = [
        { id: 1, name: "Pepsi", price: 85, maxQuantity: 5, selected: false, quantity: 1 },
        { id: 2, name: "Coca-Cola", price: 96, maxQuantity: 4, selected: false, quantity: 1 },
        { id: 3, name: "Sprite", price: 101, maxQuantity: 1, selected: false, quantity: 1 },
        { id: 4, name: "Fanta", price: 79, maxQuantity: 2, selected: false, quantity: 1 },
    ];

    const productsTableData = products.map(p => {
        return (
            <tr key={p.id}>
                <td className="flex flex-row gap-2">
                    <figure className="image">
                        <Image src="/coca-cola.jpeg" width={100} height={150} alt="image" />
                    </figure>
                    <h3>{p.name}</h3>
                </td>
                <td>
                    <NumberInput defaultValue={p.quantity} maxValue={p.maxQuantity} />
                </td>
                <td className="text-center">
                    {p.price}
                </td>
            </tr>
        );
    });

    return (
        <div className="flex flex-col overflow-auto">
            <section className="section flex flex-col">
                <h1 className="title">Оформление заказа</h1>
            </section>
            <hr />
            <table className="table container">
                <thead>
                    <tr>
                        <th>Товар</th>
                        <th>Количество</th>
                        <th>Цена</th>
                    </tr>
                </thead>
                <tbody>
                    {productsTableData}
                </tbody>
            </table>
            <hr />
            <div className="flex flex-col h-24 container">
                <h3 className="self-end">Итоговая сумма: </h3>
                <div className="flex">
                    <button className="button is-warning"><Link to="/">Вернуться</Link></button>
                    <div className="mx-auto"></div>
                    <button className="button is-success"><Link to="/payment">Оплата</Link></button>
                </div>
            </div>
        </div>
    );
}