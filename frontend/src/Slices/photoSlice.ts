/* eslint-disable no-param-reassign */
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import photoService from '../Services/photoService';

type InitialState = {
	photos: object[];
	photo: any;
	error: unknown | null;
	success: boolean;
	loading: boolean;
	message: string | null;
};

const initialState: InitialState = {
	photos: [],
	photo: {},
	error: false,
	success: false,
	loading: false,
	message: null,
};

export const publishPhoto = createAsyncThunk(
	'photo/publish',
	async (photo: any, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const data = await photoService.publishPhoto(photo, jwt);
		if (data.errors) {
			return thunkAPI.rejectWithValue(
				`${Object.keys(data.errors)[0]} - ${
					data.errors[Object.keys(data.errors)[0]]
				}`
			);
		}
		return data;
	}
);

export const getUserPhotos = createAsyncThunk(
	'photo/user',
	async (id: string, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const data = await photoService.getUserPhotos(id, jwt);
		if (data.errors) {
			return thunkAPI.rejectWithValue(
				`${Object.keys(data.errors)[0]} - ${
					data.errors[Object.keys(data.errors)[0]]
				}`
			);
		}
		return data;
	}
);

export const deletePhoto = createAsyncThunk(
	'photo/delete',
	async (id: string, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const data = await photoService.deletePhoto(id, jwt);
		if (data.errors) {
			return thunkAPI.rejectWithValue(
				`${Object.keys(data.errors)[0]} - ${
					data.errors[Object.keys(data.errors)[0]]
				}`
			);
		}
		return data;
	}
);

export const updatePhoto = createAsyncThunk(
	'photo/update',
	async (photoData: any, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const data = await photoService.updatePhoto(
			{ title: photoData.title },
			photoData.id,
			jwt
		);
		if (data.errors) {
			return thunkAPI.rejectWithValue(
				`${Object.keys(data.errors)[0]} - ${
					data.errors[Object.keys(data.errors)[0]]
				}`
			);
		}
		return data;
	}
);

export const getPhotoById = createAsyncThunk(
	'photo/getPhoto',
	async (id: string, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const data = await photoService.getPhotoById(id, jwt);
		if (data.errors) {
			return thunkAPI.rejectWithValue(
				`${Object.keys(data.errors)[0]} - ${
					data.errors[Object.keys(data.errors)[0]]
				}`
			);
		}
		return data;
	}
);

export const getPhotos = createAsyncThunk(
	'photo/getPhotos',
	async (_, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const data = await photoService.getPhotos(jwt);
		if (data.errors) {
			return thunkAPI.rejectWithValue(
				`${Object.keys(data.errors)[0]} - ${
					data.errors[Object.keys(data.errors)[0]]
				}`
			);
		}
		return data;
	}
);

export const like = createAsyncThunk(
	'photo/like',
	async (id: string, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const data = await photoService.like(id, jwt);
		if (data.errors) {
			return thunkAPI.rejectWithValue(
				`${Object.keys(data.errors)[0]} - ${
					data.errors[Object.keys(data.errors)[0]]
				}`
			);
		}
		return data;
	}
);

export const comment = createAsyncThunk(
	'photo/comment',
	async (commentData: any, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const resp = await photoService.comment(
			{ comment: commentData.comment },
			commentData.id,
			jwt
		);
		if (resp.errors) {
			return thunkAPI.rejectWithValue(
				`${Object.keys(resp.errors)[0]} - ${
					resp.errors[Object.keys(resp.errors)[0]]
				}`
			);
		}
		return resp;
	}
);

export const searchPhotoByTitle = createAsyncThunk(
	'photo/search',
	async (title: string, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const resp = await photoService.searchPhotoByTitle(title, jwt);
		if (resp.errors) {
			return thunkAPI.rejectWithValue(
				`${Object.keys(resp.errors)[0]} - ${
					resp.errors[Object.keys(resp.errors)[0]]
				}`
			);
		}
		return resp;
	}
);

export const photoSlice = createSlice({
	name: 'photo',
	initialState,
	reducers: {
		resetMessage: state => {
			state.message = null;
		},
	},

	extraReducers: builders => {
		builders
			.addCase(publishPhoto.pending, state => {
				state.loading = true;
				state.error = null;
			})
			.addCase(publishPhoto.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.photo = action.payload;
				state.photos.unshift(state.photo);
				state.message = 'Foto publicada com sucesso';
			})
			.addCase(publishPhoto.rejected, (state, action) => {
				state.loading = false;
				state.error = action.payload;
				state.photo = {};
			})
			.addCase(getUserPhotos.pending, state => {
				state.loading = true;
				state.error = null;
			})
			.addCase(getUserPhotos.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.photos = action.payload;
			})
			.addCase(getPhotos.pending, state => {
				state.loading = true;
				state.error = null;
			})
			.addCase(getPhotos.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.photos = action.payload;
			})
			.addCase(deletePhoto.pending, state => {
				state.loading = true;
				state.error = null;
			})
			.addCase(deletePhoto.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.photos = state.photos.filter(
					(item: any) => item.id !== action.payload
				);
				state.message = 'Foto excluída com sucesso';
			})
			.addCase(deletePhoto.rejected, (state, action) => {
				state.loading = false;
				state.error = action.payload;
				state.photo = {};
			})
			.addCase(updatePhoto.pending, state => {
				state.loading = true;
				state.error = null;
			})
			.addCase(updatePhoto.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.photos = state.photos.map((item: any) => {
					if (item.id === action.payload.id) {
						return { ...item, title: action.payload.title };
					}
					return item;
				});

				state.message = 'Foto atualizada com sucesso';
			})
			.addCase(updatePhoto.rejected, (state, action) => {
				state.loading = false;
				state.error = action.payload;
				state.photo = {};
			})
			.addCase(getPhotoById.pending, state => {
				state.loading = true;
				state.error = null;
			})
			.addCase(getPhotoById.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.photo = action.payload;
			})
			.addCase(searchPhotoByTitle.pending, state => {
				state.loading = true;
				state.error = null;
			})
			.addCase(searchPhotoByTitle.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.photos = action.payload;
			})
			.addCase(like.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.photo = action.payload;
				state.photos = state.photos.map((item: any) => {
					if (item.id === action.payload.id) {
						const likesTemp = [...(item.likes || [])];
						return { ...item, likes: [...likesTemp, action.payload.userId] };
					}
					return item;
				});

				state.message = 'O like foi atualizado';
			})
			.addCase(like.rejected, (state, action) => {
				state.loading = false;
				state.error = action.payload;
			})
			.addCase(comment.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;

				state.photo = action.payload;
				state.photos = state.photos.map((item: any) => {
					if (item.id === action.payload.id) {
						return {
							...item,
							comments: [...item.comments, action.payload.comments],
						};
					}
					return item;
				});

				state.message = 'O comentário foi adicionado';
			})
			.addCase(comment.rejected, (state, action) => {
				state.loading = false;
				state.error = action.payload;
			});
	},
});

export const { resetMessage } = photoSlice.actions;
export default photoSlice.reducer;
