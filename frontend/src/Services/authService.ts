import { api, requestConfig } from '../Utils/config';

async function register(data: any) {
	const config = requestConfig('POST', data);
	try {
		const resp = await fetch(`${api}/users/register`, config)
			.then(res => res.json())
			.catch(err => err);
		if (resp.status !== 400) {
			if (resp) {
				localStorage.setItem('user', JSON.stringify(resp));
			}
		}

		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

function logout() {
	localStorage.removeItem('user');
}

async function login(userForLogin: any) {
	const config = requestConfig('POST', userForLogin);

	try {
		const resp = await fetch(`${api}/users/login`, config)
			.then(res => res.json())
			.catch(res => res);
		if (resp.status !== 400) {
			if (resp) {
				localStorage.setItem('user', JSON.stringify(resp));
			}
		}

		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

const authService = {
	register,
	logout,
	login,
};

export default authService;
