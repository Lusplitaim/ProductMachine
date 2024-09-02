import Image from "next/image";

export default function Basket() {

    return (
        <div>
            <section className="section flex flex-col">
                <h1 className="title">Оформление заказа</h1>
            </section>
            <hr />
            <table className="table">
                <thead>
                    <tr>
                        <th>Товар</th>
                        <th>Количество</th>
                        <th>Цена</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <figure className="image is-2by3">
                                <Image src="/coca-cola.jpeg" width={50} height={75} alt="image" />
                            </figure>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}