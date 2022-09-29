import {
	BsSearch,
	BsHouseDoorFill,
	BsFillPersonFill,
	BsFillCameraFill,
} from 'react-icons/bs';
import { Link, NavLink } from 'react-router-dom';

export function Navbar() {
	return (
		<nav id="nav">
			<Link to="/">ReactGram</Link>
			<form>
				<BsSearch />
				<input type="text" />
			</form>

			<ul id="nav-links">
				<NavLink to="/">
					<BsHouseDoorFill />
				</NavLink>
				<NavLink to="/login">Entrar</NavLink>
				<NavLink to="/register">Cadastrar</NavLink>
			</ul>
		</nav>
	);
}
