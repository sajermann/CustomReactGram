import { configureStore } from '@reduxjs/toolkit';

import authReducer from './Slices/authSlice';
import userReducer from './Slices/userSlice';

export const store = configureStore({
	reducer: {
		auth: authReducer,
		user: userReducer,
	},
});
