export default function NumberInput(props: { value: number, maxValue?: number, minValue: number, onChange: (val: number) => void }) {
    function decrease() {
        const val = props.value;
        const result = val === props.minValue ? val : val - 1;
        props.onChange(result);
    }

    function increase() {
        const val = props.value;
        const result = val === props.maxValue ? val : val + 1;
        props.onChange(result);
    }

    return (
        <div className="flex flex-row gap-2">
            <button className="button is-danger is-light" onClick={decrease}>-</button>
            <input className="text-center w-10" type="number" value={props.value} />
            <button className="button is-success is-light" onClick={increase}>+</button>
        </div>
    );
}