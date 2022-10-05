/* eslint-disable no-param-reassign */
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import photoService from '../Services/photoService';

type InitialState = {
	photos: object[];
	photo: object;
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
			});
	},
});

export const { resetMessage } = photoSlice.actions;
export default photoSlice.reducer;
