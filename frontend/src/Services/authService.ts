import { api, requestConfig } from '../Utils/config';

async function register(data: any) {
	const config = requestConfig('POST', data);
	console.log({ config });
	console.log(`${api}/users/register`);
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

const authService = {
	register,
};

export default authService;
