import { api, requestConfig } from '../Utils/config';

async function profile(data: any, token: string) {
	const config = requestConfig('GET', data, token);
	try {
		const resp = await fetch(`${api}/users/profile`, config)
			.then(res => res.json())
			.catch(err => err);
		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

async function updateProfile(data: any, token: string) {
	const config = requestConfig('PUT', data, token);
	try {
		const resp = await fetch(`${api}/users/`, config)
			.then(res => res.json())
			.catch(err => err);

		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

async function updateProfileImage(data: any, token: string) {
	const config = requestConfig('POST', data, token, true);
	try {
		const resp = await fetch(`${api}/users/upload`, config)
			.then(res => res.json())
			.catch(err => err);

		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

async function getUserDetails(id: string, token: string) {
	const config = requestConfig('GET', undefined, token);
	try {
		const resp = await fetch(`${api}/users/${id}`, config)
			.then(res => res.json())
			.catch(err => err);

		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

const userService = {
	profile,
	updateProfile,
	updateProfileImage,
	getUserDetails,
};

export default userService;
