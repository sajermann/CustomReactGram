import './index.css';

export default function Home() {
	function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();
	}
	return <div id="login">home</div>;
}
