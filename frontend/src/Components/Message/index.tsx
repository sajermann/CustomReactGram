import './index.css';

type Props = {
	msg: string;
	type: 'error' | 'success';
};

export function Message({ msg, type }: Props) {
	return (
		<div className={`message ${type}`}>
			<p>{msg}</p>
		</div>
	);
}
