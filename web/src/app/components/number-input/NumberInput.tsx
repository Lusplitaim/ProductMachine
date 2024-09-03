import { useState } from "react";

export default function NumberInput(props: { defaultValue: number, maxValue: number }) {
    const [val, setVal] = useState(props.defaultValue);

    return (
        <div className="flex flex-row gap-2 justify-center">
            <button className="button is-danger is-light">-</button>
            <input className="text-center w-10" type="number" value={val} />
            <button className="button is-success is-light">+</button>
        </div>
    );
}