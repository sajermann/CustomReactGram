/* eslint-disable jsx-a11y/click-events-have-key-events */
/* eslint-disable jsx-a11y/no-static-element-interactions */
import React, { useState } from 'react';
import {
	BsSearch,
	BsHouseDoorFill,
	BsFillPersonFill,
	BsFillCameraFill,
} from 'react-icons/bs';
import { useDispatch, useSelector } from 'react-redux';
import { Link, NavLink, useHistory } from 'react-router-dom';
import { useAuth } from '../../Hooks/UseAuth';
import { logout, reset } from '../../Slices/authSlice';

import './index.css';

export function Navbar() {
	const { auth } = useAuth();
	const { user } = useSelector((state: any) => state.auth);
	const [query, setQuery] = useState('');
	const history = useHistory();
	const dispatch = useDispatch();

	function handleLogout() {
		// @ts-expect-error Esperado
		dispatch(logout());
		dispatch(reset());
		history.push('/login');
	}

	function handleSubmit(e: React.FormEvent<HTMLFormElement>) {
		e.preventDefault();
		if (query.length > 0) {
			history.push(`/search?query=${query}`);
		}
	}

	return (
		<nav id="nav">
			<Link to="/">ReactGram</Link>
			<form id="search-form" onSubmit={handleSubmit}>
				<BsSearch />
				<input
					type="text"
					placeholder="Pesquisar"
					onChange={e => setQuery(e.target.value)}
				/>
			</form>

			<ul id="nav-links">
				{auth ? (
					<>
						<li>
							<NavLink to="/">
								<BsHouseDoorFill />
							</NavLink>
						</li>

						{user && (
							<li>
								<NavLink to={`/users/${user.id}`}>
									<BsFillCameraFill />
								</NavLink>
							</li>
						)}
						<li>
							<NavLink to="/profile">
								<BsFillPersonFill />
							</NavLink>
						</li>
						<li>
							<span onClick={handleLogout}>Sair</span>
						</li>
					</>
				) : (
					<>
						<li>
							<NavLink to="/login">Entrar</NavLink>
						</li>
						<li>
							<NavLink to="/register">Cadastrar</NavLink>
						</li>
					</>
				)}
			</ul>
		</nav>
	);
}
