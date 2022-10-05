/* eslint-disable no-param-reassign */
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import userService from '../Services/userService';

type InitialState = {
	user: object | null;
	error: unknown | null;
	success: boolean;
	loading: boolean;
	message: string | null;
};

const initialState: InitialState = {
	user: {} || null,
	error: null,
	success: false,
	loading: false,
	message: null,
};

export const profile = createAsyncThunk(
	'user/profile',
	async (user: any, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const data = await userService.profile(user, jwt);
		return data;
	}
);

export const updateProfile = createAsyncThunk(
	'user/update',
	async (user: any, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const data = await userService.updateProfile(user, jwt);
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

export const updateProfileImage = createAsyncThunk(
	'user/updateProfileImage',
	async (imageForUpdate: any, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		console.log({ imageForUpdate });
		const data = await userService.updateProfileImage(imageForUpdate, jwt);
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

export const getUserDetails = createAsyncThunk(
	'user/getUserDetails',
	async (id: string, thunkAPI: any) => {
		const { jwt } = thunkAPI.getState().auth.user;
		const data = await userService.getUserDetails(id, jwt);
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

export const userSlice = createSlice({
	name: 'user',
	initialState,
	reducers: {
		resetMessage: state => {
			state.message = null;
		},
	},

	extraReducers: builders => {
		builders
			.addCase(profile.pending, state => {
				state.loading = true;
				state.error = null;
			})
			.addCase(profile.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.user = action.payload;
			})
			.addCase(updateProfile.pending, state => {
				state.loading = true;
				state.error = null;
			})

			.addCase(updateProfile.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.user = action.payload;
				state.message = 'Usuário atualizado com sucesso';
			})

			.addCase(updateProfile.rejected, (state, action) => {
				state.loading = false;
				state.error = action.payload;
				state.user = {};
			})
			.addCase(updateProfileImage.pending, state => {
				state.loading = true;
				state.error = null;
			})

			.addCase(updateProfileImage.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
			})

			.addCase(updateProfileImage.rejected, (state, action) => {
				state.loading = false;
				state.error = action.payload;
			})
			.addCase(getUserDetails.pending, state => {
				state.loading = true;
				state.error = null;
			})
			.addCase(getUserDetails.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.user = action.payload;
			});
	},
});

export const { resetMessage } = userSlice.actions;
export default userSlice.reducer;
