import { Coin } from "@/models/coin";
import NumberInput from "../number-input/NumberInput";
import { Link } from "react-router-dom";

export default function Payment() {
    const coins: Coin[] = [
        { nominal: 1, maxQuantity: 10, quantity: 0 },
        { nominal: 2, maxQuantity: 10, quantity: 0 },
        { nominal: 5, maxQuantity: 10, quantity: 0 },
        { nominal: 10, maxQuantity: 10, quantity: 0 },
    ];

    const coinsList = coins.map(coin => {
        return (
            <tr key={coin.nominal}>
                <td className="flex justify-center">
                    <h3>{coin.nominal} руб.</h3>
                </td>
                <td>
                    <NumberInput defaultValue={coin.quantity ?? 0} maxValue={coin.maxQuantity} minValue={0} />
                </td>
                <td className="text-center">
                    {coin.nominal * coin.quantity} руб.
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
                        <th>Номинал</th>
                        <th>Количество</th>
                        <th>Сумма</th>
                    </tr>
                </thead>
                <tbody>
                    {coinsList}
                </tbody>
            </table>
            <hr />
            <div className="flex flex-col h-24 container">
                <h3 className="self-end">Итоговая сумма: </h3>
                <div className="flex">
                    <button className="button is-warning"><Link to="/basket">Вернуться</Link></button>
                    <div className="mx-auto"></div>
                    <button className="button is-success"><Link to="/payment-success">Оплатить</Link></button>
                </div>
            </div>
        </div>
    );
}