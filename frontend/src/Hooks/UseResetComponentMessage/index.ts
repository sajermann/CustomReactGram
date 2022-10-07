import { resetMessage } from '../../Slices/photoSlice';

export function useResetComponentMessage(dispatch: any) {
	return () => {
		setTimeout(() => {
			dispatch(resetMessage());
		}, 2000);
	};
}
