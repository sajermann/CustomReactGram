import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link, useHistory } from 'react-router-dom';
import { register, reset } from '../../Slices/authSlice';

import './index.css';

export default function Register() {
	const [name, setName] = useState('');
	const [email, setEmail] = useState('');
	const [password, setPassword] = useState('');
	const [confirmPassword, setConfirmPassword] = useState('');
	const { replace } = useHistory();

	// useEffect(() => {
	// 	console.log('Page C, redirect for A');
	// 	replace('/A');
	// }, []);

	const dispatch = useDispatch();

	const { loading, error } = useSelector(state => state.auth);
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
					required
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
				<input disabled={loading} type="submit" value="Cadastrar" />
			</form>

			<p>
				Já tem conta? <Link to="/login">Clique aqui.</Link>
			</p>
		</div>
	);
}
