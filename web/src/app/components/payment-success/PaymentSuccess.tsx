import { useNavigate } from "react-router-dom";
import { Badge } from "../badge/Badge";
import { CoinChange } from "@/models/coinChange";
import { useEffect } from "react";
import { useImmer } from "use-immer";

export default function PaymentSuccess() {
    const navigate = useNavigate();
    const [coins, setCoins] = useImmer([] as CoinChange[]);
    const [failed, setFailed] = useImmer(false);

    useEffect(() => {
        const coinsString = localStorage.getItem('change');
        const orderFailed = !coinsString;
        setFailed(orderFailed);

        setCoins(prev => orderFailed ? prev : JSON.parse(coinsString ?? '[]') as CoinChange[]);
    }, []);

    function getChangeSum(): number {
        return coins.map(c => c.nominal * c.quantity).reduce((a, b) => a + b, 0);
    }

    const coinElements = coins.map(c => (
        <div className="flex gap-4 justify-center items-center my-3">
            <Badge content={c.nominal.toString()} />
            <p>{c.quantity} шт.</p>
        </div>
    ));

    function navigateToProducts() {
        navigate('/');
    }

    const content = failed
        ? (
            <>
                <p className="has-text-error">Извините, в данный момент мы не можем продать вам товар по причине того, что автомат не может выдать вам нужную сдачу.</p>
            </>
        )
        : (
            <>
                <p>Спасибо за покупку!</p>
                <p>Пожалуйста, возьмите вашу сдачу: <b className="has-text-success">{getChangeSum()} руб.</b></p>
                <p className="mt-10">Ваши монеты:</p>
                {coinElements}
            </>
        );

    return (
        <div className="container flex flex-col items-center justify-center h-screen is-size-4">
            {content}
            <button className="button is-warning is-medium mt-10" onClick={navigateToProducts}>Вернуться</button>
        </div>
    );
}