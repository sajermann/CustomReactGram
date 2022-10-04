import { Redirect, Route, Switch } from 'react-router-dom';
import { useAuth } from '../../Hooks/UseAuth';
import EditProfile from '../EditProfile';
import Home from '../Home';
import Login from '../Login';
import Register from '../Register';

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
			<Route path="/login" exact>
				{!auth ? <Login /> : <Redirect to="/login" />}
			</Route>
			<Route path="/register" exact>
				{!auth ? <Register /> : <Redirect to="/login" />}
			</Route>
		</Switch>
	);
}
