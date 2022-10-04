import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { Message } from '../../Components/Message';
import { login, reset } from '../../Slices/authSlice';
import './index.css';

export default function Login() {
	const [email, setEmail] = useState('sajermannbruno@gmail.com');
	const [password, setPassword] = useState('310591');
	const dispatch = useDispatch();
	const { loading, error } = useSelector((state: any) => state.auth);

	function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();
		const user = {
			email,
			password,
		};

		// @ts-expect-error Esperado
		dispatch(login(user));
		console.log({ error });
	}

	useEffect(() => {
		dispatch(reset());
	}, [dispatch]);

	return (
		<div id="login">
			<h2>ReactGram</h2>
			<p className="subtitle">Faça login para ver o que há de novo.</p>
			<form onSubmit={handleSubmit}>
				<input
					type="email"
					placeholder="Email"
					value={email}
					onChange={e => setEmail(e.target.value)}
				/>
				<input
					type="password"
					placeholder="Senha"
					value={password}
					onChange={e => setPassword(e.target.value)}
				/>
				<input
					disabled={loading}
					type="submit"
					value={loading ? 'Aguarde...' : 'Entrar'}
				/>
				{error && <Message type="error" msg={error} />}
			</form>

			<p>
				Não tem uma conta? <Link to="/register">Clique aqui</Link>
			</p>
		</div>
	);
}
