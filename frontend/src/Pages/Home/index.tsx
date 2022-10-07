import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { LikeContainer } from '../../Components/LikeContainer';
import { Message } from '../../Components/Message';
import { PhotoItem } from '../../Components/PhotoItem';
import { useResetComponentMessage } from '../../Hooks/UseResetComponentMessage';
import { getPhotos, like } from '../../Slices/photoSlice';
import './index.css';

export default function Home() {
	const dispatch = useDispatch();
	const resetMessage = useResetComponentMessage(dispatch);
	const { user } = useSelector((state: any) => state.auth);
	const { photos, loading, error, message } = useSelector(
		(state: any) => state.photo
	);

	useEffect(() => {
		// @ts-expect-error esperado
		dispatch(getPhotos());
	}, [dispatch]);

	function handleLike(idPhoto: string) {
		// @ts-expect-error esperado
		dispatch(like(idPhoto));
		resetMessage();
	}

	if (loading) {
		return <p>Carregando...</p>;
	}

	return (
		<div id="home">
			{photos &&
				photos.map((photo: any) => (
					<div key={photo.id}>
						<PhotoItem photo={photo} />
						<LikeContainer
							photo={photo}
							user={user}
							handleLike={() => handleLike(photo.id)}
						/>
						<Link className="btn" to={`/photos/${photo.id}`}>
							Ver mais
						</Link>
						<div className="message-container">
							{error && <Message msg={message} type="error" />}
							{message && <Message msg={message} type="success" />}
						</div>
					</div>
				))}
			{photos && photos.length === 0 && (
				<h2 className="no-photos">
					Ainda não há fotos publicadas,{' '}
					<Link to={`/users/${user.id}`}>Clique aqui</Link>
				</h2>
			)}
		</div>
	);
}
