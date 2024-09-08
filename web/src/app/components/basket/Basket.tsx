import { Product } from "@/models/product";
import Image from "next/image";
import NumberInput from "../number-input/NumberInput";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { useImmer } from "use-immer";
import { FaRegTrashAlt } from "react-icons/fa";

export default function Basket() {
    const [products, setProducts] = useImmer([] as Product[]);
    const navigate = useNavigate();

    useEffect(() => {
        setProducts(JSON.parse(getSelectedItems()));
    }, []);

    function getSelectedItems(): string {
        return localStorage.getItem('selectedProducts') ?? '';
    }

    function updateProductQuantity(productId: number) {
        return (val: number) => {
            setProducts(prods => {
                const prod = prods.find(p => p.id === productId)!;
                prod.quantity = val;
            });
        };
    }

    function removeProduct(id: number) {
        return () => {
            setProducts(prev => {
                return prev.filter(p => p.id !== id);
            });
        };
    }

    const productsTableData = products.map(p => {
        return (
            <tr key={p.id}>
                <td>
                    <div className="flex flex-row gap-2 items-center">
                        <figure className="image">
                            <Image src="/coca-cola.jpeg" width={100} height={150} alt="image" />
                        </figure>
                        <h3 className="is-size-4">{p.name}</h3>
                    </div>
                </td>
                <td>
                    <div className="flex flex-col justify-center">
                        <NumberInput value={p.quantity ?? 0} maxValue={p.maxQuantity} minValue={0} onChange={updateProductQuantity(p.id)} />
                    </div>
                </td>
                <td>
                    <p className="text-center">
                        {p.price}
                    </p>
                </td>
                <td>
                    <span className="icon">
                        <button onClick={removeProduct(p.id)}><FaRegTrashAlt /></button>
                    </span>
                </td>
            </tr>
        );
    });

    function getTotalSum(): number {
        let totalSum = 0;
        for (const p of products) {
            if (p.quantity) {
                totalSum += p.price * p.quantity;
            }
        }
        return totalSum;
    }

    function navigateToPayment() {
        localStorage.setItem('totalSum', getTotalSum().toString());
        localStorage.setItem('selectedProducts', JSON.stringify(products));

        navigate('/payment');
    }

    function navigateBack() {
        navigate('/');
    }

    const content = products.length
        ? (
            <>
                <table className="table container">
                    <thead>
                        <tr>
                            <th>Товар</th>
                            <th>Количество</th>
                            <th>Цена</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {productsTableData}
                    </tbody>
                </table>
                <hr />
                <div className="flex flex-col h-24 container">
                    <h3 className="self-end">Итоговая сумма: {getTotalSum()}</h3>
                    <div className="flex">
                        <button className="button is-warning is-medium" onClick={navigateBack}>Вернуться</button>
                        <div className="mx-auto"></div>
                        <button className="button is-success is-medium" disabled={getTotalSum() <= 0} onClick={navigateToPayment}>Оплата</button>
                    </div>
                </div>
            </>
        )
        : (
            <div className="container flex flex-col items-center justify-center">
                <p className="is-size-4">У вас нет ни одного товара, вернитесь на страницу каталога</p>
                <button className="button is-warning is-medium" onClick={navigateBack}>Вернуться</button>
            </div>
        );

    return (
        <div className="flex flex-col overflow-auto">
            <section className="section container flex flex-col">
                <h1 className="title">Оформление заказа</h1>
            </section>
            <hr />
            {content}
        </div>
    );
}