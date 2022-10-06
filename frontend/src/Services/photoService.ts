import { api, requestConfig } from '../Utils/config';

async function publishPhoto(data: any, token: string) {
	const config = requestConfig('POST', data, token, true);
	try {
		const resp = await fetch(`${api}/photos/upload`, config)
			.then(res => res.json())
			.catch(err => err);
		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

async function getUserPhotos(id: string, token: string) {
	const config = requestConfig('GET', null, token);
	try {
		const resp = await fetch(`${api}/photos/user/${id}`, config)
			.then(res => res.json())
			.catch(err => err);
		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

async function deletePhoto(id: string, token: string) {
	const config = requestConfig('DELETE', null, token);
	try {
		const resp = await fetch(`${api}/photos/${id}`, config)
			.then(() => id)
			.catch(err => err);
		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

async function updatePhoto(data: any, id: string, token: string) {
	const config = requestConfig('PUT', data, token);
	try {
		const resp = await fetch(`${api}/photos/${id}`, config)
			.then(res => res.json())
			.catch(err => err);
		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

async function getPhotoById(id: string, token: string) {
	const config = requestConfig('GET', null, token);
	try {
		const resp = await fetch(`${api}/photos/${id}`, config)
			.then(res => res.json())
			.catch(err => err);
		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

async function like(id: string, token: string) {
	const config = requestConfig('PUT', null, token);
	try {
		const resp = await fetch(`${api}/photos/like/${id}`, config)
			.then(res => res.json())
			.catch(err => err);
		return resp;
	} catch (error) {
		console.log({ error });
		return error;
	}
}

const photoService = {
	publishPhoto,
	getUserPhotos,
	deletePhoto,
	updatePhoto,
	getPhotoById,
	like,
};

export default photoService;
