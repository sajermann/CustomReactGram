import { useEffect } from 'react';
import { useHistory } from 'react-router-dom';

export default function Register() {
	const { replace } = useHistory();

	// useEffect(() => {
	// 	console.log('Page C, redirect for A');
	// 	replace('/A');
	// }, []);

	return <h1>Register</h1>;
}
