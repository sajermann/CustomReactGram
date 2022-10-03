import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import authService from '../Services/authService';

const user = JSON.parse(localStorage.getItem('user') || null);

const initialState = {
	user: user || null,
	error: false,
	success: false,
	loading: false,
};

export const register = createAsyncThunk(
	'auth/register',
	async (user, thunkAPI) => {
		const data = await authService.register(user);

		if (data.errors) {
			return thunkAPI.rejectWithValue(data.errors[0]);
		}

		return data;
	}
);

export const authSlice = createSlice({
	name: 'auth',
	initialState,
	reducers: {
		reset: state => {
			state.loading = false;
			state.error = false;
			state.success = false;
		},
	},

	extraReducers: builders => {
		builders.addCase(register.pending, state => {
			state.loading = true;
			state.error = false;
		});

		builders.addCase(register.fulfilled, (state, action) => {
			state.loading = false;
			state.success = true;
			state.error = false;
			state.user = action.payload;
		});

		builders.addCase(register.rejected, (state, action) => {
			state.loading = false;
			state.error = action.payload;
			state.user = null;
		});
	},
});

export const { reset } = authSlice.actions;
export default authSlice.reducer;
