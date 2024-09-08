import { Coin } from "@/models/coin";
import { useNavigate } from "react-router-dom";
import { Badge } from "../badge/Badge";

export default function PaymentSuccess() {
    const navigate = useNavigate();
    const changeSum = 34;
    const coins: Coin[] = [
        {nominal: 1, quantity: 2, maxQuantity: 0},
        {nominal: 2, quantity: 1, maxQuantity: 0},
        {nominal: 5, quantity: 0, maxQuantity: 0},
        {nominal: 10, quantity: 0, maxQuantity: 0},
    ];

    const coinElements = coins.map(c => (
        <div className="flex gap-4 justify-center items-center my-3">
            <Badge content={c.nominal.toString()} />
            <p>{c.quantity} шт.</p>
        </div>
    ));

    function navigateToProducts() {
        navigate('');
    }

    return (
        <div className="container flex flex-col items-center justify-center h-screen is-size-4">
            <p>Спасибо за покупку!</p>
            <p>Пожалуйста, возьмите вашу сдачу: <b className="has-text-success">{changeSum} руб.</b></p>
            <p className="mt-10">Ваши монеты:</p>
            {coinElements}
            <button className="button is-warning is-medium mt-10" onClick={navigateToProducts}>Вернуться</button>
        </div>
    );
}