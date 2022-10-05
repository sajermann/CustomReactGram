/* eslint-disable no-param-reassign */
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import authService from '../Services/authService';

function getUserLocalStorage() {
	const userTemp = localStorage.getItem('user');
	if (!userTemp) {
		return null;
	}
	return JSON.parse(userTemp);
}

const user = getUserLocalStorage();

type InitialState = {
	user: object | null;
	error: unknown | null;
	success: boolean;
	loading: boolean;
};

const initialState: InitialState = {
	user: user || null,
	error: null,
	success: false,
	loading: false,
};

export const register = createAsyncThunk(
	'auth/register',
	async (userForRegister, thunkAPI) => {
		const data = await authService.register(userForRegister);
		console.log(data.errors[0]);
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

export const login = createAsyncThunk(
	'auth/login',
	async (userForRegister, thunkAPI) => {
		const data = await authService.login(userForRegister);
		console.log(data.errors[0]);
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

export const logout = createAsyncThunk('auth/logout', async () => {
	await authService.logout();
});

export const authSlice = createSlice({
	name: 'auth',
	initialState,
	reducers: {
		reset: state => {
			state.loading = false;
			state.error = null;
			state.success = false;
		},
	},

	extraReducers: builders => {
		builders
			.addCase(register.pending, state => {
				state.loading = true;
				state.error = null;
			})

			.addCase(register.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.user = action.payload;
			})

			.addCase(register.rejected, (state, action) => {
				state.loading = false;
				state.error = action.payload;
				state.user = null;
			})
			.addCase(logout.fulfilled, state => {
				state.loading = false;
				state.success = false;
				state.error = null;
				state.user = null;
			})
			.addCase(login.pending, state => {
				state.loading = true;
				state.error = null;
			})

			.addCase(login.fulfilled, (state, action) => {
				state.loading = false;
				state.success = true;
				state.error = null;
				state.user = action.payload;
			})

			.addCase(login.rejected, (state, action) => {
				state.loading = false;
				state.error = action.payload;
				state.user = null;
			});
	},
});

export const { reset } = authSlice.actions;
export default authSlice.reducer;
