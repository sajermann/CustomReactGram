export const api = 'https://localhost:5000/api';
export const uploads = 'https://localhost:5000/files';

const headers: Record<string, string> = {};

export const requestConfig = (
	method: string,
	data: BodyInit | null | undefined,
	token: string | null = null,
	image: boolean | null = null
) => {
	let config;

	if (image) {
		config = {
			method,
			body: data,
			headers,
		};
	} else if (method === 'DELETE' || data === null) {
		config = {
			method,
			headers,
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
	console.log({ token });
	if (token) {
		config.headers.Authorization = `Bearer ${token}`;
	}

	return config;
};
