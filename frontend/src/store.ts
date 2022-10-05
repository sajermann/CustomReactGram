import { configureStore } from '@reduxjs/toolkit';

import authReducer from './Slices/authSlice';
import userReducer from './Slices/userSlice';
import photoReducer from './Slices/photoSlice';

export const store = configureStore({
	reducer: {
		auth: authReducer,
		user: userReducer,
		photo: photoReducer,
	},
});
