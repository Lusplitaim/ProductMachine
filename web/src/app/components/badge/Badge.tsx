import "./badge.css"

export function Badge(props: { content: string }) {
    return (
        <div className="badge">
            {props.content}
        </div>
    );
}