import { Redirect, Route, Switch } from 'react-router-dom';
import { useAuth } from '../../Hooks/UseAuth';
import EditProfile from '../EditProfile';
import Home from '../Home';
import Login from '../Login';
import Photo from '../Photo';
import Profile from '../Profile';
import Register from '../Register';
import Search from '../Search';

export default function Routes() {
	const { auth, loading } = useAuth();
	if (loading) {
		return <p>Carregando...</p>;
	}

	return (
		<Switch>
			<Route path="/" exact>
				{auth ? <Home /> : <Redirect to="/login" />}
			</Route>
			<Route path="/profile" exact>
				{auth ? <EditProfile /> : <Redirect to="/login" />}
			</Route>
			<Route path="/users/:id" exact>
				{auth ? <Profile /> : <Redirect to="/login" />}
			</Route>
			<Route path="/login" exact>
				{!auth ? <Login /> : <Redirect to="/" />}
			</Route>
			<Route path="/register" exact>
				{!auth ? <Register /> : <Redirect to="/login" />}
			</Route>
			<Route path="/search">
				{auth ? <Search /> : <Redirect to="/login" />}
			</Route>
			<Route path="/photos/:id" exact>
				{auth ? <Photo /> : <Redirect to="/login" />}
			</Route>
		</Switch>
	);
}
