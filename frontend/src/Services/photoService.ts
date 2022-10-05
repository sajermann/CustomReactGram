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

const photoService = {
	publishPhoto,
};

export default photoService;
