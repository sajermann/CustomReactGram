import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useHistory } from 'react-router-dom';
import { Message } from '../../Components/Message';
import { register, reset } from '../../Slices/authSlice';

import './index.css';

export default function Register() {
	const [name, setName] = useState('');
	const [email, setEmail] = useState('sajermannbruno@gmail.com');
	const [password, setPassword] = useState('12');
	const [confirmPassword, setConfirmPassword] = useState('123');
	const { replace } = useHistory();

	// useEffect(() => {
	// 	console.log('Page C, redirect for A');
	// 	replace('/A');
	// }, []);

	const dispatch = useDispatch();

	const { loading, error } = useSelector((state: any) => state.auth);
	console.log({ loading, error });

	function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();

		const user = {
			name,
			email,
			password,
			confirmPassword,
		};

		console.log({ user });
		// @ts-expect-error Esperado
		dispatch(register(user));
	}

	useEffect(() => {
		dispatch(reset());
	}, [dispatch]);

	return (
		<div id="register">
			<h2>React Gram</h2>
			<p className="subtitle">Cadastre-se para ver as fotos dos seus amigos.</p>
			<form onSubmit={handleSubmit}>
				<input
					type="text"
					placeholder="Nome"
					onChange={e => setName(e.target.value)}
					value={name}
				/>
				<input
					type="email"
					placeholder="Email"
					onChange={e => setEmail(e.target.value)}
					value={email}
				/>
				<input
					type="password"
					placeholder="Senha"
					onChange={e => setPassword(e.target.value)}
					value={password}
				/>
				<input
					type="password"
					placeholder="Confirme a Senha"
					onChange={e => setConfirmPassword(e.target.value)}
					value={confirmPassword}
				/>
				<input
					disabled={loading}
					type="submit"
					value={loading ? 'Aguarde...' : 'Cadastrar'}
				/>
				{error && <Message type="error" msg={error} />}
			</form>

			<p>
				Já tem conta? <Link to="/login">Clique aqui.</Link>
			</p>
		</div>
	);
}
