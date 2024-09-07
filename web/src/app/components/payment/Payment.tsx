import { Coin } from "@/models/coin";
import NumberInput from "../number-input/NumberInput";
import { useImmer } from "use-immer";
import { useEffect } from "react";
import coinsApi from "@/app/api/coins.api";
import { useNavigate } from "react-router-dom";
import ordersApi from "@/app/api/orders.api";
import { CreateOrderModel } from "@/models/createOrderModel";
import { Product } from "@/models/product";
import { OrderedProduct } from "@/models/orderedProduct";
import { InsertedCoin } from "@/models/insertedCoin";

export default function Payment() {
    const [coins, setCoins] = useImmer([] as Coin[]);
    const navigate = useNavigate();
    let totalSum = JSON.parse(localStorage.getItem('totalSum') ?? '') as number;

    useEffect(() => {
        const getCoins = async () => {
            const coins = await coinsApi.getCoins();
            coins.forEach(c => c.quantity = 0);

            setCoins(coins);
        };

        getCoins();
    }, []);


    function updateCoinQuantity(nominal: number) {
        return (val: number) => {
            setCoins(prev => {
                const coin = prev.find(c => c.nominal === nominal)!;
                coin.quantity = val;
            });
        };
    }

    function getInsertedSum(): number {
        let sum = 0;
        coins.forEach(c => sum += c.quantity * c.nominal);
        return sum;
    }

    const coinsList = coins.map(coin => {
        return (
            <tr key={coin.nominal}>
                <td className="flex justify-center">
                    <h3>{coin.nominal} руб.</h3>
                </td>
                <td>
                    <NumberInput value={coin.quantity ?? 0} maxValue={coin.maxQuantity} minValue={0} onChange={updateCoinQuantity(coin.nominal)} />
                </td>
                <td className="text-center">
                    {coin.nominal * coin.quantity ?? 0} руб.
                </td>
            </tr>
        );
    });

    function navigateBack() {
        navigate('/basket');
    }

    function createOrder() {
        const selectedProducts = JSON.parse(localStorage.getItem('selectedProducts') ?? '') as Product[];
        const model: CreateOrderModel = {
            products: selectedProducts.map(p => ({ id: p.id, quantity: p.quantity } as OrderedProduct)),
            coins: coins.map(c => ({ nominal: c.nominal, quantity: c.quantity } as InsertedCoin)),
        };
        ordersApi.createOrder(model)
            .then(_ => {
                navigate('/payment-success');
            },);
    }

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
            <div className="flex flex-col gap-3 h-24 container">
                <div className="flex justify-end gap-6">
                    <h3 className="self-end">Внесенная сумма: {getInsertedSum()}</h3>
                    <h3 className="self-end">Итоговая сумма: {totalSum}</h3>
                </div>
                <div className="flex">
                    <button className="button is-warning" onClick={navigateBack}>Вернуться</button>
                    <div className="mx-auto"></div>
                    <button className="button is-success" disabled={totalSum > getInsertedSum()} onClick={createOrder}>Оплатить</button>
                </div>
            </div>
        </div>
    );
}