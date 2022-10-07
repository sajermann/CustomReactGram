import { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Link } from 'react-router-dom';
import { LikeContainer } from '../../Components/LikeContainer';
import { PhotoItem } from '../../Components/PhotoItem';
import { useQuery } from '../../Hooks/UseQuery';
import { useResetComponentMessage } from '../../Hooks/UseResetComponentMessage';
import { searchPhotoByTitle, like } from '../../Slices/photoSlice';
import './index.css';

export default function Search() {
	const query = useQuery();
	const dispatch = useDispatch();
	const resetMessage = useResetComponentMessage(dispatch);
	const { user } = useSelector((state: any) => state.auth);
	const { photos, loading } = useSelector((state: any) => state.photo);
	useEffect(() => {
		// @ts-expect-error esperado
		dispatch(searchPhotoByTitle(query.get('query')));
	}, [dispatch, query]);

	function handleLike(idPhoto: string) {
		// @ts-expect-error esperado
		dispatch(like(idPhoto));
		resetMessage();
	}

	return (
		<div id="search">
			<h2>Você está buscando por: {query.get('query')}</h2>
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
					</div>
				))}
			{photos && photos.length === 0 && (
				<h2 className="no-photos">
					Não foram encontrados resultados para sua busca
				</h2>
			)}
		</div>
	);
}
