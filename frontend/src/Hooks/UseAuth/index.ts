import { useEffect, useState } from 'react';
import { useSelector } from 'react-redux';

export function useAuth() {
	const { user } = useSelector((state: any) => state.auth);
	const [auth, setAuth] = useState(false);
	const [loading, setLoading] = useState(true);

	useEffect(() => {
		if (user) {
			setAuth(true);
		} else {
			setAuth(false);
		}

		setLoading(false);
	}, [user]);

	return { auth, loading };
}
