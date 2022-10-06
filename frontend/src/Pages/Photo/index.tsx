import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import { LikeContainer } from '../../Components/LikeContainer';
import { PhotoItem } from '../../Components/PhotoItem';
import { getPhotoById, like } from '../../Slices/photoSlice';
import './index.css';

export default function Photo() {
	const { id } = useParams<{ id: string }>();
	const dispatch = useDispatch();
	const { user } = useSelector((state: any) => state.auth);
	const { photo, loading, error, message } = useSelector(
		(state: any) => state.photo
	);

	useEffect(() => {
		// @ts-expect-error Esperado
		dispatch(getPhotoById(id));
	}, [dispatch, id]);

	function handleLike() {
		// @ts-expect-error Esperado
		dispatch(like(photo.id));
	}

	if (loading) {
		return <p>Carregando...</p>;
	}

	return (
		<div id="photo">
			<PhotoItem photo={photo} />
			<LikeContainer photo={photo} user={user} handleLike={handleLike} />
		</div>
	);
}
