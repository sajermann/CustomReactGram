export const api = 'http://localhost:5000/api';
export const uploads = 'http://localhost:5000/api/files';

export const requestConfig = (
	method: string,
	data: object,
	token: any | null = null,
	image: any | null = null
) => {
	let config;

	if (image) {
		config = {
			method,
			body: data,
			headers: {},
		};
	} else if (method === 'DELETE' || data === null) {
		config = {
			method,
			headers: {},
		};
	} else {
		config = {
			method,
			body: JSON.stringify(data),
			headers: {
				'Content-Type': 'application/json',
			},
		};
	}

	if (token) {
		config.headers.Authorization = `Bearer ${token}`;
	}

	return config;
};
