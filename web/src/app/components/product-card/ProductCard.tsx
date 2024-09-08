import Image from "next/image";
import './product-card.css';
import { Product } from "@/models/product";
import { useState } from "react";

export default function ProductCard(props: { product: Product, selected: boolean, onToggleStatus: (id: number, selected: boolean) => void }) {
    const prod = props.product;
    const [selected, setSelected] = useState(props.selected);

    let selectBtnText = "Выбрать";
    let selectBtnClass = "is-warning";
    let selectBtnDisabled = false;
    if (!prod.maxQuantity) {
        selectBtnText = "Закончился";
        selectBtnClass = "is-light";
        selectBtnDisabled = true;
    } else if (selected) {
        selectBtnText = "Выбрано";
        selectBtnClass = "is-success";
    }

    const selectBtnClasses = ["button", selectBtnClass];

    function toggleSelected() {
        setSelected(prev => !prev);
        props.onToggleStatus(props.product.id, !selected);
    }

    return (
        <div className="card basis-1/5">
            <div className="card-image">
                <figure className="image is-2by3">
                    <Image src="/coca-cola.jpeg" width={100} height={150} alt="image" />
                </figure>
            </div>
            <header className="card-header flex flex-row">
                <p className="card-header-title justify-center">{prod.name}</p>
            </header>
            <div className="card-content flex flex-col gap-5 justify-center items-center">
                <p>{prod.price} руб.</p>
                <button className={selectBtnClasses.join(' ')} onClick={toggleSelected} disabled={selectBtnDisabled}>{selectBtnText}</button>
            </div>
        </div>
    );
}