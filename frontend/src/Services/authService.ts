import { api, requestConfig } from '../Utils/config';

async function register(data: any) {
	const config = requestConfig('POST', data);
	console.log({ config });
	console.log(`${api}/users/register`);
	try {
		const resp = await fetch(`${api}/users/register`, config)
			.then(res => res.json())
			.catch(err => err);
		console.log({ resp });

		if (resp) {
			localStorage.setItem('user', JSON.stringify(resp));
		}
	} catch (error) {
		console.log({ error });
	}
}

const authService = {
	register,
};

export default authService;
